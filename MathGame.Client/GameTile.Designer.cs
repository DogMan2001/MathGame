
namespace MathGame.Client
{
    partial class GameTile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelNumberToGuess = new System.Windows.Forms.Label();
            this.guessNumberTextBox = new System.Windows.Forms.MaskedTextBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.timerFailedGuess = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelNumberToGuess
            // 
            this.labelNumberToGuess.AutoSize = true;
            this.labelNumberToGuess.Location = new System.Drawing.Point(4, 4);
            this.labelNumberToGuess.Name = "labelNumberToGuess";
            this.labelNumberToGuess.Size = new System.Drawing.Size(59, 25);
            this.labelNumberToGuess.TabIndex = 0;
            this.labelNumberToGuess.Text = "label1";
            // 
            // guessNumberTextBox
            // 
            this.guessNumberTextBox.Location = new System.Drawing.Point(4, 33);
            this.guessNumberTextBox.Mask = "09";
            this.guessNumberTextBox.Name = "guessNumberTextBox";
            this.guessNumberTextBox.Size = new System.Drawing.Size(82, 31);
            this.guessNumberTextBox.TabIndex = 1;
            this.guessNumberTextBox.TextChanged += new System.EventHandler(this.guessNumberTextBox_TextChanged);
            this.guessNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.guessNumberTextBox_KeyPress);
            this.guessNumberTextBox.Leave += new System.EventHandler(this.guessNumberTextBox_Leave);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(0, 71);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(86, 34);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "Guess";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // timerFailedGuess
            // 
            this.timerFailedGuess.Interval = 150;
            this.timerFailedGuess.Tick += new System.EventHandler(this.timerFailedGuess_Tick);
            // 
            // GameTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.guessNumberTextBox);
            this.Controls.Add(this.labelNumberToGuess);
            this.Name = "GameTile";
            this.Size = new System.Drawing.Size(99, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNumberToGuess;
        private System.Windows.Forms.MaskedTextBox guessNumberTextBox;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Timer timerFailedGuess;
    }
}
