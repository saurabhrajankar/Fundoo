using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long NoteId, EmailModel email);
        public List<NoteEntity> GetAllNotes(long noteId);
        public bool DeleteNote(long NoteId);

    }
}
