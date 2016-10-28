namespace MineSweeper
{
    partial class ErrorLog
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
            this.textBoxErrorLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxErrorLog
            // 
            this.textBoxErrorLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxErrorLog.Location = new System.Drawing.Point(0, 0);
            this.textBoxErrorLog.Multiline = true;
            this.textBoxErrorLog.Name = "textBoxErrorLog";
            this.textBoxErrorLog.ReadOnly = true;
            this.textBoxErrorLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxErrorLog.Size = new System.Drawing.Size(683, 304);
            this.textBoxErrorLog.TabIndex = 0;
            // 
            // ErrorLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 304);
            this.Controls.Add(this.textBoxErrorLog);
            this.Name = "ErrorLog";
            this.Text = "Errors";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxErrorLog;
    }
}