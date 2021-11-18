using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.services.render
{
    public class MyButton :Button
    {
        public Object Data { set; get; }
        public string listType { set; get; }
    }
}
