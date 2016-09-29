using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Logic;
using MineSweeper.Models;
using MineSweeper.Solvers;

namespace MineSweeper
{
    public partial class MineSweeperBoard : Form
    {
        private GameRunner _gameRunner;
        private ErrorLog _errorLog;

        public MineSweeperBoard()
        {
            InitializeComponent();
            
            // set up error log
            this._errorLog = new ErrorLog();
            this.AddOwnedForm(this._errorLog);
            this._errorLog.Visible = false; // initially invisible
            this._errorLog.ControlBox = false; // no close button

            //// set the default solver
            this.comboBoxSolver.SelectedIndex = 0;
        }

        public void InitializeGrid(Cell[,] grid)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.ColumnCount = grid.GetLength(1);
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                var row = new DataGridViewRow();
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell
                    {
                        Value = grid[y, x]
                    });
                    row.Cells[x].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                this.dataGridView1.Rows.Add(row);
            }

            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.RowHeadersVisible = false;
        }

        public void RedrawGrid(Cell[,] grid)
        {
            this.dataGridView1.ColumnCount = grid.GetLength(1);
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                var row = this.dataGridView1.Rows[y];
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    row.Cells[x].Value = grid[y, x];
                }
            }
        }

        public void UpdateRunInfo(List<Game> games)
        {
            if (games.Count > 0)
            {
                var totalGames = games.Count;
                var successes = games.Count(g => g.Success);
                this.comboBoxCurrentRun.Items.Clear();
                Enumerable.Range(1, totalGames).ToList().ForEach(i => this.comboBoxCurrentRun.Items.Add(i.ToString()));
                this.comboBoxCurrentRun.SelectedIndex = 0;
                this.labelRatio.Text = $"Success ratio: {successes / totalGames * 100}%";
                this.labelSuccessRate.Text = $"Success rate: {successes}/{totalGames}";
            }
        }

        public void CompleteRunInfo()
        {
            this.labelStatus.Text = "Status: Complete";
        }

        public void LogError(string error)
        {
            this._errorLog.Invoke((MethodInvoker)delegate { this._errorLog.Log(error); });
        }

        #region ---- UI Events ----

        private void buttonRun_Click(object sender, EventArgs e)
        {
            this.labelStatus.Text = "Status: Running";
            this._errorLog.Clear();

            var xSize = int.Parse(this.textBoxSizeX.Text);
            var ySize = int.Parse(this.textBoxSizeY.Text);
            var mineCount = int.Parse(this.textBoxMineCount.Text);
            var runCount = int.Parse(this.textBoxRunCount.Text);

            var solverType = this.comboBoxSolver.SelectedItem.ToString();
            var runner = new GameRunner(xSize, ySize, mineCount, runCount, this, SolverSelector.GetSolverFactory(solverType));
            this._gameRunner = runner;
            Task.Run(() => runner.RunAll());
        }

        private void comboBoxCurrentRun_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set step counts
            var runIndex = int.Parse(this.comboBoxCurrentRun.Text) - 1;
            var run = this._gameRunner.GetGames().ElementAt(runIndex);
            this.comboBoxCurrentStep.Items.Clear();
            Enumerable.Range(1, run.Steps.Count).ToList().ForEach(i => this.comboBoxCurrentStep.Items.Add(i.ToString()));

            var stepIndex = run.Steps.Count - 1;
            this.comboBoxCurrentStep.SelectedIndex = stepIndex;
            this.RedrawGrid(run.Steps.ElementAt(stepIndex));
        }

        private void comboBoxCurrentStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            var runIndex = int.Parse(this.comboBoxCurrentRun.Text) - 1;
            var stepIndex = int.Parse(this.comboBoxCurrentStep.Text) - 1;
            var run = this._gameRunner.GetGames().ElementAt(runIndex);

            this.RedrawGrid(run.Steps.ElementAt(stepIndex));
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        private void MineSweeperBoard_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    this.comboBoxCurrentStep.SelectedIndex = Math.Max(this.comboBoxCurrentStep.SelectedIndex - 1, 0);
                    this.comboBoxCurrentStep_SelectedIndexChanged(sender, e);
                    break;
                case Keys.Right:
                    this.comboBoxCurrentStep.SelectedIndex = Math.Min(this.comboBoxCurrentStep.SelectedIndex + 1, this.comboBoxCurrentStep.Items.Count - 1);
                    this.comboBoxCurrentStep_SelectedIndexChanged(sender, e);
                    break;
                case Keys.Up:
                    this.comboBoxCurrentRun.SelectedIndex = Math.Min(this.comboBoxCurrentRun.SelectedIndex + 1, this.comboBoxCurrentRun.Items.Count - 1);
                    this.comboBoxCurrentRun_SelectedIndexChanged(sender, e);
                    break;
                case Keys.Down:
                    this.comboBoxCurrentRun.SelectedIndex = Math.Max(this.comboBoxCurrentRun.SelectedIndex - 1, 0);
                    this.comboBoxCurrentRun_SelectedIndexChanged(sender, e);
                    break;
            }
        }

        private void buttonToggleErrorLog_Click(object sender, EventArgs e)
        {
            if (this._errorLog.Visible)
            {
                this._errorLog.Hide();
                this.buttonToggleErrorLog.Text = "View errors";
            }
            else
            {
                this._errorLog.Show();
                this.buttonToggleErrorLog.Text = "Hide errors";
            }
        }

        #endregion
    }
}
