using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B23_Ex05_SharonOlshanetsky_318845740_DenisKharenko_324464536
{
    public class Board : Form
    {
        public const int k_ButtonSpacing = 10;
        public const int k_BorderSpacing = 20;
        public const int k_LabelSpacing = 15;
        private const int k_BoardWindowSize = 500;
        public const string k_EmptyCell = " ";
        public const string k_FormName = "TicTacToeMisere";
        private readonly int r_Dimension;
        private readonly Button[,] r_GameBoard;
        private readonly Label r_FirstPlayerScoreLabel = new Label();
        private readonly Label r_SecondPlayerScoreLabel = new Label();

        public Label FirstPlayerScoreLabel
        {
            get { return r_FirstPlayerScoreLabel; }
        }

        public Label SecondPlayerScoreLabel
        {
            get { return r_SecondPlayerScoreLabel; }
        }

        public Button[,] GameBoard
        {
            get { return r_GameBoard; }
        }
        public int Dimention
        {
            get { return r_Dimension; }
        }
        public Board(int i_Dimension)
        {
            r_Dimension = i_Dimension;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Text = k_FormName;
            Size = new Size(k_BoardWindowSize, k_BoardWindowSize);
            StartPosition = FormStartPosition.CenterScreen;
            r_GameBoard = new Button[r_Dimension, r_Dimension];
            initAllButtonsOnBoard();
            CleanBoard();
        }

        public void CleanBoard()
        {
            for (int i = 0; i < r_Dimension; i++)
            {
                for (int j = 0; j < r_Dimension; j++)
                {
                    r_GameBoard[i, j].Text = k_EmptyCell;
                    r_GameBoard[i, j].Enabled = true;
                }
            }
        }

        private void initAllButtonsOnBoard()
        {
            int totalSpacing = (r_Dimension + 1) * k_ButtonSpacing;
            int availableSize = k_BoardWindowSize - 2 * k_BorderSpacing - totalSpacing;
            int buttonSize = (availableSize / r_Dimension) - k_ButtonSpacing;
            int buttonX;
            int buttonY;

            for (int i = 0; i < r_Dimension; i++)
            {
                for (int j = 0; j < r_Dimension; j++)
                {
                    buttonX = k_BorderSpacing + j * (buttonSize + k_ButtonSpacing);
                    buttonY = k_BorderSpacing + i * (buttonSize + k_ButtonSpacing);
                    r_GameBoard[i, j] = new Button();
                    r_GameBoard[i, j].AutoSize = true;
                    r_GameBoard[i, j].Location = new Point(buttonX, buttonY);
                    r_GameBoard[i, j].Size = new Size(buttonSize, buttonSize);
                    r_GameBoard[i, j].Tag = new int[] { i, j };
                    r_GameBoard[i, j].TabStop = false;
                    this.Controls.Add(r_GameBoard[i, j]);
                }
            }
        }
    }
}