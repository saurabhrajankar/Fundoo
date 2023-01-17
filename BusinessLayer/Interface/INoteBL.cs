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
        public bool DeleteNote(long NoteId);
        public bool ArchiveOrNot(long noteid);
        public bool PinOrNot(long noteid);
        public NoteEntity UpdateNotes(NotesModel notes, long noteid);
    }
}
