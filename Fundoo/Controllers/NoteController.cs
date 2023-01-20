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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Fundoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        //private readonly IMemoryCache memoryCache;
        private readonly FundooContext fundoocontext;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, FundooContext fundoocontext, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
           // this.memoryCache = memoryCache;
            this.fundoocontext = fundoocontext;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateNote")]
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
                    return this.BadRequest(new { Success = false, message = "Note Adding Failes" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllNotes")]
        public ActionResult GetAllNotes()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteBL.GetAllNotes(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Getting User Notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed To Load Notes" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    return this.Ok(new { success = true, Message = "pinning successfully"});
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
        [Authorize]
        [HttpPut]
        [Route("TrashOrNot")]
        public IActionResult TrashOrNote(long NoteId)
        {
            try
            {
                var result = noteBL.TrashOrNot(NoteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Trashed successfully", responce = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Unable to Trashed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateColor")]
        public IActionResult UpdateColor(long NoteId, string Color)
        {
            try
            {
                var result = noteBL.UpdateColor(NoteId, Color);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Color Updated successfully", responce = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "color Updatation faild" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage(long NoteId, long UserId, IFormFile img)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = noteBL.UploadImage(NoteId, UserId,img);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Image Uploaded Successfully", responce = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Failed To Upload Image" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Redies")]

        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NoteList";
            string serializedNoteList;
            var NoteList = new List<NoteEntity>();
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
            }
            else
            {
                NoteList = await fundoocontext.Notes.ToListAsync();
                serializedNoteList = JsonConvert.SerializeObject(NoteList);
                redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNoteList, options);
            }
            return Ok(NoteList);
        }
    } 
}
