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
      

        private void listBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                if (listBox2?.SelectedItems.Count > 0)
                {
                    int indexChapter = ((MyListBox)listBox2).SelectedIndex;
                    int indexLesson = ((MyListBox)listBox3).SelectedIndex;
                    Chapter ch = (Chapter)((MyListBox)listBox2).DataItems[indexChapter];
                    Lesson l = (Lesson)((MyListBox)listBox3).DataItems[indexLesson];


                    Dictionary<string, Object> options = new Dictionary<string, Object>();
                    options.Add("mode", "edit");
                    options.Add("Lesson", l);

                    LessonScreen frm = new LessonScreen(ctx, ch, options);
                    frm.Show();
                    frm.FormClosed += new FormClosedEventHandler(reloadLessonList);

                }
                else
                {
                    MessageBox.Show("select Chapter To Add Lesson To ?");
                }

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItems.Count > 0)
                {
                    int index = listBox1.SelectedIndex;
                    selectedLang = (Lang)listBox1.DataItems[index];


                    refreshlistBox2();
                    refreshlistBox3();



                    reloadChapterForLang(selectedLang.LangId);
                }

            }

            catch (System.ArgumentOutOfRangeException ex)
            {

            }
            //if (e.Button == MouseButtons.Right)
            //{
            //    //langFocusItem = listBox1.FocusedItem;
            //    var langFocusItem = listBox1.SelectedItems[0];
            //    if (langFocusItem != null && langFocusItem.Bounds.Contains(e.Location))
            //    {

            //        // MessageBox.Show("RightClick" +((Lang)((MyListViewItem)langFocusItem) .Data).name);
            //        contextMenuStrip1 = cMenuServ.showContext((MyListViewItem)langFocusItem);
            //        contextMenuStrip1.Show(Cursor.Position);
            //    }
            //}
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    //langFocusItem = (MyListViewItem)listView2.FocusedItem;
            //    var langFocusItem = listBox2.FocusedItem;
            //    if (langFocusItem != null && langFocusItem.Bounds.Contains(e.Location))
            //    {
            //        contextMenuStrip1 = cMenuServ.showContext((MyListViewItem)langFocusItem);
            //        contextMenuStrip1.Show(Cursor.Position);
            //    }
            //}


            try
            {
                if (listBox2.SelectedItems?.Count > 0)
                {
                    int index = listBox2.SelectedIndex;
                    selectedChapter = (Chapter)(listBox2).DataItems[index];
                    refreshlistBox3();

                    reloadLessonsForCh(selectedChapter.ChapterId);
                }

            }
            catch (System.ArgumentOutOfRangeException ex)
            {

            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            //messageBoxCS.AppendFormat("{0} = {1}", "IsSelected", e.IsSelected);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "Item", e.Item);
            //messageBoxCS.AppendLine();
            //messageBoxCS.AppendFormat("{0} = {1}", "ItemIndex", e.ItemIndex);
            //messageBoxCS.AppendLine();
            //MessageBox.Show(messageBoxCS.ToString(), "ItemSelectionChanged Event");
            if (listBox3.SelectedItems?.Count > 0)
            {
                int index = listBox3.SelectedIndex;


                try
                {
                    int lessId = ((Lesson)(listBox3.DataItems[index])).LessonId;

                    //    refreshViewer();
                    reloadListViewForLesson(lessId);
                    viewPages();
                }
                catch (Exception ex)
                {

                }

            }
            //    else   {

            //    refreshlistBox3();
            //}
        }
        
        private void reloadLessonList(object sender, FormClosedEventArgs e)
        {
            try
            {
                int chId = ((Chapter)listBox2.DataItems[listBox1.SelectedIndex]).ChapterId;
                reloadLessonsForCh(chId);

            }
            catch (Exception ex)
            {

            }
        }
        private void reloadChapterList(object sender, EventArgs e)
        {
            int LangId = ((Lang)listBox1.DataItems[listBox1.SelectedIndex]).LangId;

            reloadChapterForLang(LangId);
        }
        public void reloadLangList(object sender, EventArgs e)
        {
            reloadLang();
        }
      
        public void reloadLang()
        {
            refreshlistBox1();
            foreach (Lang item in langServ.findAll())
            {
                listBox1.Items.Add(item.name);
                listBox1.DataItems.Add(item);
                listBox1.listType = GlobalConstants.TYPE_LANG;

            }
            label3.Text = listBox1.Items.Count.ToString();

        }
        private void reloadChapterForLang(int LangId)
        {
            refreshlistBox2();

            foreach (Chapter item in chServ.findChByLangId(LangId))
            {
                listBox2.Items.Add(item.name);
                listBox2.DataItems.Add(item);
                listBox2.listType = GlobalConstants.TYPE_CHAPTER;
            }
            label4.Text = listBox2.Items.Count.ToString();
        }
        private void reloadLessonsForCh(int chId)
        {
            refreshlistBox3();
           
            foreach (Lesson item in lessonServ.findLessByChId(chId))
            {
                
                listBox3.Items.Add(item.name);
                listBox3.DataItems.Add(item);
                listBox3.listType = GlobalConstants.TYPE_LESSON;
            }

            label6.Text = listBox3.Items.Count.ToString();
        }
    }
}