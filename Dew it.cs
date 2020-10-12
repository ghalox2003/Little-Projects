using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EURO_pupille
{
    public class CentralPixel
    {
        public static Point cp1
        {
            get;
            set;
        }
        public static Point cp2
        {
            get;
            set;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            pictureBox1.Select();
        }
        int ClickCount = 0;
        private void pictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Point BasePixel1 = new Point(0, 0);
            Point BasePixel2 = new Point(0, 0);
            Graphics g;
            g = this.CreateGraphics();
            Pen myPen = new Pen(Color.Blue);
            myPen.Width = 1;
            if (e.KeyValue == (Char)Keys.E)
            {
                Console.WriteLine(ClickCount);
                if (ClickCount == 0)
                {
                    ClickCount = ClickCount + 1;
                    BasePixel1 = new Point(MousePosition.X, MousePosition.Y);
                    Console.WriteLine(BasePixel1);
                }
                else if (ClickCount == 1)
                {
                    ClickCount = ClickCount + 1;
                    BasePixel2 = new Point(MousePosition.X, MousePosition.Y);
                    Console.WriteLine(BasePixel2);
                    g.DrawLine(myPen, BasePixel1, BasePixel2);
                    pictureBox1.SendToBack();
                }
            }
            CentralPixel cp = new CentralPixel();
            CentralPixel.cp1 = BasePixel1;
            CentralPixel.cp2 = BasePixel2;
        }
        string fileGet = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileGet = ofd.FileName;
                Bitmap bit = new Bitmap(fileGet);
                pictureBox1.Image = bit;

            }
        }

        private void cursor_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Size = new System.Drawing.Size(ClientSize.Width, ClientSize.Height);
            pictureBox1.Location = new System.Drawing.Point(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(fileGet);
            CentralPixel cp = new CentralPixel();
            Color black = System.Drawing.Color.Black;
            Point currentPixel1 = CentralPixel.cp1;
            Point currentPixel2 = CentralPixel.cp2;
            Color currentColor1 = bit.GetPixel(CentralPixel.cp1.X, CentralPixel.cp1.Y);
            Color currentColor2 = bit.GetPixel(CentralPixel.cp2.X, CentralPixel.cp2.Y);
            int PixelCount = 0;
            while (currentColor1.R >= 10)                                                           //left
            {       
                currentColor1 = bit.GetPixel(currentPixel1.X, currentPixel1.Y);
                while (bit.GetPixel(currentPixel1.X, (currentPixel1.Y+1)).R >= 10)
                {
                    bit.SetPixel(currentPixel1.X, currentPixel1.Y, black);
                    currentPixel1.Y = currentPixel1.Y + 1;
                }
                currentPixel1 = CentralPixel.cp2;
                while (bit.GetPixel(currentPixel1.X, (currentPixel1.Y-1)).R >= 10)
                {
                    bit.SetPixel(currentPixel1.X, currentPixel1.Y, black);
                    currentPixel1.Y = currentPixel1.Y - 1;
                }
                currentPixel1 = CentralPixel.cp2;
                currentPixel1.X = currentPixel1.X - 1;
                PixelCount++;
                CentralPixel.cp1 = currentPixel1;
            }
            currentPixel1 = CentralPixel.cp2;
            currentPixel1.X = currentPixel1.X + PixelCount;
            PixelCount = 0;
            CentralPixel.cp2 = currentPixel1;
            while (currentColor1.R >= 10)                                                           //right
            {
                currentColor1 = bit.GetPixel(currentPixel1.X, currentPixel1.Y);
                while (bit.GetPixel(currentPixel1.X, (currentPixel1.Y + 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel1.X, currentPixel1.Y, black);
                    currentPixel1.Y = currentPixel1.Y + 1;
                }
                currentPixel1 = CentralPixel.cp2;
                while (bit.GetPixel(currentPixel1.X, (currentPixel1.Y - 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel1.X, currentPixel1.Y, black);
                    currentPixel1.Y = currentPixel1.Y - 1;
                }
                currentPixel1 = CentralPixel.cp2;
                currentPixel1.X = currentPixel1.X + 1;
                CentralPixel.cp1 = currentPixel1;
            }
            currentPixel1 = CentralPixel.cp2;
            currentPixel1.X = currentPixel1.X + PixelCount;
            PixelCount = 0;
            CentralPixel.cp2 = currentPixel1;
            while (currentColor2.R >= 10)                                                           //left2
            {
                currentColor2 = bit.GetPixel(currentPixel2.X, currentPixel2.Y);
                while (bit.GetPixel(currentPixel2.X, (currentPixel2.Y + 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel2.X, currentPixel2.Y, black);
                    currentPixel2.Y = currentPixel2.Y + 1;
                }
                currentPixel2 = CentralPixel.cp2;
                while (bit.GetPixel(currentPixel2.X, (currentPixel2.Y - 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel2.X, currentPixel2.Y, black);
                    currentPixel2.Y = currentPixel2.Y - 1;
                }
                currentPixel2 = CentralPixel.cp2;
                currentPixel2.X = currentPixel2.X + 1;
                CentralPixel.cp2 = currentPixel2;
            }
            currentPixel2 = CentralPixel.cp2;
            currentPixel2.X = currentPixel2.X + PixelCount;
            PixelCount = 0;
            CentralPixel.cp2 = currentPixel2;
            while (currentColor2.R >= 10)                                                           //right2
            {
                currentColor2 = bit.GetPixel(currentPixel2.X, currentPixel2.Y);
                while (bit.GetPixel(currentPixel2.X, (currentPixel2.Y + 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel2.X, currentPixel2.Y, black);
                    currentPixel2.Y = currentPixel2.Y + 1;
                }
                currentPixel2 = CentralPixel.cp2;
                while (bit.GetPixel(currentPixel2.X, (currentPixel2.Y - 1)).R >= 10)
                {
                    bit.SetPixel(currentPixel2.X, currentPixel2.Y, black);
                    currentPixel2.Y = currentPixel2.Y - 1;
                }
                currentPixel2 = CentralPixel.cp2;
                currentPixel2.X = currentPixel2.X + 1;
                CentralPixel.cp2 = currentPixel2;
            }
            currentPixel2 = CentralPixel.cp2;
            currentPixel2.X = currentPixel2.X + PixelCount;
            PixelCount = 0;
            CentralPixel.cp2 = currentPixel2;
        }
     
    }
}
