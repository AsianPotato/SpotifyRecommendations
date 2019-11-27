namespace Spotify_Recommendations
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
            this.search = new System.Windows.Forms.Button();
            this.playlists = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.keyword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(22, 35);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(119, 23);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.Search_Click);
            // 
            // playlists
            // 
            this.playlists.FormattingEnabled = true;
            this.playlists.Location = new System.Drawing.Point(199, 32);
            this.playlists.Name = "playlists";
            this.playlists.Size = new System.Drawing.Size(225, 342);
            this.playlists.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Playlists";
            // 
            // keyword
            // 
            this.keyword.Location = new System.Drawing.Point(22, 9);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(119, 20);
            this.keyword.TabIndex = 4;
            this.keyword.Text = "Enter your keyword...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 386);
            this.Controls.Add(this.keyword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.playlists);
            this.Controls.Add(this.search);
            this.Name = "Form1";
            this.Text = "Spotify Recommendations";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.ListBox playlists;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyword;
    }
}

