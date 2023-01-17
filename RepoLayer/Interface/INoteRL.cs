using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NotesModel notesModel, long UserId);
        public bool DeleteNote(long NoteId);
        public bool ArchiveOrNot(long noteid);
        public bool PinOrNot(long noteid);
        public NoteEntity UpdateNotes(NotesModel notes, long noteid);
    }
}
