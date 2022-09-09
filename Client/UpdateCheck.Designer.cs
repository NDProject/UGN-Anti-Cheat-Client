namespace UGN_Client
{
    partial class UpdateCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateCheck));
            this.label10 = new System.Windows.Forms.Label();
            this.youtube = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.youtube)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(51, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 18);
            this.label10.TabIndex = 6;
            this.label10.Text = "Downloading Update";
            // 
            // youtube
            // 
            this.youtube.BackColor = System.Drawing.Color.Transparent;
            this.youtube.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.youtube.Cursor = System.Windows.Forms.Cursors.Hand;
            this.youtube.Image = ((System.Drawing.Image)(resources.GetObject("youtube.Image")));
            this.youtube.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.youtube.Location = new System.Drawing.Point(120, 44);
            this.youtube.Name = "youtube";
            this.youtube.Size = new System.Drawing.Size(47, 47);
            this.youtube.TabIndex = 111;
            this.youtube.TabStop = false;
            // 
            // UpdateCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.youtube);
            this.Controls.Add(this.label10);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UGN Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.youtube)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox youtube;
    }
}