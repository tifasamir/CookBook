using CodeBook.db;
using CodeBook.models;
using CodeBook.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.Dialog
{
    public partial class SearchScreen : Form
    {
        CodeBookContext ctx = new CodeBookContext();
        ChapterService chServ;
        LangService langServ;
        LessonService lessonServ;
        ViewerService viewServ;

        String _searchText;
        public SearchScreen(String searchText)
        {
         
            InitializeComponent();
            chServ = new ChapterService(ctx);
            langServ = new LangService(ctx);
            lessonServ = new LessonService(ctx);
            viewServ = new ViewerService(ctx);

            _searchText = searchText;
            textBox2.Text = _searchText;
            button7.PerformClick();
        }
        DataTable dt = new DataTable();
       public  string fileName;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                MessageBox.Show(((DataTable)senderGrid.DataSource).Rows[0][0].ToString());//
                 fileName = ((DataTable)senderGrid.DataSource).Rows[0][0].ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DirectoryInfo rootDir = new DirectoryInfo(IoService.pathTodirectory);
            WalkDirectoryTree(rootDir);
            dataGridView2.DataSource = dt;

        }

        public void matchSearch(string filename)
        {
            dt.Rows.Clear();
            dt.Columns.Clear();

            Dictionary<string, string> found = new Dictionary<string, string>();
            string line;
            bool textFound = false;
            using (StreamReader file = new StreamReader(filename))
            {

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(textBox2.Text))
                    {
                        found.Add(line, filename);
                        dt.Columns.Add("filename");
                        dt.Rows.Add(filename);
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
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
        

        private void SearchScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
