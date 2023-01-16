using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL 
    {
        public NoteEntity AddNote(NotesModel notesModel, long UserId);
    }
}
