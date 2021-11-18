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
        DataTable dt = new DataTable();


        //search
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "" && textBox2.Text != null) { 
              
                    getSearchOption();
                    dt.Rows.Clear();
                    dt.Columns.Clear();

                    dt.Columns.Add("lineno", typeof(string));
                    dt.Columns.Add("filename", typeof(string));
                    DirectoryInfo rootDir = new DirectoryInfo(IoService.MaterialLoc);
                    WalkDirectoryTree(rootDir);
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();
                    dataGridView2.Refresh();
                }
            }
            catch(Exception ex)
            {

            }
         
        }

        private void getSearchOption()
        {
            //if (langRadio.Checked == true)
            //{
            //    if (titledRadio.Checked == true) searchByTitle(GlobalConstants.TYPE_LANG);
            //    else if (filesRadio.Checked == true) searchInFiles(GlobalConstants.TYPE_LANG);
            //    else if (snipRadio.Checked == true) searchInSnip(GlobalConstants.TYPE_LANG);
            //    else if (radioAll2.Checked == true) searchInAll(GlobalConstants.TYPE_LANG);
            //}
            //if (chRadio.Checked == true)
            //    if (titledRadio.Checked == true) searchByTitle(GlobalConstants.TYPE_CHAPTER);
            //    else if (filesRadio.Checked == true) searchInFiles(GlobalConstants.TYPE_CHAPTER);
            //    else if (snipRadio.Checked == true) searchInSnip(GlobalConstants.TYPE_CHAPTER);
            //    else if (radioAll2.Checked == true) searchInAll(GlobalConstants.TYPE_CHAPTER);
            //if (lessonRadio.Checked == true)
            //    if (titledRadio.Checked == true) searchByTitle(GlobalConstants.TYPE_LESSON);
            //    else if (filesRadio.Checked == true) searchInFiles(GlobalConstants.TYPE_LESSON);
            //    else if (snipRadio.Checked == true) searchInSnip(GlobalConstants.TYPE_LESSON);
            //    else if (radioAll2.Checked == true) searchInAll(GlobalConstants.TYPE_LESSON);
            //if (radioAll2.Checked == true)
            //    if (titledRadio.Checked == true) searchByTitle(GlobalConstants.TYPE_ALL);
            //    else if (filesRadio.Checked == true) searchInFiles(GlobalConstants.TYPE_ALL);
            //    else if (snipRadio.Checked == true) searchInSnip(GlobalConstants.TYPE_ALL);
            //    else if (radioAll2.Checked == true) searchInAll(GlobalConstants.TYPE_ALL);


        }

        private void searchInSnip(string type)
        {
            //if (type == GlobalConstants.TYPE_LANG)
            //{

            //}
        }

        private void searchInAll(string tYPE_LANG)
        {
            throw new NotImplementedException();
        }

        private void searchInFiles(string tYPE_LANG)
        {
            throw new NotImplementedException();
        }

        private void searchByTitle(string tYPE_LANG)
        {
            throw new NotImplementedException();
        }

        public void matchSearch(string filename)
        {
      
            Dictionary<string, string> found = new Dictionary<string, string>();
            string line;
            bool textFound = false;
            using (StreamReader file = new StreamReader(filename))
            {

                while ((line = file.ReadLine()) != null)
                {
                    if ((line.ToLower()).Contains(textBox2.Text.ToLower()))
                    {
                        found.Add(line, filename);
                        DataRow r =  dt.NewRow();
                        r[0] = filename.ToString();
                        r[1] = line.ToString();

                        dt.Rows.Add(r);
                        //dt.Rows["filename"].Add(filename);
                        //   MessageBox.Show("Error Found");
                        textFound = true;
                    }
                    //if (line.Contains("Warning: 1"))
                    //{
                    //    found.Add(line, filename);
                    //    MessageBox.Show("Warning Found");
                    //    textFound = true;
                    //}
                }
            }
            if (!textFound) Console.WriteLine("Your message here");
            //MessageBox.Show("Your message here");

        }
        public void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                // log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    matchSearch(fi.FullName);
                    Console.WriteLine(fi.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    if(!dirInfo.FullName.Contains("\\img")) WalkDirectoryTree(dirInfo);

                }
            }
        }
        private void LoadSearechedLesson(String myFileName)
        {
            try
            {
                   string search = myFileName.Replace(IoService.startupPath+"\\", "");
                    Viewer l = viewServ.findFile(search);
                    Lesson ll = lessonServ.find(l.LessonId);
                    Chapter c = chServ.find(ll.ChapterId);
                    Lang g = langServ.find(c.LangId);

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    var myItem = listBox1.Items[i];
                    Lang ii = (Lang)listBox1.DataItems[i];

                    if (ii.LangId == g.LangId) listBox1.SelectedIndex = i;
                }

                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    var myItem = listBox2.Items[i];
                    Chapter ii = (Chapter)listBox2.DataItems[i];

                    if (ii.ChapterId == c.ChapterId) listBox2.SelectedIndex = i;

                }

                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    var myItem = listBox1.Items[i];
                    Lesson ii = (Lesson)listBox3.DataItems[i];

                    if (ii.LessonId == ll.LessonId)
                    {

                        listBox3.SelectedIndex = i;
                        reloadListViewForLesson(ll.LessonId);
                    }
                  
                }
               
                    
                

            }
            catch (Exception ex)
            {

            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cellValue = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ;
            if (e.ColumnIndex == 0 && cellValue != null)
            {
                LoadSearechedLesson(cellValue);
            }
        }
    }
}