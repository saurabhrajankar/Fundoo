using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRL:ILabelRL
    {
        FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext; 
        }
        public bool AddLabel(LabelModel labelModel,long UserId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.UserId== UserId);
                if (result != null)
                {
                    LabalEntity objlabel = new LabalEntity();
                    objlabel.NoteId = labelModel.NoteId;
                    objlabel.LabelName = labelModel.LabelName;
                    objlabel.UserId = UserId;
                    fundooContext.Add(objlabel);
                    int result1 =fundooContext.SaveChanges();
                    if (result1 > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabalEntity> GetAllLabel(long UserID,long LabelId)
        {
            try
            {
                var Result = fundooContext.Label.Where(n => n.LabelId == LabelId).ToList();
                if (Result != null)
                {
                    return Result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long UserID, long LabelId)
        {
            try
            {
                var deletenotes = fundooContext.Label.FirstOrDefault(n => n.LabelId == LabelId);
                if (deletenotes != null)
                {
                    fundooContext.Label.Remove(deletenotes);
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
        public bool UpdateLabel(long userId, UpdateLabel update)
        {
            try
            {
                var result = fundooContext.Label.Where(e => e.UserId == userId && e.LabelId == update.labelId).FirstOrDefault();
                if (result != null)
                {
                    result.LabelName = update.NewLabelName;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

