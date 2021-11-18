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
        private void refreshlistBox1()
        {
            listBox1.Items.Clear();
            listBox1.DataItems.Clear();
            listBox1.listType = "";
        }
        private void refreshlistBox2()
        {
            listBox2.Items.Clear();
            listBox2.DataItems.Clear();
            listBox2.listType = "";
        }
        private void refreshlistBox3()
        {
            listBox3.Items.Clear();
            listBox3.DataItems.Clear();
            listBox3.listType = "";
            refreshViewer();
        }
        private void refreshViewer()
        {
            vv.Clear();
            currentPage = null;
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.Image = null;
            //pagination
            label9.Text = "0";
            label8.Text = "0";
            //errors
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
        }



    }
}