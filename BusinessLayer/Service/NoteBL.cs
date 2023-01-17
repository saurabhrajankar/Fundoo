using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
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
                return noteRL.AddNote(notesModel, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity UpdateNotes(NotesModel notes, long noteid)
        {
            try
            {
                return noteRL.UpdateNotes(notes,noteid);

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
                return noteRL.DeleteNote(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ArchiveOrNot(long noteid)
        {
            try
            {
                return noteRL.ArchiveOrNot(noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool PinOrNot(long noteid)
        {
            try
            {
                return noteRL.PinOrNot(noteid);

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
                return noteRL.TrashOrNot(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
