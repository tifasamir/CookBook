using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeBook.models
{
    public class Lang
    {
        public Lang()
        {
            Chapters = new List<Chapter>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        

        [Category("refernce")]
        public int LangId { get; set; }
        public string name { get; set; }
        [Browsable(false)]
        [Category("refernce")]
        public string desc { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
