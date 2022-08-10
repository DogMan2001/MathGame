using MathGame.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MathGame.Client
{
    enum GameDifficulty
    {
        Easy = 10,
        Medium = 18,
        Hard = 24,
    }

    public partial class Form1 : Form
    {
        IMap map;

        int score = 0;
        DateTime gameStartedDate;
        int rightguesses;


        public int NumberOfTries
        {
            get => score; set
            {
                score = value;
                labelNumberTries.Text = score.ToString();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
          

        // Called when application starts
        private void InitGame()
        {   
            if(Properties.Settings.Default.HintShown == false)
            {
                showInstructions();
                Properties.Settings.Default.HintShown = true;
            }
            // Read last selected difficulty

            GameDifficulty lastDifficulty = (GameDifficulty)Properties.Settings.Default.lastDifficoulity;
            SetGameDifficulty(lastDifficulty);
        }

        private void SetGameDifficulty(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    radioButtonEasy.Checked = true;
                    break;
                case GameDifficulty.Medium:
                    radioButtonMedium.Checked = true;
                    break;
                case GameDifficulty.Hard:
                    radioButtonHard.Checked = true;
                    break;               
            }
        }

        private GameDifficulty GetGameDifficulty()
        {
            if (radioButtonEasy.Checked)
            {
                return GameDifficulty.Easy;
            }
            else if (radioButtonMedium.Checked)
            {
                return GameDifficulty.Medium;
            }
            else
            {
                return GameDifficulty.Hard;
            }
        }

        private void NewGame()
        {
            GameDifficulty difficulty = GetGameDifficulty();

            Properties.Settings.Default.lastDifficoulity = (int)difficulty;

            //int maxValue = 9;

            //if (difficulty == GameDifficulty.Medium) maxValue = 18;
            //else if (difficulty == GameDifficulty.Hard) maxValue = 24;

            rightguesses = 0;
            // old algorithm
            map = new Map(3, 3, (int)difficulty);

            // better algorithm
            //map = new MapBettter();
            
            map.Generate();
            score = 0;

            Debug.WriteLine("Print map");

            mapPanel.Controls.Clear();

            // columns
            for (int x = 0; x < 3; x++)
            {
                // rows
                for (int y = 0; y < 3; y++)
                {
                    Position tag = new Position(x, y);
                    int number = map.GetPartOfMap(x, y);



                    GameTile tile = new GameTile();
                    tile.NumberToGuess = number;
                    tile.Tag = tag;
                    tile.BackColor = number % 2 == 0 ? Color.LightBlue : Color.Coral;
                    tile.TriedToGuess += Tile_GuessClicked;

                    mapPanel.Controls.Add(tile, x, y);

                    tile.NumberToGuessVisible = checkBoxShowNumbers.Checked;
                }
            }

            for (int y = 0; y < 3; y++)
            {
                Label sumLabel = new Label();
                sumLabel.Text = map.GetSumRow(y).ToString();

                mapPanel.Controls.Add(sumLabel, 3, y);
            }

            for (int x = 0; x < 3; x++)
            {
                Label sumLabel = new Label();
                sumLabel.Text = map.GetSumColumn(x).ToString();

                mapPanel.Controls.Add(sumLabel, x, 3);
            }

            timerGame.Start();
            gameStartedDate = DateTime.Now;
        }

        private void ShowWinnerForm()
        {
            this.timerGame.Stop();

            DateTime now = DateTime.Now;
            TimeSpan timeSpan = now - gameStartedDate;

            WinnerForm form = new WinnerForm(score, timeSpan);

            DialogResult result = form.ShowDialog();

            NewGame();
        }

        private void Tile_GuessClicked(object sender, EventArgs e)
        {
            GameTile tile = (GameTile)sender;
            if (tile.IsFinished)
            {
                score = score + 10;
                rightguesses = rightguesses + 1;
            }
            else
            {
                score--;
            }

            labelNumberTries.Text = score.ToString();

            //Check win
            if (rightguesses == 9)
            {
                this.ShowWinnerForm();
            }

            // alternativa
            // if(CheckWin()) {... winner }
        }

        private bool CheckWin()
        {
            foreach (Control x in mapPanel.Controls)
            {
                if (x is GameTile tile && tile.IsFinished == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void btnNewGame_Click(object sender, EventArgs e) => NewGame();

        private void timerGame_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = now - gameStartedDate;

            labelTime.Text = $"Time: {timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        }

        private void checkBoxShowNumbers_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control x in mapPanel.Controls)
            {
                if (x is GameTile tile && !tile.IsFinished)
                {
                    tile.NumberToGuessVisible = checkBoxShowNumbers.Checked;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowWinnerForm();

        }

        private void btnHint_Click(object sender, EventArgs e)
        {
            showInstructions();
        }

        private void showInstructions()
        {
            Instructions ins = new Instructions();
            ins.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGame();
        }
    }
}