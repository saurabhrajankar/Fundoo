using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NotesModel notesModel, long UserId);
        public List<NoteEntity> GetAllNotes(long UserId);
        public NoteEntity UpdateNotes(NotesModel notes, long noteid);
        public bool DeleteNote(long NoteId);
        public bool ArchiveOrNot(long noteid);
        public bool PinOrNot(long noteid);
        public bool TrashOrNot(long NoteId);
        public NoteEntity UpdateColor(long NoteId, string Color);
        public string UploadImage(long NoteId, long UserId, IFormFile img);
    }
}
