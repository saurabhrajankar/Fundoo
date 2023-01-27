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
        public List<NoteEntity> GetAllCollab(long noteId)
        {
            try
            {
                return collabRL.GetAllCollab(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCollab(long NoteId)
        {
            try
            {
                return collabRL.DeleteCollab(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
