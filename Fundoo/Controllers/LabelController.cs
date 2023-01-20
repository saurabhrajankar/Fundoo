using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepoLayer.Entities;
using System.Collections.Generic;

namespace Fundoo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [HttpPost]
        [Route("Addlabel")]
        public IActionResult AddLabel(LabelModel labelModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelBL.AddLabel(labelModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Label Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Added" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetAllLabel")]
        public IActionResult GetAllLabel(long LabelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelBL.GetAllLabel(userId,LabelId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Label Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Added" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteNote(long LabelId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelBL.DeleteNote(userId, LabelId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Label Delete Succesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Not Deleted" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("UpdateLabel")]
        public IActionResult UpdateLabel( UpdateLabel update)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = labelBL.UpdateLabel(userId, update);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Label Update SuccessFully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Not Updated" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
