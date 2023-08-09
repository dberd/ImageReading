using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ImageReading
{
    public partial class Form1 : Form
    {
        Bitmap srcbmp;
        Bitmap outbmp;
        Color pickedColorMin = Color.FromArgb(0, 0, 0, 0);
        Color pickedColorMax = Color.FromArgb(0, 0, 0, 0);
        public Form1()
        {
            InitializeComponent();
        }
        
        public static Bitmap ChangeColor(Bitmap scrBitmap, Color pickedColorMin, Color pickedColorMax)
        {
            Color actualColor;
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    actualColor = scrBitmap.GetPixel(i, j);
                    if ((actualColor.R >= pickedColorMin.R && actualColor.R <= pickedColorMax.R) && (actualColor.G >= pickedColorMin.G && actualColor.G <= pickedColorMax.G) && (actualColor.B >= pickedColorMin.B && actualColor.B <= pickedColorMax.B))
                        newBitmap.SetPixel(i, j, Color.Transparent);
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                    
                }
            }
            return newBitmap;
        }


        private void display(string text)
        {
            MessageBox.Show(text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            outbmp = ChangeColor(srcbmp,pickedColorMin, pickedColorMax);
            pictureBox2.Image = outbmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    srcbmp = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.Image = srcbmp;
                }
                catch (IOException)
                {
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            outbmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
            MessageBox.Show("File Saved");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) >= 0 && int.Parse(textBox2.Text) <= 255 && int.Parse(textBox3.Text) >= 0 && int.Parse(textBox4.Text) <= 255 && int.Parse(textBox5.Text) >= 0 && int.Parse(textBox6.Text) <= 255 && int.Parse(textBox1.Text) <= int.Parse(textBox2.Text) && int.Parse(textBox3.Text) <= int.Parse(textBox4.Text) && int.Parse(textBox5.Text) <= int.Parse(textBox6.Text))
            {
                pickedColorMin = Color.FromArgb(0, int.Parse(textBox1.Text), int.Parse(textBox3.Text), int.Parse(textBox5.Text));
                pickedColorMax = Color.FromArgb(0, int.Parse(textBox2.Text), int.Parse(textBox4.Text), int.Parse(textBox6.Text));
            }
            else
            {
                MessageBox.Show("Input error");
            }
            Console.WriteLine(pickedColorMin.R + " " + pickedColorMin.G + " " + pickedColorMin.B + "\n");
            Console.WriteLine(pickedColorMax.R + " " + pickedColorMax.G + " " + pickedColorMax.B + "\n");
        }
    }
}