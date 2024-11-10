using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B23_Ex05_SharonOlshanetsky_318845740_DenisKharenko_324464536
{
    public partial class GameSettingsForm : Form
    {
        public const string k_DefaultPlayerOneName = "Player 1";
        public const string k_DefaultPlayerTwoName = "Player 2";
        public const string k_DefaultComputerName = "Computer";
        public const string k_DefaultComputerTextBox = "[Computer]";
        public const string k_Empty = "";
        private GameLogic m_GameLogic = new GameLogic();
        public Font m_MediumFont = new Font("Arial", 10);
        private Label m_LabelPlayers = new Label();
        private Label m_LabelPlayer1 = new Label();
        private Label m_LabelPlayer2 = new Label();
        private Label m_LabelBoardSize = new Label();
        private Label m_LabelRows = new Label();
        private Label m_LabelCols = new Label();
        private TextBox m_TextBoxPlayer1Name = new TextBox();
        private TextBox m_TextBoxPlayer2Name = new TextBox();
        private CheckBox m_CheckBoxPlayer2Computer = new CheckBox();
        private NumericUpDown m_NumericUpDownRows = new NumericUpDown();
        private NumericUpDown m_NumericUpDownCols = new NumericUpDown();
        private Button m_ButtonStart = new Button();

        public GameSettingsForm()
        {
            this.Size = new Size(270, 300);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.SuspendLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            InitializePlayersDisplay();
            InitializeBoardSizeDisplay();
            InitializeStartButton();
            AddControls();
        }

        private void InitializePlayersDisplay()
        {
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Font = m_MediumFont;
            m_LabelPlayers.Location = new Point(15, 10);
            InitializePlayer1Display();
            InitializePlayer2Display();
        }

        private void InitializePlayer1Display()
        {
            int textBoxTop;
            
            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.Font = m_MediumFont;
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Top = m_LabelPlayers.Bottom + 7;
            m_LabelPlayer1.Left = m_LabelPlayers.Left + 15;
            textBoxTop = m_LabelPlayer1.Top + m_LabelPlayer1.Height / 2;
            textBoxTop -= m_TextBoxPlayer1Name.Height / 2;
            m_TextBoxPlayer1Name.Location = new Point(m_LabelPlayer1.Right + 8, textBoxTop - 2);
            m_TextBoxPlayer1Name.Font = m_MediumFont;
        }

        private void InitializePlayer2Display()
        {
            int textBox2Top;
           
            m_CheckBoxPlayer2Computer.AutoSize = true;
            m_CheckBoxPlayer2Computer.Left = m_LabelPlayer1.Left + 2;
            m_CheckBoxPlayer2Computer.Top = m_LabelPlayer1.Bottom + 9;
            m_CheckBoxPlayer2Computer.Name = "checkBoxPlayer2Computer";
            m_CheckBoxPlayer2Computer.UseVisualStyleBackColor = true;
            m_LabelPlayer2.Text = "Player 2:";
            m_LabelPlayer2.Font = m_MediumFont;
            m_LabelPlayer2.Top = m_LabelPlayer1.Bottom + 7;
            m_LabelPlayer2.Left = m_LabelPlayer1.Left + 20;
            m_LabelPlayer2.AutoSize = true;
            m_TextBoxPlayer2Name.Enabled = false;
            m_TextBoxPlayer2Name.Text = k_DefaultComputerTextBox;
            m_TextBoxPlayer2Name.Font = m_MediumFont;
            textBox2Top = m_LabelPlayer2.Top + m_LabelPlayer2.Height / 2;
            textBox2Top -= m_TextBoxPlayer2Name.Height / 2;
            m_TextBoxPlayer2Name.Location = new Point(m_TextBoxPlayer1Name.Left, textBox2Top - 2);
            m_CheckBoxPlayer2Computer.Click += new System.EventHandler(this.m_CheckBoxPlayer2Computer_Checked);
        }

        private void InitializeBoardSizeDisplay()
        {
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.AutoSize = true;
            m_LabelBoardSize.Left = m_LabelPlayers.Left;
            m_LabelBoardSize.Top = m_LabelPlayer2.Bottom + 25;
            m_LabelBoardSize.Font = m_MediumFont;
            InitializeRowsDisplay();
            InitializeColsDisplay();
        }

        private void InitializeRowsDisplay()
        {
            int numericUpDownTop;

            m_LabelRows.Text = "Rows:";
            m_LabelRows.AutoSize = true;
            m_LabelRows.Left = m_LabelPlayer1.Left;
            m_LabelRows.Top = m_LabelBoardSize.Bottom + 7;
            m_LabelRows.Font = m_MediumFont;
            m_NumericUpDownRows.AutoSize = true;
            m_NumericUpDownRows.Size = new Size(10, 10);
            m_NumericUpDownRows.Minimum = 4;
            m_NumericUpDownRows.Maximum = 10;
            m_NumericUpDownRows.TabIndex = 4;
            m_NumericUpDownRows.Font = m_MediumFont;
            numericUpDownTop = m_LabelRows.Top + m_LabelRows.Height / 2;
            numericUpDownTop -= m_NumericUpDownRows.Height / 2;
            m_NumericUpDownRows.Top = numericUpDownTop - 2;
            m_NumericUpDownRows.Left = m_LabelRows.Right - 45;
            m_NumericUpDownRows.ValueChanged += new System.EventHandler(this.m_NumericUpDownRows_ValueChanged);
        }

        private void InitializeColsDisplay()
        {
            m_LabelCols.Text = "Cols:";
            m_LabelCols.AutoSize = true;
            m_LabelCols.Left = m_NumericUpDownRows.Right + 50;
            m_LabelCols.Top = m_LabelRows.Top;
            m_LabelCols.Font = m_MediumFont;
            m_NumericUpDownCols.AutoSize = true;
            m_NumericUpDownCols.Size = m_NumericUpDownRows.Size;
            m_NumericUpDownCols.Minimum = 4;
            m_NumericUpDownCols.Maximum = 10;
            m_NumericUpDownCols.TabIndex = 4;
            m_NumericUpDownCols.Font = m_MediumFont;
            m_NumericUpDownCols.Left = m_LabelCols.Right - 45;
            m_NumericUpDownCols.Top = m_NumericUpDownRows.Top;
            m_NumericUpDownCols.ValueChanged += new System.EventHandler(this.m_NumericUpDownCols_ValueChanged);
        }

        private void InitializeStartButton()
        {
            m_ButtonStart.Name = "buttonStart";
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Font = m_MediumFont;
            m_ButtonStart.Size = new Size(230, 30);
            m_ButtonStart.Left = m_LabelBoardSize.Left;
            m_ButtonStart.Top = m_LabelRows.Bottom + 30;
            m_ButtonStart.UseVisualStyleBackColor = true;
            m_ButtonStart.Click += new System.EventHandler(this.m_ButtonStart_Click);
        }

        private void AddControls()
        {
            this.Controls.Add(m_LabelPlayers);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_TextBoxPlayer1Name);
            this.Controls.Add(m_CheckBoxPlayer2Computer);
            this.Controls.Add(m_LabelPlayer2);
            this.Controls.Add(m_TextBoxPlayer2Name);
            this.Controls.Add(m_LabelBoardSize);
            this.Controls.Add(m_LabelRows);
            this.Controls.Add(m_NumericUpDownRows);
            this.Controls.Add(m_LabelCols);
            this.Controls.Add(m_NumericUpDownCols);
            this.Controls.Add(m_ButtonStart);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void m_NumericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            m_NumericUpDownCols.Value = m_NumericUpDownRows.Value;
        }

        private void m_NumericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            m_NumericUpDownRows.Value = m_NumericUpDownCols.Value;
        }

        private void m_CheckBoxPlayer2Computer_Checked(object sender, EventArgs e)
        {
            if (m_CheckBoxPlayer2Computer.Checked)
            {
                m_TextBoxPlayer2Name.Enabled = true;
                m_TextBoxPlayer2Name.Text = k_Empty;
            }
            else
            {
                m_TextBoxPlayer2Name.Enabled = false;
                m_TextBoxPlayer2Name.Text = k_DefaultComputerTextBox;
            }
        }

        private void InitializePlayersNames(out string o_PlayerOneName, out string o_PlayerTwoName)
        {
            if (string.IsNullOrWhiteSpace(m_TextBoxPlayer1Name.Text))
            {
                o_PlayerOneName = k_DefaultPlayerOneName;
            }
            else
            {
                o_PlayerOneName = m_TextBoxPlayer1Name.Text;
            }
            if (m_CheckBoxPlayer2Computer.Checked)
            {
                if (string.IsNullOrWhiteSpace(m_TextBoxPlayer2Name.Text))
                { 
                    o_PlayerTwoName = k_DefaultPlayerTwoName;
                }
                else
                {
                    o_PlayerTwoName = m_TextBoxPlayer2Name.Text;
                }
            }
            else
            {
                o_PlayerTwoName = k_DefaultComputerName;
            }
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            string playerOneName;
            string playerTwoName;

            InitializePlayersNames(out playerOneName, out playerTwoName);
            Interface gameInterface = new Interface();
            gameInterface.InitializeGame((int)m_NumericUpDownRows.Value, m_CheckBoxPlayer2Computer.Checked, playerOneName, playerTwoName);
            this.DialogResult = DialogResult.OK;
            gameInterface.Logic.Board.ShowDialog();
            this.Close();
        }
    }
}