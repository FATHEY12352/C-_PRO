namespace JointProject1
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label4 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblSurvivorsSaved = new System.Windows.Forms.Label();
            this.lblZombiesKilled = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(107, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Health";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(481, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(92, 25);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "Score: 0";
            // 
            // lblSurvivorsSaved
            // 
            this.lblSurvivorsSaved.AutoSize = true;
            this.lblSurvivorsSaved.BackColor = System.Drawing.Color.Transparent;
            this.lblSurvivorsSaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurvivorsSaved.ForeColor = System.Drawing.Color.White;
            this.lblSurvivorsSaved.Location = new System.Drawing.Point(449, 20);
            this.lblSurvivorsSaved.Name = "lblSurvivorsSaved";
            this.lblSurvivorsSaved.Size = new System.Drawing.Size(139, 20);
            this.lblSurvivorsSaved.TabIndex = 6;
            this.lblSurvivorsSaved.Text = "Survivors Saved: 0";
            // 
            // lblZombiesKilled
            // 
            this.lblZombiesKilled.AutoSize = true;
            this.lblZombiesKilled.BackColor = System.Drawing.Color.Transparent;
            this.lblZombiesKilled.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZombiesKilled.ForeColor = System.Drawing.Color.White;
            this.lblZombiesKilled.Location = new System.Drawing.Point(459, 38);
            this.lblZombiesKilled.Name = "lblZombiesKilled";
            this.lblZombiesKilled.Size = new System.Drawing.Size(126, 20);
            this.lblZombiesKilled.TabIndex = 7;
            this.lblZombiesKilled.Text = "Zombies killed: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(603, 423);
            this.Controls.Add(this.lblZombiesKilled);
            this.Controls.Add(this.lblSurvivorsSaved);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.label4);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblSurvivorsSaved;
        private System.Windows.Forms.Label lblZombiesKilled;
    }
}

