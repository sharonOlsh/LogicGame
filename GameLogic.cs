using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B23_Ex05_SharonOlshanetsky_318845740_DenisKharenko_324464536
{
    public class GameLogic
    {
        private Board m_Board;
        public const string k_TieMessage = "Tie";
        public const int k_FirstPlayerNumber = 1;
        public const int k_SecondPlayerNumber = 2;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayer;
        private List<Tuple<int, int>> m_FreeCellsList = new List<Tuple<int, int>>();
        private int m_NumberOfEmptyCells;
        public event Action<string> GameEnding;
        public event Action<string []> ScoreUpdate;
        public event Action<int> CurrentPlayerUpdate;

        public Player PlayerOne
        {
            get { return m_PlayerOne; }
        }

        public Player PlayerTwo
        {
            get { return m_PlayerTwo; }
        }
        public Board Board
        {
            get { return m_Board; }
        }

        protected virtual void OnCurrentPlayerUpdate(Player i_CurrentPlayer)
        {
            if (CurrentPlayerUpdate != null)
            {
                if (i_CurrentPlayer == PlayerOne)
                {
                    CurrentPlayerUpdate.Invoke(k_FirstPlayerNumber);
                }
                else
                {
                    CurrentPlayerUpdate.Invoke(k_SecondPlayerNumber);
                }
            }
        }

        protected virtual void OnScoreUpdate()
        {
            if (ScoreUpdate != null)
            {
                ScoreUpdate.Invoke(new string[] { m_PlayerOne.PlayerScore.ToString(), m_PlayerTwo.PlayerScore.ToString(), m_PlayerOne.PlayerName, m_PlayerTwo.PlayerName });
            }
        }

        public void InitializeBoard(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            activateAllButtons();
            m_FreeCellsList.Clear();
            FillFreeCellsList(m_FreeCellsList);
            m_NumberOfEmptyCells = m_Board.Dimention * m_Board.Dimention;
        }

        public void InitializePlayers(bool i_IsPlayerPerson, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_PlayerOne = new Player(((char)Player.e_PlayerSigns.FirstSign).ToString(), 0, true);
            m_PlayerOne.PlayerName = i_FirstPlayerName;
            if (i_IsPlayerPerson == true)
            {
                m_PlayerTwo = new Player(((char)Player.e_PlayerSigns.SecondSign).ToString(), 0, true);
            }
            else
            {
                m_PlayerTwo = new Player(((char)Player.e_PlayerSigns.SecondSign).ToString(), 0, false);
            }

            m_PlayerTwo.PlayerName = i_SecondPlayerName;
            m_CurrentPlayer = m_PlayerOne;
            OnScoreUpdate();
            OnCurrentPlayerUpdate(m_CurrentPlayer);
        }

        protected virtual void OnGameEnding(bool i_IsTie)
        {
            string message;

            if (!i_IsTie)
            {
                message = m_CurrentPlayer.PlayerName;
            }
            else
            {
                message = k_TieMessage;
            }

            m_CurrentPlayer = m_PlayerOne;
            if (GameEnding != null)
            {
                GameEnding.Invoke(message);
            }
        }

        private void activateAllButtons()
        {
            for (int i = 0; i < m_Board.Dimention; i++)
            {
                for (int j = 0; j < m_Board.Dimention; j++)
                {
                    m_Board.GameBoard[i, j].Click += new EventHandler(button_Click);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            HandleMoveLogic(sender as Button, m_FreeCellsList, out bool o_IsGameOver);
            if (!o_IsGameOver && !(m_CurrentPlayer.IsPlayerPerson))
            {
                HandleComputerMove(out int o_RowNum, out int o_ColNum, m_Board.Dimention, m_FreeCellsList);
                HandleMoveLogic(m_Board.GameBoard[o_RowNum, o_ColNum], m_FreeCellsList, out o_IsGameOver);
            }
        }

        public void HandleMoveLogic(Button i_CurrentCell, List<Tuple<int, int>> i_FreeCellsList, out bool o_IsGameOver)
        {
            int[] rowAndColNumbers = i_CurrentCell.Tag as int[];
            bool isTie = false;

            o_IsGameOver = false;
            i_CurrentCell.Text = m_CurrentPlayer.PlayerSign;
            i_CurrentCell.Enabled = false;
            m_FreeCellsList.Remove(m_FreeCellsList.Find(t => t.Item1 == rowAndColNumbers[0] && t.Item2 == rowAndColNumbers[1]));
            m_NumberOfEmptyCells--;
            o_IsGameOver = CheckIfPlayerLost(m_CurrentPlayer, rowAndColNumbers[0], rowAndColNumbers[1]);
            m_CurrentPlayer = UpdateCurrentPlayer(m_CurrentPlayer);
            if (o_IsGameOver)
            {
                m_CurrentPlayer.PlayerScore++;
                OnScoreUpdate();
            }
            else
            {
                o_IsGameOver = CheckIfThereIsTie(m_NumberOfEmptyCells);
                isTie = true;
            }
            if (o_IsGameOver)
            {
                OnGameEnding(isTie);
            }
        }

        public void FillFreeCellsList(List<Tuple<int, int>> i_FreeCellsList)
        {
            for (int row = 0; row < m_Board.Dimention; row++)
            {
                for (int col = 0; col < m_Board.Dimention; col++)
                {
                    i_FreeCellsList.Add(new Tuple<int, int>(row, col));
                }
            }
        }

        public static void HandleComputerMove(out int o_RowNum, out int o_ColNum, int i_BoardSize, List<Tuple<int, int>> freeCellsList)
        {
            Random rnd = new Random();
            int index = rnd.Next(freeCellsList.Count);
            Tuple<int, int> newTouple = freeCellsList[index];

            o_RowNum = newTouple.Item1;
            o_ColNum = newTouple.Item2;
        }

        public bool CheckIfThereIsTie(int i_NumberOfEmptyCells)
        {
            bool isTie = false;

            if (i_NumberOfEmptyCells == 0)
            {
                isTie = true;
            }

            return isTie;
        }

        public Player UpdateCurrentPlayer(Player i_CurrentPlayer)
        {
            Player newCurrentPlayer;

            if (i_CurrentPlayer.PlayerSign == ((char)Player.e_PlayerSigns.FirstSign).ToString())
            {
                newCurrentPlayer = m_PlayerTwo;
            }
            else
            {
                newCurrentPlayer = m_PlayerOne;
            }

            OnCurrentPlayerUpdate(newCurrentPlayer);

            return newCurrentPlayer;
        }

        public bool CheckIfPlayerLost(Player i_Current_Player, int i_RowNum, int i_ColNum)
        {
            int countNumberOfSameSigns = 0;
            string currPlayerSign = m_Board.GameBoard[i_RowNum, i_ColNum].Text;
            bool currentPlayerLost = false;

            for (int col = 0; col < m_Board.Dimention; col++)
            {
                if (m_Board.GameBoard[i_RowNum, col].Text == currPlayerSign)
                {
                    countNumberOfSameSigns++;
                }
                else
                {
                    break;
                }
            }

            if (countNumberOfSameSigns == m_Board.Dimention)
            {
                currentPlayerLost = true;
            }
            else
            {
                countNumberOfSameSigns = 0;
                for (int row = 0; row < m_Board.Dimention; row++)
                {
                    if (m_Board.GameBoard[row, i_ColNum].Text == currPlayerSign)
                    {
                        countNumberOfSameSigns++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (countNumberOfSameSigns == m_Board.Dimention)
                {
                    currentPlayerLost = true;
                }
                else
                {
                    currentPlayerLost = CheckIfThereIsASameSignDiagonal(currPlayerSign);
                }
            }

            return currentPlayerLost;
        }

        public bool CheckIfThereIsASameSignDiagonal(string i_CurrPlayerSign)
        {
            int countNumberOfSameSigns = 0;
            bool currentPlayerLost = false;

            for (int row = 0; row < m_Board.Dimention; row++)
            {
                if (m_Board.GameBoard[row, row].Text == i_CurrPlayerSign)
                {
                    countNumberOfSameSigns++;
                }
                else
                {
                    break;
                }
            }
            if (countNumberOfSameSigns == m_Board.Dimention)
            {
                currentPlayerLost = true;
            }
            else
            {
                countNumberOfSameSigns = 0;
                for (int row = 0; row < m_Board.Dimention; row++)
                {
                    if (m_Board.GameBoard[row, (m_Board.Dimention - 1) - row].Text == i_CurrPlayerSign)
                    {
                        countNumberOfSameSigns++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (countNumberOfSameSigns == m_Board.Dimention)
                {
                    currentPlayerLost = true;
                }
                else
                {
                    currentPlayerLost = false;
                }
            }

            return currentPlayerLost;
        }
    }
}