using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
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
    }
}
