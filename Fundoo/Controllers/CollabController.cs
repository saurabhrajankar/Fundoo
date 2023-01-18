using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Entities;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [HttpPost]
        [Route("AddCollab")]
        public IActionResult Addcollab(long NoteId, EmailModel email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = collabBL.AddCollab(NoteId, email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "CollabEntity Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "CollabEntity not Added" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("RetriveData")]
        public IActionResult GetAllNotes(long noteId)
        {
            try
            {
                var result = collabBL.GetAllNotes(noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Retrive data sucessfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Retrive data not sucessfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {
                if (collabBL.DeleteNote(NoteId))
                {
                    return this.Ok(new { Success = true, message = "Note Deleted", });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note not Deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
 
}
