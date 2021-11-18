using CodeBook.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.services.render
{
    public class ContextMenuService
    {
        Form1 _frm;
        public ContextMenuService(Form1 frm)
        {
            _frm = frm;
        }
      
            
        public ContextMenuStrip showContext(MyListViewItem n)
        {

            // 
            // contextMenuStrip1
            // 
            ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(61, 4);

            ToolStripMenuItem addLang = new ToolStripMenuItem();
            addLang.Name = "addTableToolStripMenuItem";
            addLang.Size = new System.Drawing.Size(180, 32);
            addLang.Text = "Add Language";
            addLang.Click += new System.EventHandler(_frm.showAddNewLang);

            ToolStripMenuItem addLesson = new ToolStripMenuItem();
            addLesson.Name = "addTableToolStripMenuItem";
            addLesson.Size = new System.Drawing.Size(180, 32);
            addLesson.Text = "Add Lesson";
            addLesson.Click += new System.EventHandler(_frm.showAddNewLesson);

            ToolStripMenuItem addChapter = new ToolStripMenuItem();
            addChapter.Name = "addTableToolStripMenuItem";
            addChapter.Size = new System.Drawing.Size(180, 32);
            addChapter.Text = "Add Chapter";
            addChapter.Click += new System.EventHandler(_frm.showAddNewChapter);

            ToolStripMenuItem DelLang = new ToolStripMenuItem();
            DelLang.Name = "addTableToolStripMenuItem";
            DelLang.Size = new System.Drawing.Size(180, 32);
            DelLang.Text = "Delete Lang";
            DelLang.Click += new System.EventHandler(_frm.DeleteLang);

            ToolStripMenuItem DelChap = new ToolStripMenuItem();
            DelChap.Name = "addTableToolStripMenuItem";
            DelChap.Size = new System.Drawing.Size(180, 32);
            DelChap.Text = "Delete Chapter";
            DelChap.Click += new System.EventHandler(_frm.DeleteChapter);

            ToolStripMenuItem DelLess = new ToolStripMenuItem();
            DelLess.Name = "addTableToolStripMenuItem";
            DelLess.Size = new System.Drawing.Size(180, 32);
            DelLess.Text = "Delete Lesson";
            DelLess.Click += new System.EventHandler(_frm.DeleteLesson);
            if (n.Data.GetType() == typeof(Lang))
            {
                contextMenuStrip1.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] {
            addLang ,
            addChapter,
            DelLang
                    }
                );
            }
            else if (n.Data.GetType() == typeof(Chapter))
            {
                contextMenuStrip1.Items.AddRange(
                    new System.Windows.Forms.ToolStripItem[] {
                    addLesson,
                    addChapter,
                    DelChap

            }
            );
            }
            else if (n.Data.GetType() == typeof(Lesson))
            {
                contextMenuStrip1.Items.AddRange(
            new System.Windows.Forms.ToolStripItem[] {
                            addLesson,
                            DelLess
            }
            );
            }
            //    contextMenuStrip1.Items.AddRange(
            //new System.Windows.Forms.ToolStripItem[] {
            //        addTable,
            //        addColumn ,
            //        DelTab,
            //        DelColumn,
            //}
            //);
            return contextMenuStrip1;
        }
    }
    
}
