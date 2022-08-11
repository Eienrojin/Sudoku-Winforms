using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Creator
{
    internal abstract class SudokuCreator : ISudokuCreator
    {
        public abstract void CreateSudoku();
    }
}
