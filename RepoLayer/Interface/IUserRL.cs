using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Registration(UserRegistration userRegistration);
        public string UserLogin(UserLogin userLogin);
        public string ForgotPassword(string email);
    }
}
