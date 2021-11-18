using CodeBook.Configs;
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
    public partial class Form1 : Form
    {
        CodeBookContext ctx = new CodeBookContext();
        ChapterService chServ;
        LangService langServ;
        LessonService lessonServ;
        ViewerService viewServ;
        SettingService settingServ;

        Lang selectedLang;
        Lesson selectedLesson;
        Chapter selectedChapter;
        Viewer currentPage;
        List<Viewer> vv = new List<Viewer>();



        //int currentIndex = 0;
        IoService ioServ = new IoService();
        ContextMenuService cMenuServ;

        public Form1()
        {
            InitializeComponent();
            chServ = new ChapterService(ctx);
            langServ = new LangService(ctx);
            lessonServ = new LessonService(ctx);
            viewServ = new ViewerService(ctx);
            settingServ = new SettingService(ctx);
            cMenuServ = new ContextMenuService(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            loadBook();
            //console
            updateWorkinDir();

            loadConfigs();
            PrepareToolTip();
        }

        private void loadConfigs()
        {
            ShortCutsConfig x = new ShortCutsConfig();
            propertyGrid1.SelectedObject = x;
            NoteConfigs xx = new NoteConfigs();
            propertyGrid2.SelectedObject = xx;
        }

        private void loadBook()
        {
            reloadLang();
        }



        public void showAddNewChapter(object sender, EventArgs e)
        {
            if (listBox1?.SelectedItems.Count > 0)
            {
                var index = listBox1.SelectedIndex;
                ChapterScreen frm;

               Lang l = (Lang) listBox1.DataItems[index];

                var id = ((Lang) (l)).LangId;
                frm = new ChapterScreen(ctx, id);

                frm.Show();
                frm.FormClosed += new FormClosedEventHandler(reloadChapterList);



            }
            else
            {
                MessageBox.Show("select Lang To Add To ?");
            }


        }

        public void showAddNewLesson(object sender, EventArgs e)
        {
            if (listBox2?.SelectedItems.Count > 0)
            {
                var index = listBox2.SelectedIndex;
                Chapter chap = (Chapter)listBox2.DataItems[index];

              
                Dictionary<string, Object> options = new Dictionary<string, Object>();
                options.Add("mode", "add");
           //     Lesson cLess = (Lesson)((MyListViewItem)langFocusItem).Data;
           //     options.Add("Lesson", cLess);

                LessonScreen frm = new LessonScreen(ctx, chap, options);
                frm.Show();
                frm.FormClosed += new FormClosedEventHandler(reloadLessonList);

            }
            else
            {
                MessageBox.Show("select Chapter To Add Lesson To ?");
            }

        }

     


        public  void showAddNewLang(object sender, EventArgs e)
        {
            LangScreen frm = new LangScreen(ctx);
            frm.Show();
            frm.FormClosed += new FormClosedEventHandler(reloadLangList);
        }





        //Delete
        private void button15_Click(object sender, EventArgs e)
        {
            CascadingDeleteLang();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case (Keys.Q):
                        // Simulate clicks on button1.
                        button12.PerformClick();
                        break;

                    case Keys.W:

                        // Simulates clic on button2.
                        button14.PerformClick();
                        break;

                    case Keys.E:
                        // Simulates clic on button2.
                        button13.PerformClick();
                        break;



                    default:
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        button5.PerformClick();
                        break;
                    default:
                        break;
                }
            }
        }

       

        public void refreshBedoreViewer()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.Image = null;
            //pagination
            //label9.Text = "0";
            //label8.Text = "0";
            //errors
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";


        }
        //copy
        private void button9_Click(object sender, EventArgs e)
        {
            copy(richTextBox2);
        }

        public void copy(Control r)
        {
            Clipboard.SetText(r.Text);

        }
        //copy code
        private void button8_Click(object sender, EventArgs e)
        {
            copy(richTextBox1);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                ImageViewer frm = new ImageViewer(pictureBox1.Image);
                frm.Show();
            }
            
        }
        //upload to edit
        private void button20_Click(object sender, EventArgs e)
        {

              openFileDialog1.ShowDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Image Files";
            openFileDialog1.DefaultExt = "png";
            openFileDialog1.Filter = "png (*.png)|*.png|All files (*.*)|*.*";

           
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
        }
        //edit image
        private void button21_Click(object sender, EventArgs e)
        {
            Viewer v = currentPage;

            //ioServ.save(richTextBox1, IoService.startupPath + "\\" + v.fileurl);

            //ioServ.save(richTextBox2, IoService.startupPath + "\\" + v.snippeturl);
          
            if (openFileDialog1.FileName != "")
            {
                try
                {
                    if (v.imageurl != "")
                    {
                        int maxV = viewServ.getMax();
                        string maxv_s = maxV.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                        string filePathImg = ioServ.fileNameConvention(v.LessonId + 1, "img", lessonServ.find(v.LessonId).Chapter);
                        string fileNameImg = "img_" + maxv_s.ToString() + ".png";
                        string pathUmg = ioServ.getMaterialFolder() + "\\" + filePathImg;
                        if (!Directory.Exists(pathUmg)) Directory.CreateDirectory(pathUmg);
                        Bitmap img = new Bitmap(openFileDialog1.FileName);

                        v.imageurl = filePathImg + "\\" + fileNameImg;
                   
                        pictureBox1.Image.Save(IoService.startupPath + "\\" + v.imageurl);
                        openFileDialog1.FileName = "";
                    }  else { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //edit
        private void button18_Click(object sender, EventArgs e)
        {
            Viewer v = currentPage;

            ioServ.save(richTextBox1, IoService.startupPath + "\\" + v.fileurl);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Viewer v = currentPage;

            ioServ.save(richTextBox2, IoService.startupPath + "\\" + v.snippeturl);
        }

        //copy selected
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox2.SelectedRtf, TextDataFormat.Rtf);
            }catch(Exception ex)
            {

            }
        
        }
        public void PrepareToolTip()
        {
            toolTip1.SetToolTip(button6, "Copy Selectd Code");
            toolTip1.SetToolTip(button9, "Copy");
            toolTip1.SetToolTip(button20, "Upload a Photo");

            toolTip1.SetToolTip(button8, "Copy Text");
            toolTip1.SetToolTip(button18, "Edit and Save File");
            toolTip1.SetToolTip(button19, "Edit and Save Snippet");
            toolTip1.SetToolTip(button21, "Edit and Save Photo");
        }

      










        /////  search
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    SearchScreen frm = new SearchScreen(textBox1.Text);
        //    frm.Show();
        //    frm.FormClosed += new FormClosedEventHandler(LoadSearechedLesson);

        //}






        //private void listBox3_MouseClick(object sender, MouseEventArgs e)
        //{
        //    //if (e.Button == MouseButtons.Right)
        //    //{
        //    //    var langFocusItem = listBox3.SelectedItems[0];
        //    //    if (langFocusItem != null && langFocusItem.Bounds.Contains(e.Location))
        //    //    {
        //    //        contextMenuStrip1 = cMenuServ.showContext((MyListViewItem)langFocusItem);
        //    //        contextMenuStrip1.Show(Cursor.Position);
        //    //    }
        //    //}

        //}


        //private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (listView1.SelectedItems.Count > 0)
        //    //{
        //    //    listView2.Items.Clear();
        //    //    refreshListView3();

        //    //    int LangId = ((Lang)((MyListViewItem)listView1.SelectedItems[0]).Data).LangId;

        //    //    reloadChapterForLang(LangId);
        //    //}

        //}

        //private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //if (listView2.SelectedItems.Count > 0)
        //    //{
        //    //    listView3.Items.Clear();


        //    //    int chId = ((Chapter)((MyListViewItem)listView2.SelectedItems[0]).Data).ChapterId ?? default(int);

        //    //    reloadLessonsForCh(chId);
        //    //}
        //}

        //private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (listView3.SelectedItems.Count > 0)
        //    {
        //        try
        //        {
        //            int chId = ((Lesson)((MyListViewItem)listView3.SelectedItems[0]).Data).LessonId;

        //            reloadListViewForLesson(chId);
        //            viewPages();
        //        }catch(Exception ex)
        //        {

        //        }

        //    }
        //}



        //public void refreshViewer()
        //{
        //    richTextBox1.Text = "";
        //    richTextBox2.Text = "";
        //    pictureBox1.Image = null;
        //    //pagination
        //    label9.Text = "0";
        //    label8.Text = "0";
        //    //errors
        //    label11.Text = "";
        //    label12.Text = "";
        //    label13.Text = "";


        //}



    }
}

     
    


