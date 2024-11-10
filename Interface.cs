using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B23_Ex05_SharonOlshanetsky_318845740_DenisKharenko_324464536
{
    public class Interface
    {
        private const string k_AnotherRoundMessage = "Would you like to play another round?";
        private GameLogic m_Logic = new GameLogic();

        public void InitializeGame(int i_BoardDimension, bool i_IsPlayerTwoPerson, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            int boardSize;
            bool isPlayerPerson;

            boardSize = i_BoardDimension;
            m_Logic.GameEnding += GameLogic_AskIfPlayerWantsToPlayAgain;
            m_Logic.ScoreUpdate += GameLogic_UpdateScoreLabels;
            m_Logic.CurrentPlayerUpdate += GameLogic_UpdateCurrentPlayerLabelsFont;
            m_Logic.InitializeBoard(boardSize);
            isPlayerPerson = i_IsPlayerTwoPerson;
            m_Logic.InitializePlayers(isPlayerPerson, i_FirstPlayerName, i_SecondPlayerName);
        }

        public void GameLogic_UpdateCurrentPlayerLabelsFont(int i_CurrentPlayerNumber)
        {
            if(i_CurrentPlayerNumber == GameLogic.k_FirstPlayerNumber)
            {
                m_Logic.Board.FirstPlayerScoreLabel.Font = new Font(m_Logic.Board.FirstPlayerScoreLabel.Font, FontStyle.Bold);
                m_Logic.Board.SecondPlayerScoreLabel.Font = new Font(m_Logic.Board.SecondPlayerScoreLabel.Font, FontStyle.Regular);
            }
            else
            {
                m_Logic.Board.FirstPlayerScoreLabel.Font = new Font(m_Logic.Board.FirstPlayerScoreLabel.Font, FontStyle.Regular);
                m_Logic.Board.SecondPlayerScoreLabel.Font = new Font(m_Logic.Board.SecondPlayerScoreLabel.Font, FontStyle.Bold);
            }
        }

        public void GameLogic_UpdateScoreLabels(string [] i_PlayerScoresAndNames)
        {
            const int k_FirstPlayerScoreIndex = 0;
            const int k_SecondPlayerScoreIndex = 1;
            const int k_FirstPlayerNameIndex = 2;
            const int k_SecondPlayerNameIndex = 3;
            int labelX;
            int labelY;

            m_Logic.Board.FirstPlayerScoreLabel.Text = string.Format("{0}: {1}", i_PlayerScoresAndNames[k_FirstPlayerNameIndex], int.Parse(i_PlayerScoresAndNames[k_FirstPlayerScoreIndex]));
            m_Logic.Board.SecondPlayerScoreLabel.Text = string.Format("{0}: {1}", i_PlayerScoresAndNames[k_SecondPlayerNameIndex], int.Parse(i_PlayerScoresAndNames[k_SecondPlayerScoreIndex]));
            m_Logic.Board.FirstPlayerScoreLabel.AutoSize = true;
            m_Logic.Board.SecondPlayerScoreLabel.AutoSize = true;
            labelX = m_Logic.Board.GameBoard[m_Logic.Board.Dimention - 1, (m_Logic.Board.Dimention / 2) - 1].Left;
            labelY = m_Logic.Board.GameBoard[m_Logic.Board.Dimention - 1, (m_Logic.Board.Dimention / 2) - 1].Bottom + Board.k_LabelSpacing;
            m_Logic.Board.FirstPlayerScoreLabel.Location = new Point(labelX, labelY);
            m_Logic.Board.Controls.Add(m_Logic.Board.FirstPlayerScoreLabel);
            m_Logic.Board.SecondPlayerScoreLabel.Location = new Point(m_Logic.Board.FirstPlayerScoreLabel.Right + Board.k_LabelSpacing, m_Logic.Board.FirstPlayerScoreLabel.Top);
            m_Logic.Board.Controls.Add(m_Logic.Board.SecondPlayerScoreLabel);
        }

        public void GameLogic_AskIfPlayerWantsToPlayAgain(string i_WinnerName)
        {
            DialogResult anotherRound;
            
            if (i_WinnerName == GameLogic.k_TieMessage)
            {
                anotherRound = MessageBox.Show(string.Format("Tie!\n{1}", i_WinnerName, k_AnotherRoundMessage), "A Tie!", MessageBoxButtons.YesNo);
            }
            else
            {
                anotherRound = MessageBox.Show(string.Format("The Winner is {0}!\n{1}", i_WinnerName, k_AnotherRoundMessage), "A Win!", MessageBoxButtons.YesNo);
            }

            m_Logic.Board.Close();
            m_Logic.Board.Dispose();
            if (anotherRound == DialogResult.Yes)
            {
                m_Logic.InitializeBoard(m_Logic.Board.Dimention);
                GameLogic_UpdateScoreLabels(new string[] { m_Logic.PlayerOne.PlayerScore.ToString(), m_Logic.PlayerTwo.PlayerScore.ToString(), m_Logic.PlayerOne.PlayerName, m_Logic.PlayerTwo.PlayerName });
                GameLogic_UpdateCurrentPlayerLabelsFont(GameLogic.k_FirstPlayerNumber);
                m_Logic.Board.ShowDialog();
            }
        }

        public GameLogic Logic
        {
            get { return m_Logic; }
        }
    }
}
