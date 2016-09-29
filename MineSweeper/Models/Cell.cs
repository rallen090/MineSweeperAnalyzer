using System;
using Newtonsoft.Json;

namespace MineSweeper.Models
{
    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        [JsonIgnore]
        public bool IsMine { get; private set; }

        public int Value { get; set; }
        public CellState State { get; set; }

        public Cell(int x, int y, bool isMine = false)
        {
            this.X = x;
            this.Y = y;
            this.IsMine = isMine;
        }

        public override string ToString()
        {
            switch (this.State)
            {
                case CellState.Hidden:
                    return "[]";
                case CellState.Flagged:
                    return "F";
                case CellState.Revealed:
                    if (IsMine)
                    {
                        return "X";
                    }
                    
                    return this.Value == 0 ? " " : this.Value.ToString();
                default:
                    throw new Exception("Invalid cell state");
            }
        }
    }

    public enum CellState
    {
        Hidden = 0,
        Flagged = 1,
        Revealed = 2
    }
}
