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
        private void reloadListViewForLesson(int lessonId)
        {
            vv.Clear();

            Lesson ll = lessonServ.find(lessonId);
            List<Viewer> sVV = viewServ.findViewerByLessId(ll.LessonId); ;
            vv = sVV;
            if (vv.Count > 0)
            {
                currentPage = vv[0];
                settingServ.update("sView", currentPage.ViewerId);
            }
            else
            {
                //vv = new List<Viewer>();
            }
            //if (vv.Count > 0) currentPage = vv[0];
            //else currentPage = null;


        }
        private void viewPages()
        {
            try
            {
                if (currentPage == null)
                {
                    currentPage = viewServ.find(int.Parse(settingServ.find(4).value));
                }
                if (currentPage != null)
                {
                    var p = IoService.startupPath;
                    try
                    { richTextBox1.Text = ioServ.read(p + "\\" + currentPage.fileurl); }
                    catch (Exception ex)
                    {
                        richTextBox1.Text = "";
                    }
                    try
                    { richTextBox2.Text = ioServ.read(p + "\\" + currentPage.snippeturl); }
                    catch (Exception ex)
                    {
                        richTextBox2.Text = "";
                    }
                    try
                    {
                        if (currentPage.imageurl != "")
                            pictureBox1.Image = ioServ.readImage(IoService.startupPath + "\\" + currentPage.imageurl);
                        else pictureBox1.Image = null;
                    }
                    catch (Exception ex)
                    {
                        pictureBox1.Image = null;
                    }
                    try
                    {
                        //int index = currentPage.ViewerId;
                        //Viewer v = vv[index];
                        label9.Text = vv.Count.ToString();//total
                        if (vv.Count != 0)
                        {
                            label8.Text = (vv.IndexOf(currentPage) + 1).ToString();
                        }
                        else
                        {
                            label8.Text = (0).ToString();
                        }
                        //(vv.IndexOf(currentPage) + 1).ToString();
                    }
                    catch (Exception ex)
                    {
                        label9.Text = "0";
                        label8.Text = "0";
                    }

                }
                else
                {
                    label8.Text = (0 + 1).ToString();
                }

            }
            catch (Exception ex)
            {

            }
        }
        //Next Page
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                int i = vv.IndexOf(currentPage);
                if (i < vv.Count() && i != -1)
                {
                    var newcurrentPage = vv[i + 1];
                    currentPage = newcurrentPage;
                    viewPages();
                }
            }
            catch (Exception ex)
            {

            }


        }
        //prev page
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                int i = vv.IndexOf(currentPage);
                if (i > 0 && i != -1)
                {
                    var newcurrentPage = vv[i - 1];
                    currentPage = newcurrentPage;
                    viewPages();
                }



            }
            catch (Exception ex)
            {

            }

        }
        private void loadPage(Viewer currentPage)
        {

            try
            {
                if (currentPage == null)
                {
                    currentPage = viewServ.find(int.Parse(settingServ.find(4).value));
                }
                richTextBox1.Text = ioServ.read(IoService.startupPath + "\\" + currentPage.fileurl);
            }
            catch (Exception ex)
            {
                label12.Text = ex.Message;
            }


            try
            {
                richTextBox2.Text = ioServ.read(IoService.startupPath + "\\" + currentPage.snippeturl);
            }
            catch (Exception ex)
            {
                label13.Text = ex.Message;
            }

            try
            {
                if (currentPage.imageurl != "")
                    pictureBox1.Image = ioServ.readImage(IoService.startupPath + "\\" + currentPage.imageurl);
                else pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                label11.Text = ex.Message;
                pictureBox1.Image = null;
            }

            try
            {

                int index = currentPage.ViewerId;
                Viewer v = vv[index];

                label9.Text = vv.Count.ToString();
                //label8.Text = (vv.IndexOf(v) + 1).ToString();// "view Id : " + v.ViewerId.ToString(); ///(vv.IndexOf(v) + 1).ToString();

                label9.Text = vv.Count.ToString();//total
                if (vv.Count != 0)
                {
                    label8.Text = (vv.IndexOf(v) + 1).ToString();
                }
                else
                {
                    label8.Text = (0).ToString();
                }

            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
            }

        }

    }
}