using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollab(long NoteId, EmailModel emailmodel);
        public List<NoteEntity> GetAllNotes(long noteId);
        public bool DeleteNote(long NoteId);
    }
}
