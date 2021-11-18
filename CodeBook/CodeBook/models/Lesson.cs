using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeBook.models
{
    public class Lesson
    {
        public Lesson()
        {
            Views = new List<Viewer>();
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Category("refernce")]
        [Browsable(false)]
        public int LessonId { get; set; }
        public string name { get; set; }
        [Browsable(false)]
        [Category("refernce")]
        public string desc { get; set; }

        //public Lang LangId { get; set; }
  
        [Category("refernce")]
        public int ChapterId { get; set; }
        [Browsable(false)]
        public Chapter Chapter { get; set; }
        [Browsable(false)]
        public List<Viewer> Views { get; set; }
    }
}
