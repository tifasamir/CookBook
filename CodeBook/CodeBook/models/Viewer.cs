using CodeBook.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace CodeBook
{
    public class Viewer
    {
        //lesson will have list of viewer
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Category("refernce")]
        public int ViewerId { get; set; }
        public string imageurl { get; set; }
        public string fileurl { get; set; }
        public string snippeturl { get; set; }
        [Category("refernce")]
        public int LessonId { get; set; }
        public  Lesson Lesson { get; set; }

        //public virtual string txt1 { get; set; }
        //public virtual string txt2 { get; set; }
        //public virtual string txt3 { get; set; }
        //public virtual Image image { get; set; }
}
}
