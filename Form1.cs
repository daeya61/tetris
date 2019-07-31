using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            myTetris = new Testris();

        }

        Testris myTetris;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    myTetris.Rotation();
                    myTetris.DrawBoard(this);
                    break;
                case Keys.Right:
                    myTetris.MoveRight();
                    myTetris.DrawBoard(this);
                    break;
                case Keys.Down:
                    myTetris.MoveDown();
                    myTetris.DrawBoard(this);
                    break;
                case Keys.Left:
                    myTetris.MoveLeft();
                    myTetris.DrawBoard(this);
                    break;
            }
        }

        bool isLoaded = false;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(!isLoaded)
            {
                myTetris.StartGame();
                myTetris.DrawBoard(this);
                timer1.Enabled = true;
                isLoaded = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] test = new int[10, 20];
            test[5, 5] = 1;
            test[5, 6] = 1;
            test.GetLength(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            myTetris.DrawBoard(this);
        }
    }
}
