using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeBook.models
{
    public class Setting
    {
        public Setting()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingId { get; set; }
        public string key { get; set; }
    
        public string value { get; set; }
        public string desc { get; set; }
    }
}
