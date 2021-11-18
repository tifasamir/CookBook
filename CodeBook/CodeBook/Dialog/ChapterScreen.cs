using CodeBook.db;
using CodeBook.models;
using CodeBook.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.Dialog
{
    public partial class ChapterScreen : Form
    {
        ChapterService chServ;
        LangService langServ;
        public ChapterScreen(CodeBookContext ctx , int selectedLang)
        {
            InitializeComponent();
            this.CenterToScreen();
            chServ = new ChapterService(ctx);
            langServ = new LangService(ctx);

            var langs = langServ.findAll();
            comboBox1.DataSource = langs;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "LangId";
            if (selectedLang != null)
            {
                var lang = langs.Where(w => w.LangId == selectedLang).FirstOrDefault();
                comboBox1.SelectedItem = lang;
                //comboBox1.SelectedValue = selectedLang;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Chapter ch = new Chapter();
            ch.name = textBox1.Text;
            ch.Lang = (Lang) comboBox1.SelectedItem;
            chServ.save(ch);
            this.Close();
        }

        private void ChapterScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
