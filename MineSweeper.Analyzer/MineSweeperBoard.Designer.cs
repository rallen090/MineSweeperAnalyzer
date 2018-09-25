namespace MineSweeper
{
    sealed partial class MineSweeperBoard
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
			this.dataGridBoard = new System.Windows.Forms.DataGridView();
			this.buttonRun = new System.Windows.Forms.Button();
			this.labelStatus = new System.Windows.Forms.Label();
			this.labelSize = new System.Windows.Forms.Label();
			this.labelMineCount = new System.Windows.Forms.Label();
			this.labelRuns = new System.Windows.Forms.Label();
			this.labelRatio = new System.Windows.Forms.Label();
			this.labelSuccessRate = new System.Windows.Forms.Label();
			this.labelCurrentRun = new System.Windows.Forms.Label();
			this.labelCurrentStep = new System.Windows.Forms.Label();
			this.textBoxSizeX = new System.Windows.Forms.TextBox();
			this.textBoxSizeY = new System.Windows.Forms.TextBox();
			this.labelX = new System.Windows.Forms.Label();
			this.textBoxMineCount = new System.Windows.Forms.TextBox();
			this.textBoxRunCount = new System.Windows.Forms.TextBox();
			this.comboBoxCurrentRun = new System.Windows.Forms.ComboBox();
			this.comboBoxCurrentStep = new System.Windows.Forms.ComboBox();
			this.labelSolver = new System.Windows.Forms.Label();
			this.comboBoxSolver = new System.Windows.Forms.ComboBox();
			this.buttonToggleErrorLog = new System.Windows.Forms.Button();
			this.buttonRerun = new System.Windows.Forms.Button();
			this.checkBoxRealTimeRedraw = new System.Windows.Forms.CheckBox();
			this.buttonSaveJson = new System.Windows.Forms.Button();
			this.buttonLoadJson = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridBoard)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridBoard
			// 
			this.dataGridBoard.AllowUserToAddRows = false;
			this.dataGridBoard.AllowUserToDeleteRows = false;
			this.dataGridBoard.AllowUserToResizeColumns = false;
			this.dataGridBoard.AllowUserToResizeRows = false;
			this.dataGridBoard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridBoard.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridBoard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			this.dataGridBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridBoard.Location = new System.Drawing.Point(13, 133);
			this.dataGridBoard.Name = "dataGridBoard";
			this.dataGridBoard.ReadOnly = true;
			this.dataGridBoard.Size = new System.Drawing.Size(608, 429);
			this.dataGridBoard.TabIndex = 0;
			this.dataGridBoard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridBoard_KeyDown);
			// 
			// buttonRun
			// 
			this.buttonRun.Location = new System.Drawing.Point(13, 13);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(160, 85);
			this.buttonRun.TabIndex = 1;
			this.buttonRun.Text = "Run";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
			// 
			// labelStatus
			// 
			this.labelStatus.AutoSize = true;
			this.labelStatus.Location = new System.Drawing.Point(371, 13);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(43, 13);
			this.labelStatus.TabIndex = 2;
			this.labelStatus.Text = "Status: ";
			// 
			// labelSize
			// 
			this.labelSize.AutoSize = true;
			this.labelSize.Location = new System.Drawing.Point(179, 13);
			this.labelSize.Name = "labelSize";
			this.labelSize.Size = new System.Drawing.Size(30, 13);
			this.labelSize.TabIndex = 3;
			this.labelSize.Text = "Size:";
			// 
			// labelMineCount
			// 
			this.labelMineCount.AutoSize = true;
			this.labelMineCount.Location = new System.Drawing.Point(179, 36);
			this.labelMineCount.Name = "labelMineCount";
			this.labelMineCount.Size = new System.Drawing.Size(38, 13);
			this.labelMineCount.TabIndex = 4;
			this.labelMineCount.Text = "Mines:";
			// 
			// labelRuns
			// 
			this.labelRuns.AutoSize = true;
			this.labelRuns.Location = new System.Drawing.Point(179, 59);
			this.labelRuns.Name = "labelRuns";
			this.labelRuns.Size = new System.Drawing.Size(35, 13);
			this.labelRuns.TabIndex = 5;
			this.labelRuns.Text = "Runs:";
			// 
			// labelRatio
			// 
			this.labelRatio.AutoSize = true;
			this.labelRatio.Location = new System.Drawing.Point(371, 36);
			this.labelRatio.Name = "labelRatio";
			this.labelRatio.Size = new System.Drawing.Size(74, 13);
			this.labelRatio.TabIndex = 6;
			this.labelRatio.Text = "Success ratio:";
			// 
			// labelSuccessRate
			// 
			this.labelSuccessRate.AutoSize = true;
			this.labelSuccessRate.Location = new System.Drawing.Point(371, 59);
			this.labelSuccessRate.Name = "labelSuccessRate";
			this.labelSuccessRate.Size = new System.Drawing.Size(72, 13);
			this.labelSuccessRate.TabIndex = 7;
			this.labelSuccessRate.Text = "Success rate:";
			// 
			// labelCurrentRun
			// 
			this.labelCurrentRun.AutoSize = true;
			this.labelCurrentRun.Location = new System.Drawing.Point(500, 13);
			this.labelCurrentRun.Name = "labelCurrentRun";
			this.labelCurrentRun.Size = new System.Drawing.Size(62, 13);
			this.labelCurrentRun.TabIndex = 8;
			this.labelCurrentRun.Text = "Current run:";
			// 
			// labelCurrentStep
			// 
			this.labelCurrentStep.AutoSize = true;
			this.labelCurrentStep.Location = new System.Drawing.Point(500, 36);
			this.labelCurrentStep.Name = "labelCurrentStep";
			this.labelCurrentStep.Size = new System.Drawing.Size(67, 13);
			this.labelCurrentStep.TabIndex = 9;
			this.labelCurrentStep.Text = "Current step:";
			// 
			// textBoxSizeX
			// 
			this.textBoxSizeX.Location = new System.Drawing.Point(226, 10);
			this.textBoxSizeX.Name = "textBoxSizeX";
			this.textBoxSizeX.Size = new System.Drawing.Size(29, 20);
			this.textBoxSizeX.TabIndex = 10;
			this.textBoxSizeX.Text = "10";
			this.textBoxSizeX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBoxSizeY
			// 
			this.textBoxSizeY.Location = new System.Drawing.Point(279, 10);
			this.textBoxSizeY.Name = "textBoxSizeY";
			this.textBoxSizeY.Size = new System.Drawing.Size(29, 20);
			this.textBoxSizeY.TabIndex = 11;
			this.textBoxSizeY.Text = "10";
			this.textBoxSizeY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelX
			// 
			this.labelX.AutoSize = true;
			this.labelX.Location = new System.Drawing.Point(261, 15);
			this.labelX.Name = "labelX";
			this.labelX.Size = new System.Drawing.Size(12, 13);
			this.labelX.TabIndex = 12;
			this.labelX.Text = "x";
			// 
			// textBoxMineCount
			// 
			this.textBoxMineCount.Location = new System.Drawing.Point(226, 33);
			this.textBoxMineCount.Name = "textBoxMineCount";
			this.textBoxMineCount.Size = new System.Drawing.Size(29, 20);
			this.textBoxMineCount.TabIndex = 13;
			this.textBoxMineCount.Text = "10";
			this.textBoxMineCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBoxRunCount
			// 
			this.textBoxRunCount.Location = new System.Drawing.Point(226, 56);
			this.textBoxRunCount.Name = "textBoxRunCount";
			this.textBoxRunCount.Size = new System.Drawing.Size(29, 20);
			this.textBoxRunCount.TabIndex = 14;
			this.textBoxRunCount.Text = "10";
			this.textBoxRunCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// comboBoxCurrentRun
			// 
			this.comboBoxCurrentRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCurrentRun.FormattingEnabled = true;
			this.comboBoxCurrentRun.Location = new System.Drawing.Point(568, 9);
			this.comboBoxCurrentRun.Name = "comboBoxCurrentRun";
			this.comboBoxCurrentRun.Size = new System.Drawing.Size(43, 21);
			this.comboBoxCurrentRun.TabIndex = 15;
			this.comboBoxCurrentRun.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrentRun_SelectedIndexChanged);
			// 
			// comboBoxCurrentStep
			// 
			this.comboBoxCurrentStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCurrentStep.FormattingEnabled = true;
			this.comboBoxCurrentStep.Location = new System.Drawing.Point(568, 32);
			this.comboBoxCurrentStep.Name = "comboBoxCurrentStep";
			this.comboBoxCurrentStep.Size = new System.Drawing.Size(43, 21);
			this.comboBoxCurrentStep.TabIndex = 16;
			this.comboBoxCurrentStep.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrentStep_SelectedIndexChanged);
			// 
			// labelSolver
			// 
			this.labelSolver.AutoSize = true;
			this.labelSolver.Location = new System.Drawing.Point(180, 85);
			this.labelSolver.Name = "labelSolver";
			this.labelSolver.Size = new System.Drawing.Size(40, 13);
			this.labelSolver.TabIndex = 17;
			this.labelSolver.Text = "Solver:";
			// 
			// comboBoxSolver
			// 
			this.comboBoxSolver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSolver.FormattingEnabled = true;
			this.comboBoxSolver.Items.AddRange(new object[] {
            "LocalOptimized",
            "LocalBase",
            "LocalRandom",
            "Python",
            "Java",
            "JavaScript (NodeJS)",
            "CSharp"});
			this.comboBoxSolver.Location = new System.Drawing.Point(226, 82);
			this.comboBoxSolver.Name = "comboBoxSolver";
			this.comboBoxSolver.Size = new System.Drawing.Size(121, 21);
			this.comboBoxSolver.TabIndex = 18;
			// 
			// buttonToggleErrorLog
			// 
			this.buttonToggleErrorLog.Location = new System.Drawing.Point(14, 104);
			this.buttonToggleErrorLog.Name = "buttonToggleErrorLog";
			this.buttonToggleErrorLog.Size = new System.Drawing.Size(160, 23);
			this.buttonToggleErrorLog.TabIndex = 19;
			this.buttonToggleErrorLog.Text = "View errors";
			this.buttonToggleErrorLog.UseVisualStyleBackColor = true;
			this.buttonToggleErrorLog.Click += new System.EventHandler(this.buttonToggleErrorLog_Click);
			// 
			// buttonRerun
			// 
			this.buttonRerun.Location = new System.Drawing.Point(503, 55);
			this.buttonRerun.Name = "buttonRerun";
			this.buttonRerun.Size = new System.Drawing.Size(108, 21);
			this.buttonRerun.TabIndex = 20;
			this.buttonRerun.Text = "Rerun";
			this.buttonRerun.UseVisualStyleBackColor = true;
			this.buttonRerun.Click += new System.EventHandler(this.buttonRerun_Click);
			// 
			// checkBoxRealTimeRedraw
			// 
			this.checkBoxRealTimeRedraw.AutoSize = true;
			this.checkBoxRealTimeRedraw.Checked = true;
			this.checkBoxRealTimeRedraw.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxRealTimeRedraw.Location = new System.Drawing.Point(183, 108);
			this.checkBoxRealTimeRedraw.Name = "checkBoxRealTimeRedraw";
			this.checkBoxRealTimeRedraw.Size = new System.Drawing.Size(105, 17);
			this.checkBoxRealTimeRedraw.TabIndex = 21;
			this.checkBoxRealTimeRedraw.Text = "Real-time redraw";
			this.checkBoxRealTimeRedraw.UseVisualStyleBackColor = true;
			// 
			// buttonSaveJson
			// 
			this.buttonSaveJson.Location = new System.Drawing.Point(503, 85);
			this.buttonSaveJson.Name = "buttonSaveJson";
			this.buttonSaveJson.Size = new System.Drawing.Size(46, 21);
			this.buttonSaveJson.TabIndex = 22;
			this.buttonSaveJson.Text = "Save";
			this.buttonSaveJson.UseVisualStyleBackColor = true;
			this.buttonSaveJson.Click += new System.EventHandler(this.buttonSaveJson_Click);
			// 
			// buttonLoadJson
			// 
			this.buttonLoadJson.Location = new System.Drawing.Point(568, 85);
			this.buttonLoadJson.Name = "buttonLoadJson";
			this.buttonLoadJson.Size = new System.Drawing.Size(43, 21);
			this.buttonLoadJson.TabIndex = 23;
			this.buttonLoadJson.Text = "Load";
			this.buttonLoadJson.UseVisualStyleBackColor = true;
			this.buttonLoadJson.Click += new System.EventHandler(this.buttonLoadJson_Click);
			// 
			// MineSweeperBoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(633, 567);
			this.Controls.Add(this.buttonLoadJson);
			this.Controls.Add(this.buttonSaveJson);
			this.Controls.Add(this.checkBoxRealTimeRedraw);
			this.Controls.Add(this.buttonRerun);
			this.Controls.Add(this.buttonToggleErrorLog);
			this.Controls.Add(this.comboBoxSolver);
			this.Controls.Add(this.labelSolver);
			this.Controls.Add(this.comboBoxCurrentStep);
			this.Controls.Add(this.comboBoxCurrentRun);
			this.Controls.Add(this.textBoxRunCount);
			this.Controls.Add(this.textBoxMineCount);
			this.Controls.Add(this.labelX);
			this.Controls.Add(this.textBoxSizeY);
			this.Controls.Add(this.textBoxSizeX);
			this.Controls.Add(this.labelCurrentStep);
			this.Controls.Add(this.labelCurrentRun);
			this.Controls.Add(this.labelSuccessRate);
			this.Controls.Add(this.labelRatio);
			this.Controls.Add(this.labelRuns);
			this.Controls.Add(this.labelMineCount);
			this.Controls.Add(this.labelSize);
			this.Controls.Add(this.labelStatus);
			this.Controls.Add(this.buttonRun);
			this.Controls.Add(this.dataGridBoard);
			this.Name = "MineSweeperBoard";
			this.Text = "MineSweeper Analyzer";
			((System.ComponentModel.ISupportInitialize)(this.dataGridBoard)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridBoard;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelMineCount;
        private System.Windows.Forms.Label labelRuns;
        private System.Windows.Forms.Label labelRatio;
        private System.Windows.Forms.Label labelSuccessRate;
        private System.Windows.Forms.Label labelCurrentRun;
        private System.Windows.Forms.Label labelCurrentStep;
        private System.Windows.Forms.TextBox textBoxSizeX;
        private System.Windows.Forms.TextBox textBoxSizeY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.TextBox textBoxMineCount;
        private System.Windows.Forms.TextBox textBoxRunCount;
        private System.Windows.Forms.ComboBox comboBoxCurrentRun;
        private System.Windows.Forms.ComboBox comboBoxCurrentStep;
        private System.Windows.Forms.Label labelSolver;
        private System.Windows.Forms.ComboBox comboBoxSolver;
        private System.Windows.Forms.Button buttonToggleErrorLog;
        private System.Windows.Forms.Button buttonRerun;
        private System.Windows.Forms.CheckBox checkBoxRealTimeRedraw;
		private System.Windows.Forms.Button buttonSaveJson;
		private System.Windows.Forms.Button buttonLoadJson;
	}
}

