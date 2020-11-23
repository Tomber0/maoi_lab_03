using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maoi_lab_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "openToolStripButton":
                    UploadImage();
                    break;
                default:
                    break;
            }

        }
        public ImageMatrix mainMatrix { get; set; }
        private void UploadImage()
        {
            string filePath;
            Image image;
            DialogResult dialogRes = openFileDialog1.ShowDialog();
            if (CheckDialogResult(dialogRes))
            {
                filePath =
                openFileDialog1.FileName;
                image = new Bitmap(filePath);
                mainMatrix = new ImageMatrix(image);
                //transforms to halftone
                pictureBox1.Image = mainMatrix.LocalImage;
                this.Text = $"Maoi_zayicev_4 {openFileDialog1.SafeFileName}";
            }
        }
        private bool CheckDialogResult(DialogResult result)
        {
            switch (result)
            {
                case DialogResult.None:
                    return false;
                case DialogResult.OK:
                    return true;
                case DialogResult.Cancel:
                    return false;
                case DialogResult.Abort:
                    return false;
                case DialogResult.Retry:
                    return false;
                case DialogResult.Ignore:
                    return false;
                case DialogResult.Yes:
                    return true;
                case DialogResult.No:
                    return false;
                default:
                    return false;
            }


        }

    }
}

