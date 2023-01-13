using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL :IUserBL
    {
        IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
             {
                return iuserRL.Registration(userRegistration);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UserLogin(UserLogin userLogin)
        {
            try
            {
                return iuserRL.UserLogin(userLogin);
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
                return iuserRL.ForgotPassword(email);
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
                return iuserRL.ResetPassword(email, new_password, confirm_password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
