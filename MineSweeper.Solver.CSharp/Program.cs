using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Solver.CSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var adapter = new RemoteAdapter(new Solver());
            adapter.HandleMessages();
        }
    }
}
