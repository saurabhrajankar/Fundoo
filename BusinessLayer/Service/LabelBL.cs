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
    public class LabelBL:ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
          this.labelRL = labelRL;
        }

        public bool AddLabel(LabelModel labelModel, long UserId)
        {
            try
            {
                return labelRL.AddLabel(labelModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabalEntity> GetAllLabel(long UserID, long LabelId)
        {
            try
            {
                return labelRL.GetAllLabel(UserID, LabelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteLabel(long UserID, long LabelId)
        {
            try
            {
                return labelRL.DeleteLabel(UserID, LabelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateLabel(long userId, UpdateLabel update)
        {
            try
            {
                return labelRL.UpdateLabel(userId, update);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
