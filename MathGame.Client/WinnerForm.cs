using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MathGame.Client
{
    public partial class WinnerForm : Form
    {
        Random rand = new Random();

        int score;
        TimeSpan timeLeft;

        public WinnerForm(int score, TimeSpan timeLeft)
        {
            InitializeComponent();

            this.score = score;
            this.timeLeft = timeLeft;

            this.labelScore.Text = score.ToString();
            this.labelTime.Text = $"{timeLeft.Hours}:{timeLeft.Minutes}:{timeLeft.Seconds}";
        }

        private void timerChangeColor_Tick(object sender, EventArgs e)
        {
            label1.ForeColor = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
            label1.BackColor = Color.FromArgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
        }
    }
}
