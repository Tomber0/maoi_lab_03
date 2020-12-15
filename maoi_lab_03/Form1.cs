using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public List<Perceptron> Perceptrons { get; set; }
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
                dataGridView2.RowCount = image.Height;
                dataGridView2.ColumnCount = image.Width;

                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        dataGridView2.Columns[j].Width = 25;
                        dataGridView2.Rows[i].Cells[j].Value = mainMatrix.BinaryMatrix[j][i];
                        dataGridView2.Rows[i].Cells[j].Style.BackColor = mainMatrix.ColorMatrix[j][i];
                        dataGridView2.Rows[i].Cells[j].Style.ForeColor = Color.FromArgb(mainMatrix.ColorMatrix[j][i].ToArgb() ^ 0xffffff);

                    }
                }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            /*            for (int i = 0; i < 100*100; i++)
                        {
                            dataGridView1.Rows[i]
                        }
            */

            //mainMatrix = new ImageMatrix();



            //perceptron = new Perceptron(5, 5, 5, mainMatrix);

            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 10;

        }
        Perceptron perceptron;


        private void button1_Click(object sender, EventArgs e)
        {
            int A = 1000;
            int x = 100;
            int y = 100;

            perceptron = new Perceptron(A, x, y);
            List<string> namesI = new List<string>();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                namesI.Add(comboBox1.Items[i].ToString());
            }
            perceptron.FixNames(namesI.ToArray());

            //ifofog
/*            dataGridView1.RowCount = x * y + 1;
            dataGridView1.ColumnCount = A;

*/            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = A;

            dataGridView4.RowCount = 1;
            dataGridView4.ColumnCount = A;

            dataGridView5.RowCount = 1;
            dataGridView5.ColumnCount = A;

            for (int i = 0; i < dataGridView3.ColumnCount; i++)
            {
                dataGridView3.Columns[i].Width = 30;
            }
            for (int i = 0; i < dataGridView4.ColumnCount; i++)
            {
                dataGridView4.Columns[i].Width = 30;
            }
            for (int i = 0; i < dataGridView5.ColumnCount; i++)
            {
                dataGridView5.Columns[i].Width = 30;
            }
            if (checkBox1.Checked)
            {
                dataGridView1.RowCount = x * y + 1;
                dataGridView1.ColumnCount = A;


                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {

                        dataGridView1.Columns[j].Width = 25;

                        dataGridView1.Rows[i].Cells[j].Value = perceptron.ConnectionArray[j, i].ToString();
                    }
                }
            }
            else 
            {
                dataGridView1.ColumnCount = A;
                dataGridView1.RowCount = 1;
            }
            /*            AddRow("y");

                        //ввод y


                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        for (int i = 0; i < dataGridView1.ColumnCount; i++)
                        {
                            row.Cells[i].Value = perceptron.Yi[i];
                        }
                        dataGridView1.Rows.Add(row);
            */
            AddRow();

            // ввод лямбд
            for (int j = 0; j < perceptron.Lambdas.Count; j++)
            {
                DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row2.HeaderCell.Value = $"lambda_ {comboBox1.Items[j]}";

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].HeaderText = $"{i + 1}";

                    row2.Cells[i].Value = perceptron.Lambdas.ToArray()[j][i];
                }
                dataGridView1.Rows.Add(row2);
            }


        }
        private void AddRow(string val = "-")
        {
            DataGridViewRow rows = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                rows.Cells[i].Value = val;
            }
            dataGridView1.Rows.Add(rows);


        }
        private void AImage()
        {
            /*perceptron.ChangeImage(mainMatrix);
            perceptron.Proceede(comboBox1.SelectedIndex);

            //AddRow("y");

            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                row.HeaderCell.Value = $"Y";

                row.Cells[i].Value = perceptron.Yi[i];
            }
            dataGridView1.Rows.Add(row);


            for (int j = 0; j < perceptron.Lambdas.Count; j++)
            {
                //AddRow($"lambda_ {comboBox1.Items[1]}");
                DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row2.HeaderCell.Value = $"lambda_ {comboBox1.Items[j]}";
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    row2.Cells[i].Value = perceptron.Lambdas.ToArray()[j][i];
                }
                dataGridView1.Rows.Add(row2);
            }*/



            perceptron.ChangeImage(mainMatrix);
            perceptron.Proceede(comboBox1.SelectedIndex);

            //AddRow("y");

            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                row.HeaderCell.Value = $"Y";

                row.Cells[i].Value = perceptron.Yi[i];
            }
            dataGridView1.Rows.Add(row);


            for (int j = 0; j < perceptron.Lambdas.Count; j++)
            {
                //AddRow($"lambda_ {comboBox1.Items[1]}");
                DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row2.HeaderCell.Value = $"lambda_ {comboBox1.Items[j]}";
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    row2.Cells[i].Value = perceptron.Lambdas.ToArray()[j][i];
                }
                dataGridView1.Rows.Add(row2);
                //AddRow();
            }

            DataGridViewRow row3 = (DataGridViewRow)dataGridView3.Rows[0].Clone();
            for (int i = 0; i < dataGridView3.ColumnCount; i++)
            {
                row3.Cells[i].Value = perceptron.Lambdas.ToArray()[0][i];
            }
            dataGridView3.Rows.Add(row3);

            DataGridViewRow row4 = (DataGridViewRow)dataGridView4.Rows[0].Clone();
            for (int i = 0; i < dataGridView4.ColumnCount; i++)
            {
                row4.Cells[i].Value = perceptron.Lambdas.ToArray()[1][i];
            }
            dataGridView4.Rows.Add(row4);

            DataGridViewRow row5 = (DataGridViewRow)dataGridView5.Rows[0].Clone();
            for (int i = 0; i < dataGridView5.ColumnCount; i++)
            {

                row5.Cells[i].Value = perceptron.Lambdas.ToArray()[2][i];
            }
            dataGridView5.Rows.Add(row5);


        }
        private void button2_Click(object sender, EventArgs e)
        {
            AImage();
            /*
                        perceptron.ChangeImage(mainMatrix);
                        perceptron.Proceede(comboBox1.SelectedIndex);

                        //AddRow("y");

                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        for (int i = 0; i < dataGridView1.ColumnCount; i++)
                        {
                            row.HeaderCell.Value = $"Y";

                            row.Cells[i].Value = perceptron.Yi[i];
                        }
                        dataGridView1.Rows.Add(row);


                        for (int j = 0; j < perceptron.Lambdas.Count; j++)
                        {
                            //AddRow($"lambda_ {comboBox1.Items[1]}");
                            DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                            row2.HeaderCell.Value = $"lambda_ {comboBox1.Items[j]}";
                            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                            {
                                row2.Cells[i].Value = perceptron.Lambdas.ToArray()[j][i];
                            }
                            dataGridView1.Rows.Add(row2);
                            //AddRow();
                        }

                        DataGridViewRow row3 = (DataGridViewRow)dataGridView3.Rows[0].Clone();
                        for (int i = 0; i < dataGridView3.ColumnCount; i++)
                        {
                            row3.Cells[i].Value = perceptron.Lambdas.ToArray()[0][i];
                        }
                        dataGridView3.Rows.Add(row3);

                        DataGridViewRow row4 = (DataGridViewRow)dataGridView4.Rows[0].Clone();
                        for (int i = 0; i < dataGridView4.ColumnCount; i++)
                        {
                            row4.Cells[i].Value = perceptron.Lambdas.ToArray()[1][i];
                        }
                        dataGridView4.Rows.Add(row4);

                        DataGridViewRow row5 = (DataGridViewRow)dataGridView5.Rows[0].Clone();
                        for (int i = 0; i < dataGridView5.ColumnCount; i++)
                        {

                            row5.Cells[i].Value = perceptron.Lambdas.ToArray()[2][i];
                        }
                        dataGridView5.Rows.Add(row5);
            */
        }


        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;    // <<this line will help you

        }
        private void dataGridView3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;    // <<this line will help you

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            AddRow();
            perceptron.ChangeImage(mainMatrix);
            perceptron.FindImage();

            //AddRow("y");

            /*            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        for (int i = 0; i < dataGridView1.ColumnCount; i++)
                        {
                            row.HeaderCell.Value = $"Y";

                            row.Cells[i].Value = perceptron.Yi[i];
                        }
                        dataGridView1.Rows.Add(row);
            */

            for (int j = 0; j < perceptron.Lambdas.Count; j++)
            {
                //AddRow($"lambda_ {comboBox1.Items[1]}");
                DataGridViewRow row2 = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row2.HeaderCell.Value = $"lambda_ {comboBox1.Items[j]}";
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    row2.Cells[i].Value = perceptron.Lambdas.ToArray()[j][i];
                }
                dataGridView1.Rows.Add(row2);
                //AddRow();
            }
            label5.Text = perceptron.CurrImg;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Суммы: Л1 = {perceptron.Sums[0]},Л2 = {perceptron.Sums[1]},Л3 = {perceptron.Sums[2]}, ");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenDir();
        }
        private void OpenDir()
        {
            //DialogResult dialogRes = openFileDialog1.ShowDialog();
            DialogResult dialogRes = folderBrowserDialog1.ShowDialog();
            if (CheckDialogResult(dialogRes))
            {
                string filePath;
                filePath =
                folderBrowserDialog1.SelectedPath;

                string[] files = Directory.GetFiles(filePath);

                foreach (var file in files)
                {
                    var image = new Bitmap(file);
                    mainMatrix = new ImageMatrix(image);
                    AImage();

                }
            }

        }

        private void dataGridView3_ColumnAdded_1(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;    // <<this line will help you

        }

        private void dataGridView4_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;    // <<this line will help you

        }

        private void dataGridView5_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 10;    // <<this line will help you

        }
    }
}

