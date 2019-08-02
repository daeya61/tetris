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

        SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private void Form1_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;

        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    await semaphoreSlim.WaitAsync();
                    myTetris.Rotate();
                    myTetris.DrawBoard(this);
                    semaphoreSlim.Release();
                    break;
                case Keys.Right:
                    await semaphoreSlim.WaitAsync();
                    myTetris.MoveRight();
                    myTetris.DrawBoard(this);
                    semaphoreSlim.Release();
                    break;
                case Keys.Down:
                    await semaphoreSlim.WaitAsync();
                    myTetris.MoveDown();
                    myTetris.DrawBoard(this);
                    semaphoreSlim.Release();
                    break;
                case Keys.Left:
                    await semaphoreSlim.WaitAsync();
                    myTetris.MoveLeft();
                    myTetris.DrawBoard(this);
                    semaphoreSlim.Release();
                    break;
            }
        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            myTetris.StartGame();
            await MoveBlockDownLooplyAsync();
            myTetris.DrawBoard((Form)this);
            MessageBox.Show("Game Over.");
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

        private async Task<bool> MoveBlockDownLooplyAsync()
        {
            while (true)
            {
                await semaphoreSlim.WaitAsync();
                myTetris.MoveDown();
                semaphoreSlim.Release();
                myTetris.DrawBoard((Form)this);
                
                if (myTetris.IsGameOver())
                {
                    break;
                }
                
                await Task.Delay(150);
            }
            return true;
        }
    }
}
