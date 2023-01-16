using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entities;
using System.Linq;
using System;

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
    }
   
}
