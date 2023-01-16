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
    }
}
