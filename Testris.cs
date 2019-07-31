using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Tetris
{
    class Testris
    {
        int[,] CurrentBoard;
        int[,] BeforeBoard;
        const int BLOCK_SIZE = 20;

        int[,] CurrentBlock;
        int CurrentBlockType;
        int Direction;
        Point CurrentPosition;

        System.Timers.Timer timer;

        public Testris()
        {
            CurrentBoard = new int[10, 20];
            BeforeBoard = new int[10, 20];
            Direction = 0;
        }

        public void StartGame()
        {
            NewBlock();

            timer = new System.Timers.Timer();
            timer.Interval = 100; // ms
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (CanAction())
                CurrentPosition.Y++;
            else
            {
                MergeBlockToBoard();
                NewBlock();
            }
        }

        private void NewBlock()
        {
            CurrentPosition = new Point(4, 0);
            CurrentBlockType = new Random().Next(0, 6);
            CurrentBlock = GetBlock();
        }

        public void MoveDown()
        {
            CurrentPosition.Y++;
        }

        public void MoveLeft()
        {
            CurrentPosition.X--;
        }

        public void MoveRight()
        {
            CurrentPosition.X++;
        }

        private bool CanAction()
        {
            bool result = true;
            int size = CurrentBlock.GetLength(0);
            for (int i = 0; i < CurrentBlock.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentBlock.GetLength(1); j++)
                {
                    if(j + CurrentPosition.Y + 1 >= CurrentBoard.GetLength(1))
                    {
                        result = false;
                        break;
                    }

                    if(CurrentBlock[i,j] != 0)
                    if (CurrentBoard[i + CurrentPosition.X, j + CurrentPosition.Y + 1] != 0)
                    { 
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void MergeBlockToBoard()
        {
            for (int i = 0; i < CurrentBlock.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentBlock.GetLength(1); j++)
                {
                    if(CurrentBlock[i,j] != 0)
                        CurrentBoard[i + CurrentPosition.X, j + CurrentPosition.Y] = CurrentBlock[i, j];
                }
            }
        }

        public void Rotation()
        {
            Direction++;
            Direction = Direction % 4;
            CurrentBlock = GetBlock();
        }

        private int[,] GetBlock()
        {
            switch (CurrentBlockType)
            {               
                case 0: //O
                    //**
                    //**
                    return new int[2, 2] { { 1, 1 }, { 1, 1 } };
                case 1: //I
                    //_*__  ____
                    //_*__  ****
                    //_*__  ____
                    //_*__  ____
                    if (Direction == 0 || Direction == 2)
                        return new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
                    else if(Direction == 1 || Direction == 3)
                        return new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 } };
                    break;
                case 2: //Z
                    //_*_  **_
                    //**_  _**
                    //*__  ___
                    if (Direction == 0 || Direction == 2)
                        return new int[3, 3] { { 0, 1, 1 }, { 1, 1, 0 }, { 0, 0, 0 } };
                    else if (Direction == 1 || Direction == 3)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    break;
                case 3: //S
                        //_*_  _**
                        //_**  **_
                        //__*  ___
                    if (Direction == 0 || Direction == 2)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 0 }, { 0, 1, 1 } };
                    else if (Direction == 1 || Direction == 3)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 1, 0, 0 } };
                    break;
                case 4: //J
                    //_**  ***  _*_  *__
                    //_*_  __*  _*_  ***
                    //_*_  ___  **_  ___
                    if (Direction == 0)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 0, 0 } };
                    else if (Direction == 1)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 1, 0 } };
                    else if (Direction == 2)
                        return new int[3, 3] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if (Direction == 3)
                        return new int[3, 3] { { 1, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
                    break;
                case 5: //L
                    //**_  __*  _*_  ***
                    //_*_  ***  _*_  *__
                    //_*_  ___  _**  ___
                    if (Direction == 0)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if(Direction == 1)
                        return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } };
                    else if (Direction == 2)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 1 } };
                    else if (Direction == 3)
                        return new int[3, 3] { { 1, 1, 0 }, { 1, 0, 0 }, { 1, 0, 0 } };
                    break;
                case 6: //T
                    //***  _*_  _*_  *__
                    //_*_  **_  ***  **_
                    //___  _*_  ___  *__
                    if (Direction == 0)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 0, 0 } };
                    else if(Direction == 1)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if (Direction == 2)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    else if (Direction == 3)
                        return new int[3, 3] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    break;
            }
            throw new Exception("invalid block type");
        }
        
        public void DrawBoard(Form form)
        {
            Graphics graphics = form.CreateGraphics();
            graphics.Clear(Color.Black);
            
            for (int i = 0; i < CurrentBoard.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentBoard.GetLength(1); j++)
                {
                    //graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1F),
                    //    new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE));

                    if (CurrentBoard[i, j] == 1)
                        graphics.FillRectangle(new SolidBrush(Color.Gray),
                        new Rectangle(i * BLOCK_SIZE + 1, j * BLOCK_SIZE + 1, BLOCK_SIZE - 1, BLOCK_SIZE - 1));
                }
            }

            //현재 블록 그리기
            for (int i = 0; i < CurrentBlock.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentBlock.GetLength(1); j++)
                {
                    if(CurrentBlock[i,j] == 1)
                    graphics.FillRectangle(new SolidBrush(Color.Blue),
                        new Rectangle((i + CurrentPosition.X) * BLOCK_SIZE + 1, 
                        (j + CurrentPosition.Y) * BLOCK_SIZE + 1, BLOCK_SIZE - 1, BLOCK_SIZE - 1));
                }
            }
        }
    }
}
