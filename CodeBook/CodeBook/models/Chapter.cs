using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.models
{
   
    public  class Chapter
    {
        public Chapter()
        {
            Lessons = new List<Lesson>();

           
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Category("refernce") ]
  
        public int ChapterId { get; set; }
        public string name { get; set; }
        [Browsable(false)]
        public string desc { get; set; }
        
        [Category("refernce")]

        public int LangId { get; set; }
        [Browsable(false)]
        public Lang Lang { get; set; }

        public List<Lesson> Lessons { get; set; }

    }
}
