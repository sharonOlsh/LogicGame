using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace B23_Ex05_SharonOlshanetsky_318845740_DenisKharenko_324464536
{
    public class Player
    {
        public enum e_PlayerSigns
        {
            FirstSign = 'X',
            SecondSign = 'O',
        }

        private string m_PlayerSign;
        private int m_PlayerScore = 0;
        private bool m_IsPlayerPerson;
        private string m_PlayerName;

        public Player(string i_PlayerSign, int i_PlayerScore, bool i_IsPlayerPerson)
        {
            this.m_PlayerSign = i_PlayerSign;
            this.m_PlayerScore = i_PlayerScore;
            this.m_IsPlayerPerson = i_IsPlayerPerson;
        }

        public string PlayerSign
        {
            get { return m_PlayerSign; }
            set { m_PlayerSign = value; }
        }

        public int PlayerScore
        {
            get { return m_PlayerScore; }
            set { m_PlayerScore = value; }
        }

        public bool IsPlayerPerson
        {
            get { return m_IsPlayerPerson; }
            set { m_IsPlayerPerson = value; }
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }
    }
}
