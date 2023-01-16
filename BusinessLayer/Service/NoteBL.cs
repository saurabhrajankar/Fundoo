using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL:INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public NoteEntity AddNote(NotesModel notesModel, long UserId)
        {
            try
            {
                return noteRL.AddNote(notesModel,UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
