using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity AddCollab(long NoteId, EmailModel email)
        {
            try
            {
                var noteResult = fundooContext.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                var emailResult = fundooContext.Users.Where(x => x.Email == email.Email).FirstOrDefault();

                if (emailResult != null && noteResult != null)
                {
                    CollabEntity objCollab = new CollabEntity();
                    objCollab.Email = emailResult.Email;
                    objCollab.NotesID = noteResult.NoteID;
                    objCollab.UserId = emailResult.UserId;
                    fundooContext.Add(objCollab);
                    fundooContext.SaveChanges();
                    return objCollab;
                }
                else
                {
                    return null;    
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<NoteEntity> GetAllNotes(long noteId)
        {
            try
            {
                var Result = fundooContext.Notes.Where(n => n.NoteID == noteId).ToList();
                if (Result != null)
                {
                    return Result;
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
    }
}
