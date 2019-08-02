using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    class Testris
    {
        int[,] Board;
        const int BLOCK_SIZE = 20;

        int[,] Block;
        int BlockType;
        int Direction;
        Point Position;

        public Testris()
        {
            Board = new int[10, 20];
            Direction = 0;
        }

        public void StartGame()
        {
            NewBlock();
        }

        private void NewBlock()
        {
            Position = new Point(4, 0);
            BlockType = new Random().Next(0, 7);
            Block = GetBlock(Direction);
        }

        public void MoveDown()
        {
            if(CanMove(2))
                Position.Y++;
            else
            {
                MergeBlockToBoard();
                NewBlock();
            }
        }

        public void MoveRight()
        {
            if (CanMove(1))
                Position.X++;
        }

        public void MoveLeft()
        {
            if (CanMove(3))
                Position.X--;
        }

        private bool CanMove(int direction)
        {
            bool result = true;
            
            for (int i = 0; i < Block.GetLength(0); i++)
            {
                for (int j = 0; j < Block.GetLength(1); j++)
                {
                    if (Block[i, j] == 0)
                        continue;

                    switch(direction)
                    {
                        case 1: //right
                            if (i + Position.X + 1 >= Board.GetLength(0))
                                return false;
                            if (Board[i + Position.X +1, j + Position.Y] != 0)
                                return false;
                            break;
                        case 2: //down
                            if (j + Position.Y + 1 >= Board.GetLength(1))
                                return false;
                            if (Board[i + Position.X, j + Position.Y + 1] != 0)
                                return false;
                            break;
                        case 3: //left
                            if (i + Position.X - 1 < 0)
                                return false;
                            if (Board[i + Position.X - 1, j + Position.Y] != 0)
                                return false;
                            break;
                    }
                }
            }
            return result;
        }
        
        private void MergeBlockToBoard()
        {
            for (int i = 0; i < Block.GetLength(0); i++)
            {
                for (int j = 0; j < Block.GetLength(1); j++)
                {
                    if(Block[i,j] != 0)
                        Board[i + Position.X, j + Position.Y] = Block[i, j];
                }
            }
            DestroyLine();
        }

        private void DestroyLine()
        {
            for (int k = 0; k < 4; k++)
            {
                bool lineFull = false;
                for (int j = Board.GetLength(1) - 1; j >= 0; j--)
                {
                    if (!lineFull)
                    {
                        for (int i = 0; i < Board.GetLength(0); i++)
                        {
                            if (Board[i, j] == 0)
                            {
                                lineFull = false;
                                break;
                            }
                            else
                                lineFull = true;
                        }
                    }
                   
                    if(lineFull)
                    { 
                        for (int i = 0; i < Board.GetLength(0); i++)
                        {
                            if (j - 1 >= 0)
                                Board[i, j] = Board[i, j - 1];
                            else
                                Board[i, j] = 0;
                        }
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int i = 0; i < Block.GetLength(0); i++)
            {
                for(int j=0; j< Block.GetLength(1); j++)
                {
                    if (Block[i, j] == 1 && Board[Position.X + i, Position.Y + j] == 1)
                        return true;
                }
            }
            return false;
        }

        public void Rotate()
        {
            if (CanRotate())
            {
                Direction++;
                Direction = Direction % 4;
                Block = GetBlock(Direction);
            }
        }

        private bool CanRotate()
        {
            try
            {
                bool result = true;
                int tempDir = (Direction + 1) % 4;
                int[,] tempBlock = GetBlock(tempDir);

                if (tempBlock.GetLength(0) + Position.X > Board.GetLength(0)
                    || Position.X < 0)
                    return false;

                for (int i = 0; i < tempBlock.GetLength(0); i++)
                {
                    for (int j = 0; j < tempBlock.GetLength(1); j++)
                    {
                        if (Board[i + Position.X, j + Position.Y] == 1
                            && tempBlock[i, j] == 1)
                            return false;
                    }
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        private int[,] GetBlock(int direction)
        {
            switch (BlockType)
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
                    if (direction == 0 || direction == 2)
                        return new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
                    else if(direction == 1 || direction == 3)
                        return new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 } };
                    break;
                case 2: //Z
                    //_*_  **_
                    //**_  _**
                    //*__  ___
                    if (direction == 0 || direction == 2)
                        return new int[3, 3] { { 0, 1, 1 }, { 1, 1, 0 }, { 0, 0, 0 } };
                    else if (direction == 1 || direction == 3)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    break;
                case 3: //S
                        //_*_  _**
                        //_**  **_
                        //__*  ___
                    if (direction == 0 || direction == 2)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 0 }, { 0, 1, 1 } };
                    else if (direction == 1 || direction == 3)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 1, 0, 0 } };
                    break;
                case 4: //J
                    //_**  ***  _*_  *__
                    //_*_  __*  _*_  ***
                    //_*_  ___  **_  ___
                    if (direction == 0)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 0, 0 } };
                    else if (direction == 1)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 1, 0 } };
                    else if (direction == 2)
                        return new int[3, 3] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if (direction == 3)
                        return new int[3, 3] { { 1, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
                    break;
                case 5: //L
                    //**_  __*  _*_  ***
                    //_*_  ***  _*_  *__
                    //_*_  ___  _**  ___
                    if (direction == 0)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if(direction == 1)
                        return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } };
                    else if (direction == 2)
                        return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 1 } };
                    else if (direction == 3)
                        return new int[3, 3] { { 1, 1, 0 }, { 1, 0, 0 }, { 1, 0, 0 } };
                    break;
                case 6: //T
                    //***  _*_  _*_  *__
                    //_*_  **_  ***  **_
                    //___  _*_  ___  *__
                    if (direction == 0)
                        return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 0 }, { 1, 0, 0 } };
                    else if(direction == 1)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                    else if (direction == 2)
                        return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    else if (direction == 3)
                        return new int[3, 3] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    break;
            }
            throw new Exception("invalid block type");
        }
        
        public void DrawBoard(Form form)
        {
            Graphics graphics = form.CreateGraphics();
            graphics.Clear(Color.Black);
            
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    //graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1F),
                    //    new Rectangle(i * BLOCK_SIZE, j * BLOCK_SIZE, BLOCK_SIZE, BLOCK_SIZE));

                    if (Board[i, j] == 1)
                        graphics.FillRectangle(new SolidBrush(Color.Gray),
                        new Rectangle(i * BLOCK_SIZE + 1, j * BLOCK_SIZE + 1, BLOCK_SIZE - 1, BLOCK_SIZE - 1));
                }
            }

            //현재 블록 그리기
            for (int i = 0; i < Block.GetLength(0); i++)
            {
                for (int j = 0; j < Block.GetLength(1); j++)
                {
                    if(Block[i,j] == 1)
                    graphics.FillRectangle(new SolidBrush(Color.Blue),
                        new Rectangle((i + Position.X) * BLOCK_SIZE + 1, 
                        (j + Position.Y) * BLOCK_SIZE + 1, BLOCK_SIZE - 1, BLOCK_SIZE - 1));
                }
            }
        }
    }
}
