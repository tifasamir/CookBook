using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeBook.Dialog
{
    public partial class ImageViewer : Form
    {
        public ImageViewer(Image img)
        {
            InitializeComponent();

            pictureBox1.Image = img;
        }

        private void ImageViewer_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
