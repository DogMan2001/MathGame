using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MathGame.Client
{
    public partial class GameTile : UserControl
    {
        private int numberToGuess;

        private Color originalColor;

        const int numberOfBlinking = 6;

        public bool IsFinished => this.Enabled == false;

        [Category(("Custom"))]
        public Color FailColor { get; set; } = Color.Red;

        [Category("Custom")]
        public int NumberToGuess
        {
            get => numberToGuess;
            set
            {
                numberToGuess = value;
                labelNumberToGuess.Text = numberToGuess.ToString();
            }
        }

        [Category("Custom")]
        public string GuessNumberText
        {

            get => guessNumberTextBox.Text;
            set
            {
                guessNumberTextBox.Text = value;
                GuessNumberChanged();
            }
        }

        [Category("Custom")]
        public int GuessNumber
        {
            get => string.IsNullOrEmpty(guessNumberTextBox.Text) ? 0 : int.Parse(guessNumberTextBox.Text);
            set
            {
                GuessNumberText = value.ToString();
            }
        }

        public bool NumberToGuessVisible
        {
            get => labelNumberToGuess.Visible;
            set => labelNumberToGuess.Visible = value;
        }

        [Category("Custom")]
        public event EventHandler TriedToGuess;

        [Category("Custom"), Description("When Tile's mmber is guessed")]
        public event EventHandler TileFinished;

        public GameTile()
        {
            InitializeComponent();
            this.Enabled = true;
            NumberToGuess = 0;
            btnSet.Enabled = false;
            labelNumberToGuess.Visible = false;
        }

        public void Finish()
        {
            Enabled = false;
            btnSet.Visible = false;
            guessNumberTextBox.Visible = false;
            BackColor = Color.FromArgb(18, 224, 70);

            labelNumberToGuess.Visible = true;
            labelNumberToGuess.Font = new Font("Sagoe UI", 20);
            labelNumberToGuess.Location = new Point(this.Width / 2 - labelNumberToGuess.Width / 2, this.Height / 2 - labelNumberToGuess.Height / 2);

            if (TileFinished != null)
            {
                TileFinished.Invoke(this, EventArgs.Empty);
            }
        }

        public bool ValidateGuess()
        {
            return NumberToGuess == GuessNumber;
        }

        private void GuessNumberChanged()
        {
            btnSet.Enabled = GuessNumber != 0;
        }

        private void Guess()
        {
            if (GuessNumber == 0) return;

            if (ValidateGuess())
            {
                Finish();
            }
            else
            {
                originalColor = BackColor;
                BackColor = FailColor;
                timerFailedGuess.Tag = 0;
                timerFailedGuess.Start();
            }

            if (TriedToGuess != null)
                TriedToGuess.Invoke(this, EventArgs.Empty);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Guess();
        }

        private void guessNumberTextBox_Leave(object sender, EventArgs e)
        {
            GuessNumberText = guessNumberTextBox.Text;
        }

        private void guessNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // User pressed enter
                GuessNumberText = guessNumberTextBox.Text;
                Guess();
            }
        }

        private void guessNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            GuessNumberText = guessNumberTextBox.Text;
        }

        private void timerFailedGuess_Tick(object sender, EventArgs e)
        {
            int count = (int)timerFailedGuess.Tag;
            count++;

            timerFailedGuess.Tag = count;
                
            BackColor = count % 2 == 0 ? originalColor : FailColor;

            if (count == numberOfBlinking)
            {
                BackColor = originalColor;
                timerFailedGuess.Stop();
            }
        }
    }
}
