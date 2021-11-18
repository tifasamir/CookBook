using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.services.render
{
    public class MyListBox :ListBox
    {

        public MyListBox()
        {
            DataItems = new List<Object>();
        }
        public Object Data { set; get; }
        public string listType { set; get; }

        public List<Object> DataItems { set; get; }
    }
}
