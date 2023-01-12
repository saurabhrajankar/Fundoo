﻿using BusinessLayer.Interface;
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


    }
}