using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using System;
using System.Security.Claims;

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
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult PasswordReset(string new_password, string confirm_password)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetPassword(email, new_password, confirm_password);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Password Reset Successfull" });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Password Reset Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
    
