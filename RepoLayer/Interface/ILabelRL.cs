using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public bool AddLabel(LabelModel labelModel, long UserId);
        public List<LabalEntity> GetAllLabel(long UserID, long LabelId);
        public bool DeleteLabel(long UserID, long LabelId);
        public bool UpdateLabel(long userId, UpdateLabel update);
    }
}
