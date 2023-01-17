using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        public string Title { get; set; }
        public string Descrption { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool ArchiveNote { get; set; }
        public bool PinNote { get; set; }
        public bool DeleteNote { get; set; }
        public bool Trash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        
    }
}
