using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using System;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }
        [HttpPost]
        [Route("UserRegistration")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = iuserBL.Registration(userRegistration);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration successfull", data = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration unsuccessfull" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("UserLogin")]
        public IActionResult UserLogin(UserLogin userLogin)
        {
            try
            {
                var result = iuserBL.UserLogin(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login successfull", data = result });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Login unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = iuserBL.ForgotPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Sent Successfully" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Sending Mail Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
}