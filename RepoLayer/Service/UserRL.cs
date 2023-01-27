using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        private readonly string _secret;
        private readonly string _expDate;
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }
        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                UserEntity objentity = new UserEntity();
                objentity.FirstName = userRegistration.FirstName;
                objentity.LastName = userRegistration.LastName;
                objentity.Email = userRegistration.Email;
                objentity.Password = EncryptPassword(userRegistration.Password);
                fundooContext.Add(objentity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return objentity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(UserLogin userLogin)
        {
            try
            {
                var result = fundooContext.Users.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                if (result != null)
                {
                    string decrypass = DecryptPassword(result.Password);
                    if (decrypass ==userLogin.Password)
                    {
                        var tocken = GenerateSecurityToken(result.Email, result.UserId);
                        return tocken;
                    }
                    return "Login failed";
                } 
                else
                {
                    return null;
                }  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string EncryptPassword(string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    return null;
                }
                else
                {
                    byte[] strongpassword = ASCIIEncoding.UTF8.GetBytes(Password);
                    string EncryptPassword = Convert.ToBase64String(strongpassword);
                    return EncryptPassword;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptPassword(string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    return null;
                }
                else
                {
                    byte[] encryptpassword = Convert.FromBase64String(Password);
                    string decryptpassword = ASCIIEncoding.ASCII.GetString(encryptpassword);
                    return decryptpassword;
                }

            }
            catch (Exception)
            {

                throw;
            }
  
        }
        public string ForgotPassword(string email)
        {
            try
            {
                var result = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                if (result != null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    MSMQ objMSMQ = new MSMQ();
                    objMSMQ.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, string new_password, string confirm_password)
        {
            try
            {
                if (new_password == confirm_password)
                {
                    var result = fundooContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    string newEncryptPass=EncryptPassword(new_password);
                    result.Password = new_password;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}



