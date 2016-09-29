using System;
using System.Collections.Generic;

namespace MineSweeper.Models
{
    public class Game
    {
        public List<Cell[,]> Steps { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }

        public Game()
        {
            this.Steps = new List<Cell[,]>();
        }
    }
}
