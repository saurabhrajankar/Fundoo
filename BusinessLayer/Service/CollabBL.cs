using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL: ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabEntity AddCollab(long NoteId, EmailModel email)
        {
            try
            { 
                return collabRL.AddCollab(NoteId,email); 
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
                return collabRL.GetAllNotes(noteId);
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
                return collabRL.DeleteNote(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
