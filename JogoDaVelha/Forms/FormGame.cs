using System;
using System.Windows.Forms;

namespace JogoDaVelha.Forms
{
    public partial class FormGame : Form
    {
        private readonly int[,] values = new int[3, 3];
        private bool PlayerOne
        {
            get => playerOne;
            set
            {
                playerOne = value;

                lblCurrentPlayer.Text = value ? "Jogador Atual: 1º Player" : "Jogador Atual: 2º Player";
                pbCurrentPlayer.BackgroundImage = value ? Properties.Resources.img_circle : Properties.Resources.img_cross;
            }
        }
        
        private bool hasVictory = false;

        private bool playerOne = true;

        public FormGame()
        {
            InitializeComponent();

            PlayerOne = true;
            Repaint();
        }

        private void Repaint()
        {
            tableLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int iT = i;
                    int jT = j;

                    Button btn = new Button();
                    btn.Click += (s, e) => ButtonClick(iT, jT);
                    btn.Enabled = !hasVictory && values[i, j] == 0;

                    if (values[i, j] != 0)
                        btn.BackgroundImage = values[i, j] == 1 ? Properties.Resources.img_circle : Properties.Resources.img_cross;

                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.Dock = DockStyle.Fill;

                    tableLayoutPanel1.Controls.Add(btn, i, j);
                }
            }
        }

        private void ButtonClick(int i, int j)
        {
            values[i, j] = PlayerOne ? 1 : -1;
            Repaint();

            hasVictory = CheckVitory();

            if (hasVictory)
            {
                Repaint();
                ShowResult();
                return;
            }

            PlayerOne = !PlayerOne;
        }

        private bool CheckVitory()
        {
            for (int i = 0; i < 3; i++)
            {
                if (values[i, 0] != 0 && values[i, 0] == values[i, 1] && values[i, 1] == values[i, 2])
                    return true;

                if (values[0, i] != 0 && values[0, i] == values[1, i] && values[1, i] == values[2, i])
                    return true;
            }

            if (values[0, 0] != 0 && values[0, 0] == values[1, 1] && values[1, 1] == values[2, 2])
                return true;
            else if (values[0, 2] != 0 && values[0, 2] == values[1, 1] && values[1, 1] == values[2, 0])
                return true;

            return false;
        }

        private void ShowResult()
        {
            if (PlayerOne)
            {
                MessageBox.Show("O Player 1 foi o vencedor !!!");
            }
            else
            {
                MessageBox.Show("O Player 2 foi o vencedor !!!");
            }
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    values[i, j] = 0;

            PlayerOne = true;
            hasVictory = false;

            Repaint();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
