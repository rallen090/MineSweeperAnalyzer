using System;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class ErrorLog : Form
    {
        public ErrorLog()
        {
            InitializeComponent();
            this.CreateHandle();
        }

        public void Log(string errorText)
        {
            this.textBoxErrorLog.Text += errorText + Environment.NewLine;
        }

        public void Clear()
        {
            this.textBoxErrorLog.Text = string.Empty;
        }
    }
}
