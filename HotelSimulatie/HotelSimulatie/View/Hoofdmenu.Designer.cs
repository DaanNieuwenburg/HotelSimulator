namespace HotelSimulatie.View
{
    partial class Hoofdmenu
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
            this.startBtn = new System.Windows.Forms.Button();
            this.LogoPbx = new System.Windows.Forms.PictureBox();
            this.optiesBtn = new System.Windows.Forms.Button();
            this.quitBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPbx)).BeginInit();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.startBtn.Location = new System.Drawing.Point(30, 140);
            this.startBtn.Margin = new System.Windows.Forms.Padding(2);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(151, 26);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            // 
            // LogoPbx
            // 
            this.LogoPbx.Location = new System.Drawing.Point(30, 12);
            this.LogoPbx.Name = "LogoPbx";
            this.LogoPbx.Size = new System.Drawing.Size(151, 123);
            this.LogoPbx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPbx.TabIndex = 1;
            this.LogoPbx.TabStop = false;
            // 
            // optiesBtn
            // 
            this.optiesBtn.Location = new System.Drawing.Point(30, 171);
            this.optiesBtn.Name = "optiesBtn";
            this.optiesBtn.Size = new System.Drawing.Size(75, 23);
            this.optiesBtn.TabIndex = 2;
            this.optiesBtn.Text = "Opties";
            this.optiesBtn.UseVisualStyleBackColor = true;
            this.optiesBtn.Click += new System.EventHandler(this.optiesBtn_Click);
            // 
            // quitBtn
            // 
            this.quitBtn.Location = new System.Drawing.Point(106, 171);
            this.quitBtn.Name = "quitBtn";
            this.quitBtn.Size = new System.Drawing.Size(75, 23);
            this.quitBtn.TabIndex = 3;
            this.quitBtn.Text = "Afsluiten";
            this.quitBtn.UseVisualStyleBackColor = true;
            this.quitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            // 
            // Hoofdmenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 206);
            this.Controls.Add(this.quitBtn);
            this.Controls.Add(this.optiesBtn);
            this.Controls.Add(this.LogoPbx);
            this.Controls.Add(this.startBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hoofdmenu";
            this.Text = "Hoofdmenu";
            ((System.ComponentModel.ISupportInitialize)(this.LogoPbx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        public System.Windows.Forms.PictureBox LogoPbx;
        private System.Windows.Forms.Button optiesBtn;
        private System.Windows.Forms.Button quitBtn;
    }
}