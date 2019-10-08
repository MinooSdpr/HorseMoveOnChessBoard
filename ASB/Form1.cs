using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------
        struct Home
        {
            public int x, y;
            public bool asb;
        }
        //-----------------------------------------------------------------
        Home[,] matrix = new Home[8, 8];
        //-----------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics gr = Graphics.FromImage(b);
            for (int i = 0, y = 30; i < 8; i++, y += 65)
            {
                for (int j = 0, x = 30; j < 8; j++, x += 65)
                {
                    matrix[i, j].x = x;
                    matrix[i, j].y = y;
                    matrix[i, j].asb = false;
                    if ((i + j) % 2 == 0)
                        gr.FillRectangle(Brushes.Black, x, y, 65, 65);
                    else
                        gr.FillRectangle(Brushes.White, x, y, 65, 65);
                }
            }
            this.BackgroundImage = b;
            MoveAsb(0, 0);
        }
        //-----------------------------------------------------------------
        private void MoveAsb(int i, int j)
        {
            matrix[i, j].asb = true;
            pictureBox1.Left = matrix[i, j].x + 65 / 2 - pictureBox1.Width / 2;
            pictureBox1.Top = matrix[i, j].y + 65 / 2 - pictureBox1.Height / 2;
        }
        //-----------------------------------------------------------------
        int n = 1, index_row = 0, index_col = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int r = index_row, c = index_col;
            NextMove();
            MoveAsb(index_row, index_col);
            ZarbDar(r, c);
            n++;
            if (n == 64)
            {
                MessageBox.Show("END");
                timer1.Stop();
            }
        }
        //-----------------------------------------------------------------
        private void NextMove()
        {
            int dr = 2, dc = 1, min = int.MaxValue, i1 = 0, j1 = 0;
            int s1 = -1, s2 = -1;
            for (int tedad = 1; tedad <= 8; tedad++)
            {
                if (tedad == 1 || tedad == 2 || tedad == 5 || tedad == 6)
                {
                    dr = 2; dc = 1;
                }
                else
                {
                    dr = 1; dc = 2;
                }
                if (tedad == 1 || tedad == 2 || tedad == 3 || tedad == 8)
                    s1 = -1;
                else
                    s1 = 1;
                if (tedad == 2 || tedad == 3 || tedad == 4 || tedad == 5)
                    s2 = 1;
                else
                    s2 = -1;
                int i, j, n;
                i = index_row + dr * s1;
                j = index_col + dc * s2;
                if (i >= 0 && i <= 7 && j >= 0 && j <= 7 && !matrix[i, j].asb)
                {
                    n = CounterEmpty(i, j);
                    if (n < min)//n<=min
                    {
                        min = n;
                        //toye array list i,j
                        i1 = i;
                        j1 = j;
                    }
                }
            }
            //random az arraylist 2 ta onsor begire
            index_row = i1;
            index_col = j1;
        }
        //-----------------------------------------------------------------
        private int CounterEmpty(int row, int col)
        {
            int dr = 2, dc = 1, counter = 0;
            int s1 = -1, s2 = -1;
            for (int tedad = 1; tedad <= 8; tedad++)
            {
                //--کمکی
                if (tedad == 1 || tedad == 2 || tedad == 5 || tedad == 6)
                { dr = 2; dc = 1; }
                else
                { dr = 1; dc = 2; }
                if (tedad == 1 || tedad == 2 || tedad == 3 || tedad == 8)
                    s1 = -1;
                else
                    s1 = 1;
                if (tedad == 2 || tedad == 3 || tedad == 4 || tedad == 5)
                    s2 = 1;
                else
                    s2 = -1;
                //-------end
                int i = row + dr * s1;
                int j = col + dc * s2;
                if (i >= 0 && i <= 7 && j >= 0 && j <= 7 && !matrix[i, j].asb)
                    counter++;
            }
            return counter;
        }
        //-----------------------------------------------------------------
        private void ZarbDar(int indexi, int indexj)
        {
            //Graphics gr = this.CreateGraphics();
            //gr.FillRectangle(Brushes.Red, matrix[indexi, indexj].x + 3, matrix[indexi, indexj].y + 3, 58, 58);
            Label l = new Label();
            l.Width = l.Height = 50;
            l.Text = "";
            l.BackColor = Color.Red;
            l.Location = new Point(matrix[indexi, indexj].x + 7, matrix[indexi, indexj].y + 7);
            this.Controls.Add(l);
        }
        //-----------------------------------------------------------------
    }
}
