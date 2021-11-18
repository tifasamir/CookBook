using CodeBook.models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.services
{
   public class IoService
    {
        public static string startupPath = "";
        public static string pathTodirectory = "";
        public static string MaterialLoc = "";
        public static string MainDirectory = "";
        public static string DataBaseLoc = "";
        public IoService()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings-sqlite.json")
                   .Build();
            //var ProjLoc = configuration.GetSection("ProjLoc");

            //startupPath = ProjLoc.Value;
            //pathTodirectory = ProjLoc.Value; ;
            MainDirectory = configuration.GetSection("ProjLoc").Value;
            MaterialLoc = MainDirectory + "\\Material"; 
            DataBaseLoc = MainDirectory + "\\DB\\codebook.db";
            startupPath = MaterialLoc;


        }
  
        public void  save(RichTextBox rt, String filename)
        {
            File.WriteAllText(filename, rt.Text);
            //rt.SaveFile(filename);
        }

        public String read( String filename)
        {
            String txt = File.ReadAllText(filename);
            return txt;
        }

        public void saveImage(Bitmap img, string url)
        {


            //pictureBox.Image.Save(url, ImageFormat.Png);
            if (img != null)
            {
                // var i2 = new Bitmap(img);
                img.Save(url, ImageFormat.Png);
            }
              
        }

        public Image readImage( string url)
        {
            Image  img = Image.FromFile( url);
            return img;
        }

        public string  fileNameConvention(int nextID ,string fileType ,Chapter ch)
        {
            if (fileType == "desc")      return "lang\\lang_" + ch.LangId + "\\ch_" + ch.ChapterId.ToString() + "\\less_" + (nextID - 1).ToString() + "\\txt";
            else if (fileType == "snip") return "lang\\lang_" + ch.LangId + "\\ch_" + ch.ChapterId.ToString() + "\\less_" + (nextID - 1).ToString() + "\\snippet";
            else if (fileType == "img")  return "lang\\lang_" + ch.LangId + "\\ch_" + ch.ChapterId.ToString() + "\\less_" + (nextID - 1).ToString() + "\\img";
            else
            {
                return "lang\\lang_" + ch.LangId + "\\ch_" + ch.ChapterId.ToString()+"\\less_" + (nextID - 1).ToString();
            }
        }

        //public string getStartFolder()
        //{
        //    return startupPath;

        //}

        public void removeRelatedFiles(Lesson l)
        {
            List<Viewer> vv = l.Views;
            if(vv!= null && vv.Count > 0)
            {
                foreach (Viewer item in vv)
                {
                    removeFileIfExist(IoService.MaterialLoc + "\\"+    item.fileurl);
                    removeFileIfExist(IoService.MaterialLoc + "\\" + item.snippeturl);
                    removeFileIfExist(IoService.MaterialLoc + "\\" + item.imageurl);
                }
            }
        }

        public void removeFileIfExist(string v)
        {
            try
            {
            if (File.Exists(v))
                        {
                            File.Delete(v);
                        }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        internal string getMaterialFolder()
        {
            return MaterialLoc;
        }
    }
}
