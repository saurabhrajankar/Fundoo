using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RepoLayer.Service
{
    public class NoteRL:INoteRL
    {
        FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public NoteEntity AddNote(NotesModel notesModel,long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.UserId = UserId;
                noteEntity.Title = notesModel.Title;
                noteEntity.Descrption = notesModel.Descrption;
                noteEntity.Reminder = notesModel.Reminder;
                noteEntity.Color = notesModel.Color;
                noteEntity.Image = notesModel.Image;
                noteEntity.ArchiveNote = notesModel.ArchiveNote;
                noteEntity.PinNote = notesModel.PinNote;
                noteEntity.DeleteNote = notesModel.DeleteNote;
                noteEntity.Created = notesModel.Created;
                noteEntity.Modified = notesModel.Modified;
                fundooContext.Notes.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<NoteEntity> GetAllNotes(long UserId)
        {
            try
            {
                var allnotes = fundooContext.Notes.Where(n => n.UserId == UserId).ToList();
                if (allnotes != null)
                {
                    return allnotes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity UpdateNotes(NotesModel notes, long NoteId)
        {
            try
            {
                NoteEntity result = fundooContext.Notes.Where(e => e.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    NoteEntity noteEntity = new NoteEntity();
                    result.Title = notes.Title;
                    result.Descrption = notes.Descrption;
                    result.Reminder = notes.Reminder;
                    result.Color = notes.Color;
                    result.Image = notes.Image;
                    result.ArchiveNote = notes.ArchiveNote;
                    result.PinNote = notes.PinNote;
                    result.DeleteNote = notes.DeleteNote;
                    result.Created = notes.Created;
                    fundooContext.Notes.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNote(long NoteId)
        {
            try
            {
                var deletenotes = fundooContext.Notes.FirstOrDefault(n => n.NoteID == NoteId);
                if (deletenotes != null)
                {
                    fundooContext.Notes.Remove(deletenotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ArchiveOrNot(long NoteId)
        {
            try
            {
                var result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (result.ArchiveNote != null)
                {
                    result.ArchiveNote = !result.ArchiveNote;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.ArchiveNote = !result.ArchiveNote;
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinOrNot(long NoteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (result.PinNote != null)
                {
                    result.PinNote = !result.PinNote;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.PinNote = !result.PinNote;
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool TrashOrNot(long NoteId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (result.Trash != null)
                {
                    result.Trash = !result.Trash;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = !result.Trash;
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }   
        }
        public NoteEntity UpdateColor(long NoteId, string Color)
        {
            try
            {
                var result = this.fundooContext.Notes.FirstOrDefault(e => e.NoteID == NoteId);
                if (result != null)
                {
                    result.Color = Color;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UploadImage(long NoteId, long UserId, IFormFile img)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == NoteId && e.UserId == UserId);
                if (result != null)
                {
                    Account acc = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                        this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]);
                    Cloudinary cloudinary = new Cloudinary(acc);
                    var uploadpic = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadresult = cloudinary.Upload(uploadpic);
                    string imgpath = uploadresult.Url.ToString();
                    result.Image = imgpath;
                    fundooContext.SaveChanges();
                    return "Image Uploaded Successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
