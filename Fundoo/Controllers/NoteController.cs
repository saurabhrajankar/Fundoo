using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entities;
using System.Linq;
using System;
using Experimental.System.Messaging;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }
        [Authorize]
        [HttpPost]
        [Route("NotesModel")]
        public IActionResult AddNote(NotesModel notesModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value); 
                var responce = noteBL.AddNote(notesModel, userId);
                if (responce != null)
                {
                    return this.Ok(new { Success = true, message = "Note Added", data = responce });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Adding Failes"});
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateNotes")]
        public IActionResult updatenotes(NotesModel notes, long Noteid)
        {

            try
            {
                //long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "Id").Value);
                var result = noteBL.UpdateNotes(notes, Noteid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Update note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {
                if (noteBL.DeleteNote(NoteId))
                {
                    return this.Ok(new { Success = true, message = "Note Deleted",});
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
        [Authorize]
        [HttpPut]
        [Route("ArchiveNote")]
        public IActionResult ArchiveNote(long NoteId)
        {
            try
            {
                var result = noteBL.ArchiveOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Archived Successfully", responce = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable To Archive Note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("PinOrNot")]
        public IActionResult PinOrNot(long NoteId)
        {
            try
            {
                var result = noteBL.PinOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true,Message = "pinning successfully", responce = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Unable to pin Note" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
   
}
