using CodeBook.db;
using CodeBook.models;
using CodeBook.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.Dialog
{
    public partial class LangScreen : Form
    {
        LangService langServ;
        public LangScreen(CodeBookContext ctx)
        {
            langServ = new LangService(ctx);
            this.CenterToScreen();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lang l = new Lang();
            l.name = textBox1.Text;
            langServ.save(l);
            this.Close();
        }

        private void LangScreen_Load(object sender, EventArgs e)
        {
           
        }
    }
}
