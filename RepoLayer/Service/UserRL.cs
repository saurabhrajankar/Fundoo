using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                objentity.Password = userRegistration.Password;
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
                var result = fundooContext.Users.Where(x => x.Email == userLogin.Email && x.Password==userLogin.Password).FirstOrDefault();
                if (result != null)
                {
                    var tocken = GenerateSecurityToken(result.Email, result.UserId);
                    return tocken;
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



