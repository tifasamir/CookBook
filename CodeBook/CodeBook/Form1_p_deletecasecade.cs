using CodeBook.Constants;
using CodeBook.db;
using CodeBook.Dialog;
using CodeBook.models;
using CodeBook.services;
using CodeBook.services.render;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBook
{

      partial class Form1
    {

        //casecading Delete Lesson
        private void button16_Click(object sender, EventArgs e)
        {
            CascadingDeleteLesson();
        }
        //cascade Delete Chapters
        private void button17_Click(object sender, EventArgs e)
        {
            CascadingDeleteChapter();
        }

        public void CascadingDeleteChapter()
        {
            DialogResult dialogResult = MessageBox.Show("Are You sure to Delete this Chapters", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Chapter l = ((Chapter)(listBox2.DataItems[listBox2.SelectedIndex]));
                    int langID = l.LangId;
                    int chapterId = l.ChapterId;


                    chServ.deleteCascade(chapterId);
                    refreshlistBox3();
                    reloadChapterForLang(langID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        public void CascadingDeleteLesson()
        {
            refreshViewer();
            DialogResult dialogResult = MessageBox.Show("Are You sure to Delete this Lesson", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Lesson l = ((Lesson)(listBox3.DataItems[listBox3.SelectedIndex]));
                    int chapterId = l.ChapterId;
                    int LessonId = l.LessonId;


                    lessonServ.deleteCascade(LessonId);
                    reloadLessonsForCh(chapterId);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        public void CascadingDeleteLang()
        {
            DialogResult dialogResult = MessageBox.Show("Are You sure to Delete this Lang", "Confirm", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    if (listBox1.SelectedIndex != null)
                    {
                        int LangId = ((Lang)listBox1.DataItems[listBox1.SelectedIndex]).LangId;

                        langServ.deleteCascade(LangId);
                        reloadLang();
                        refreshlistBox2();
                        refreshlistBox3();
                    }
                    else
                    {
                        MessageBox.Show("Please Select An Item To Del ");
                    }

                }
                catch (Exception ex)
                {

                }


            }

        }



        public void DeleteLang(object sender, EventArgs e)
        {
            CascadingDeleteLang();

        }
        public void DeleteLesson(object sender, EventArgs e)
        {
            CascadingDeleteLesson();
        }
        public void DeleteChapter(object sender, EventArgs e)
        {
            CascadingDeleteChapter();

        }



    }
}