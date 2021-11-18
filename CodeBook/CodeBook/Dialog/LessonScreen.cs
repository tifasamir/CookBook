using CodeBook.db;
using CodeBook.models;
using CodeBook.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.Dialog
{
    public partial class LessonScreen : Form
    {
        ChapterService chServ;
        List<Viewer> vv = new List<Viewer>();
        IoService ioServ = new IoService();
        LessonService lessServ;
        ViewerService viewServ;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string _mode = "add";
        Dictionary<string, Object> _options;

        Lesson currentLesson ;
        public LessonScreen(CodeBookContext ctx, Chapter selectedChap ,Dictionary<string, Object> options)
        {
            _options = options;
               _mode = options["mode"].ToString();
            if(options.ContainsKey("Lesson") && options["Lesson"]!=null) currentLesson = (Lesson)options["Lesson"];



            InitializeComponent();
            chServ = new ChapterService(ctx);
            lessServ = new LessonService(ctx);
            viewServ = new ViewerService(ctx);

            var chps = chServ.findAll();
            comboBox1.DataSource = chps;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "ChapterId";

            if (_mode == "add")
            {
                if (selectedChap != null)
                {
                    var ch = chps.Where(w => w.LangId == selectedChap.LangId).FirstOrDefault();
                    comboBox1.SelectedItem = selectedChap;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                }
            }
            else if (_mode == "edit")
            {
                var chapter = chServ.find(currentLesson.ChapterId);
                comboBox1.SelectedItem = chapter;
                LoadData();
            }
           


            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void LoadData()
        {
            if (_mode == "edit")
            {
                vv = viewServ.findViewerByLessId(currentLesson.LessonId);
                
                textBox1.Text = currentLesson.name;
                if (vv.Count > 0)
                {
                  //  loadView(vv[0]);
                    loadListBox(vv);
                }

            }
        }
        public void loadView(Viewer cc)
        {

               try
            {
               
                try
                {
                    richTextBox1.Text = ioServ.read(IoService.startupPath + "\\" + cc.fileurl);
                }
                catch (Exception ex) { }
                try
                {
                    richTextBox2.Text = ioServ.read(IoService.startupPath + "\\" + cc.snippeturl);
                }
                catch (Exception ex) { }
                try
                {
                    if (cc.imageurl != ""&&cc.imageurl!=null)
                    {
                        pictureBox1.Image = ioServ.readImage(IoService.startupPath + "\\" + cc.imageurl);

                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }
        }
        //add Page Will cach data to files in the program 
        private void button3_Click(object sender, EventArgs e)
        {
            addNewPage();
          
        }

        private void addNewPage()
        {
            int max = lessServ.getMax();
            int maxV = viewServ.getMax();
            ;
            String maxv_s = maxV.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string filePathTxt = ioServ.fileNameConvention(max + 1, "desc", ((Chapter)comboBox1.SelectedItem));
            string fileName = "desc_" + maxv_s.ToString() + ".txt";
            string path = ioServ.getMaterialFolder() + "\\" + filePathTxt;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            ioServ.save(richTextBox1, path + "\\" + fileName);
            maxv_s = maxV.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string filePathSnip = ioServ.fileNameConvention(max + 1, "snip", ((Chapter)comboBox1.SelectedItem));
            string fileNameSnip = "snip_" + maxv_s + ".txt";
            string pathSnip = ioServ.getMaterialFolder() + "\\" + filePathSnip;
            if (!Directory.Exists(pathSnip)) Directory.CreateDirectory(pathSnip);

            ioServ.save(richTextBox2, pathSnip + "\\" + fileNameSnip);


            maxv_s = maxV.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string filePathImg = ioServ.fileNameConvention(max + 1, "img", ((Chapter)comboBox1.SelectedItem));
            string fileNameImg = "img_" + maxv_s.ToString() + ".png";
            string pathUmg = ioServ.getMaterialFolder() + "\\" + filePathImg;
            if (!Directory.Exists(pathUmg)) Directory.CreateDirectory(pathUmg);



            Viewer v = new Viewer();
            v.snippeturl = filePathSnip + "\\" + fileNameSnip;
            v.fileurl = filePathTxt + "\\" + fileName;


            if (openFileDialog1.FileName != "")
            {
                try
                {
                    //string imageURL = @"C:\Users\Mostafa Eltayeb\OneDrive\Pictures\242507088_4588116914587728_5025232472037994598_n.jpg";
                    //ioServ.saveImage(img, pathUmg + "\\" + fileNameImg);
                    v.imageurl = filePathImg + "\\" + fileNameImg;
                    Bitmap img = new Bitmap(openFileDialog1.FileName);
             //       MessageBox.Show(pathUmg + "\\" + fileNameImg);
                    img.Save(pathUmg + "\\" + fileNameImg);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {

            }


            //ioServ.save(richTextBox2,
            // ioServ.fileNameConvention(max + 1, "snip", ((Chapter)comboBox1.SelectedItem)));

            //ioServ.saveImage(pictureBox1,
            //             ioServ.fileNameConvention(max + 1, "img", ((Chapter)comboBox1.SelectedItem)) );


            //  v.image = pictureBox1.Image;

            vv.Add(v);
            loadListBox(vv);
            //  listBox1.Items.Add(vv.Count);

            refreshView();
        }

        private void refreshView()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.Image = null;
            openFileDialog1.FileName ="";
            loadListBox(vv);
        }

        public void loadListBox(List<Viewer>vv)
        {   listBox1.Items.Clear();
            int x=0;
            x = vv.Count;
           
            for (int i = 1; i <= x; i++)
            {
                listBox1.Items.Add(i);
            }

        }

    

        //ADD Lessons
        //Save
        private void button1_Click(object sender, EventArgs e)
        {
            if (_mode == "add")
            {
                addLesson();
            }else if (_mode == "edit")
            {
                editLesson();
            }
            
         
        }
        public void addLesson()
        {
            if (textBox1.Text == "") MessageBox.Show("Please Add Lesson Title");
            else
            {
                Lesson l = new Lesson();
                this.CenterToScreen();
                l.Chapter = (Chapter)comboBox1.SelectedItem;
                l.name = textBox1.Text;
                if (richTextBox1.Text != "" || richTextBox2.Text != "")
                {
                    addNewPage();
                }
                l.Views = vv;
                lessServ.save(l);
                this.Close();
            }
        }

        public void editLesson()
        {
            if (textBox1.Text == "") MessageBox.Show("Please Add Lesson Title");
            else
            {
                Lesson l = currentLesson;
                this.CenterToScreen();
                l.Chapter = (Chapter)comboBox1.SelectedItem;
                l.name = textBox1.Text;
                if (richTextBox1.Text != "" || richTextBox2.Text != "")
                {
                    addNewPage();
                }
                l.Views = vv;
                lessServ.update(l);
                this.Close();
            }
        }
        private void LessonScreen_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Viewer cc = vv[listBox1.SelectedIndex];
                loadView(cc);
            }
                
            catch(System.ArgumentOutOfRangeException ex)
            {

            }


        }
        //edit
        private void button5_Click(object sender, EventArgs e)
        {
            editViewer();
        }

        private void editViewer()
        {
            Viewer v = vv[listBox1.SelectedIndex];

            try {
                ioServ.save(richTextBox1, IoService.startupPath + "\\" + v.fileurl);
            }
            catch (Exception ex) { }
            try {
                ioServ.save(richTextBox1, IoService.startupPath + "\\" + v.fileurl);
            }
            catch (Exception ex) { }
            try {

                ioServ.save(richTextBox2, IoService.startupPath + "\\" + v.snippeturl);

            }
            catch (Exception ex) { }
            



            if (openFileDialog1.FileName != "")
            {
                try
                {
                    if (v.imageurl != "")
                    {
                        int maxV = viewServ.getMax();
                        string maxv_s = maxV.ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                     
                        string filePathImg = ioServ.fileNameConvention(v.LessonId + 1, "img", ((Chapter)comboBox1.SelectedItem));
                        string fileNameImg = "img_" + maxv_s.ToString() + ".png";
                        string pathUmg = ioServ.getMaterialFolder() + "\\" + filePathImg;
                        if (!Directory.Exists(pathUmg)) Directory.CreateDirectory(pathUmg);
                        Bitmap img = new Bitmap(openFileDialog1.FileName);

                        v.imageurl = filePathImg + "\\" + fileNameImg;
                        //       MessageBox.Show(pathUmg + "\\" + fileNameImg);
                     //   img.Save(pathUmg + "\\" + fileNameImg);
                        //     ioServ.saveImage(img, IoService.startupPath + "\\" + v.imageurl);
                        //v.imageurl = IoService.startupPath + "\\" + v.imageurl;
                        pictureBox1.Image.Save(IoService.startupPath + "\\" + v.imageurl);
                        openFileDialog1.FileName = "";
                    }
                    else
                    {

                    }

                
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
            else
            {
                try
                {
                    if (v.imageurl != "" && pictureBox1.Image!=null)
                    {
                        pictureBox1.Image.Save(IoService.startupPath + "\\" + v.imageurl);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //  
                //v.imageurl = IoService.startupPath + "\\" + v.imageurl;
            }

            vv[listBox1.SelectedIndex] = v;
            refreshView();
        }
        ///delete
        private void button4_Click(object sender, EventArgs e)
        {
            deleteViewer();
        }

        private void deleteViewer()
        {

            var x = listBox1.SelectedIndex;
            Viewer v = vv[listBox1.SelectedIndex];


            ioServ.removeFileIfExist(IoService.startupPath + "\\" + v.fileurl);
            ioServ.removeFileIfExist(IoService.startupPath + "\\" + v.snippeturl);
            ioServ.removeFileIfExist(IoService.startupPath + "\\" + v.imageurl);

            //  ioServ.removeRelatedFiles(richTextBox1, IoService.startupPath + "\\" + v.fileurl);






            vv.Remove(v);
            listBox1.Items.Remove(x);
        
            refreshView();
        }

        //test Save Image To Desk

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save(@"C:\Users\Mostafa Eltayeb\Desktop\myimg.jpg");
        }


        //clear phpotp
        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            openFileDialog1.FileName = "";
        }

        //Upload new photo
        private void button20_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Image Files";
            openFileDialog1.DefaultExt = "png";
            openFileDialog1.Filter = "png (*.png)|*.png|All files (*.*)|*.*";

            //   textBox1.Text = openFileDialog1.FileName;
            if (openFileDialog1.FileName != "")
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }

        }

        public void PrepareToolTip()
        {
            toolTip1.SetToolTip(button2, "Clear All unsaved Text");
            toolTip1.SetToolTip(button16, "Clear Photo");
            toolTip1.SetToolTip(button20, "Upload a Photo");

        }

        // refresh views 
        private void button2_Click(object sender, EventArgs e)
        {
            refreshView();
        }
    }
}
