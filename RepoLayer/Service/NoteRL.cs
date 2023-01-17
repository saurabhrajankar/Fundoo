using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class NoteRL:INoteRL
    {
        FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NoteEntity AddNote(NotesModel notesModel,long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.UserId = UserId;
                noteEntity.Title = notesModel.Title;
                noteEntity.Descrption = notesModel.Descrption;
                noteEntity.Reminder = notesModel.Reminder;
                noteEntity.Color = notesModel.Color;
                noteEntity.Image = notesModel.Image;
                noteEntity.ArchiveNote = notesModel.ArchiveNote;
                noteEntity.PinNote = notesModel.PinNote;
                noteEntity.DeleteNote = notesModel.DeleteNote;
                noteEntity.Created = notesModel.Created;
                noteEntity.Modified = notesModel.Modified;
                fundooContext.Notes.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NotesModel notes, long NoteId)
        {
            try
            {
                NoteEntity result = fundooContext.Notes.Where(e => e.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    NoteEntity noteEntity = new NoteEntity();
                    result.Title = notes.Title;
                    result.Descrption = notes.Descrption;
                    result.Reminder = notes.Reminder;
                    result.Color = notes.Color;
                    result.Image = notes.Image;
                    result.ArchiveNote = notes.ArchiveNote;
                    result.PinNote = notes.PinNote;
                    result.DeleteNote = notes.DeleteNote;
                    result.Created = notes.Created;
                    fundooContext.Notes.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                return null;
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
                var deletenotes = fundooContext.Notes.FirstOrDefault(n => n.NoteID == NoteId);
                if (deletenotes != null)
                {
                    fundooContext.Notes.Remove(deletenotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ArchiveOrNot(long NoteId)
        {
            try
            {
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (result.ArchiveNote != null)
                {
                    result.ArchiveNote = !result.ArchiveNote;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.ArchiveNote = !result.ArchiveNote;
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinOrNot(long NoteId)
        {
            try
            {
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (result.PinNote != null)
                {
                    result.PinNote = !result.PinNote;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.PinNote = !result.PinNote;
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool TrashOrNot(long NoteId)
        {
            NoteEntity result=fundooContext.Notes.FirstOrDefault(x=>x.NoteID == NoteId);
            if (result.Trash != null)
            {
                result.Trash = !result.Trash;
                fundooContext.SaveChanges();
                return false;
            }
            else
            { 
                result.Trash = !result.Trash;
                return true;
            }
        }
       
    }
}
