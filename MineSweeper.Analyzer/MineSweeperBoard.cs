using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Logic;
using MineSweeper.Models;
using MineSweeper.Solvers;
using Newtonsoft.Json;

namespace MineSweeper
{
    public sealed partial class MineSweeperBoard : Form
    {
        private GameRunner _gameRunner;
        private readonly ErrorLog _errorLog;
        private Task _runnerTask;

        public MineSweeperBoard()
        {
            InitializeComponent();

            // for very dynamic layouts which are updated frequently on the form
            this.DoubleBuffered = true;
            
            // set up error log
            this._errorLog = new ErrorLog();
            this.AddOwnedForm(this._errorLog);
            this._errorLog.Visible = false; // initially invisible
            this._errorLog.ControlBox = false; // no close button

            // set the default solver
            this.comboBoxSolver.SelectedIndex = 0;

            // disable highlighting of the grid
            this.dataGridBoard.DefaultCellStyle.SelectionBackColor = this.dataGridBoard.DefaultCellStyle.BackColor;
            this.dataGridBoard.DefaultCellStyle.SelectionForeColor = this.dataGridBoard.DefaultCellStyle.ForeColor;
        }

        public void InitializeGrid(Cell[,] grid)
        {
            this.dataGridBoard.Rows.Clear();
            this.dataGridBoard.ColumnCount = grid.GetLength(1);
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                var row = new DataGridViewRow();
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell
                    {
                        Value = grid[y, x]
                    });

                    // set the cell styling
                    var style = row.Cells[x].Style;
                    style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    style.Font = new Font(DefaultFont, FontStyle.Bold);
                }
                this.dataGridBoard.Rows.Add(row);
            }

            this.dataGridBoard.ColumnHeadersVisible = false;
            this.dataGridBoard.RowHeadersVisible = false;
        }

        public void RedrawGrid(Cell[,] grid)
        {
            // redraw enabled is (A) there is an active run and real-time redraw is turned on, or (B) there is not an active run
            if (this.checkBoxRealTimeRedraw.Checked || this._runnerTask.IsCompleted)
            {
                this.SuspendLayout();
                this.dataGridBoard.ColumnCount = grid.GetLength(1);
                for (var y = 0; y < grid.GetLength(0); y++)
                {
                    var row = this.dataGridBoard.Rows[y];
                    for (var x = 0; x < grid.GetLength(1); x++)
                    {
                        var cell = grid[y, x];
                        row.Cells[x].Value = cell;
                        row.Cells[x].Style.ForeColor = GetColorStyleByCell(cell);
                    }
                }
                this.ResumeLayout();
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
                this.comboBoxCurrentRun.SelectedIndex = games.Count - 1;
                this.labelRatio.Text = $"Success ratio: {Math.Round((double)successes / totalGames * 100, 2)}%";
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

        private static Color GetColorStyleByCell(Cell cell)
        {
            var color = DefaultForeColor;
            switch (cell.State)
            {
                case CellState.Hidden:
                    color = Color.Black;
                    break;
                case CellState.Revealed:
                    color = cell.IsMine ? Color.Red : GetColorByValue(cell.Value);
                    break;
                case CellState.Flagged:
                    color = Color.DarkRed;
                    break;
            }
            return color;
        }

        private static Color GetColorByValue(int value)
        {
            switch (value)
            {
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.OrangeRed;
                case 4:
                    return Color.Purple;
                case 5:
                    return Color.Brown;
                case 6:
                    return Color.Cyan;
                case 7:
                    return Color.Black;
                case 8:
                    return Color.LightSlateGray;
                default:
                    return Color.DeepPink;
            }
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
            this._runnerTask = Task.Run(() => runner.RunAll());
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

        private void buttonRerun_Click(object sender, EventArgs e)
        {
	        if (this.comboBoxCurrentRun?.SelectedIndex != null)
	        {
		        var selectedRunId = this.comboBoxCurrentRun.SelectedIndex;
				this._runnerTask = Task.Run(() => this._gameRunner.Rerun(selectedRunId));
			}
        }

        #endregion

        private void dataGridBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (this._gameRunner?.GetGames()?.Count > 0)
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
        }

		private void buttonSaveJson_Click(object sender, EventArgs e)
		{
			if (this.comboBoxCurrentRun?.SelectedIndex != null)
			{
				var json = this._gameRunner.GetGameGridJson(this.comboBoxCurrentRun.SelectedIndex);
				Clipboard.SetText(json);
			}
		}

		private void buttonLoadJson_Click(object sender, EventArgs e)
		{
			var clipboardText = Clipboard.GetText();
			var grid = JsonConvert.DeserializeObject<Cell[,]>(clipboardText);
			if (this._gameRunner == null)
			{
				var xSize = grid.GetLength(0);
				var ySize = grid.GetLength(1);
				var mineCount = grid.Cast<Cell>().Count(c => c.IsMine);
				var solverType = this.comboBoxSolver.SelectedItem.ToString();
				this._gameRunner = new GameRunner(xSize, ySize, mineCount, 1, this, SolverSelector.GetSolverFactory(solverType));
			}
			this._runnerTask = Task.Run(() => this._gameRunner.LoadGameGridJson(grid));
		}
	}
}
