using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeBook.Configs
{
    class ShortCutsConfig
    {
        public   ShortCutsConfig()
        {
            AddLanguage = "LCTRL + Q ";
            AddChapter = "LCTRL + W ";
            AddLesson = "LCTRL + E ";
        }
        [CategoryAttribute("Add New Lang"),
        DescriptionAttribute("Short Cut to add new Language Record")]
        public string AddLanguage { get; set; }
        [CategoryAttribute("Add New Chapter"),
         DescriptionAttribute("Short Cut to add new Chapter Record")]

        public string AddChapter { get; set; }
        [CategoryAttribute("Add New Lesson"),
         DescriptionAttribute("Short Cut to add new Lesson Record")]

        public string AddLesson { get; set; }

    }
}
