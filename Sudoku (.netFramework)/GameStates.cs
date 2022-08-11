using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class GameStates
    {
        public GameStates(BoardsEnum size, int countOfWin = 0, int countOfFail = 0)
        {
            SudokuSize = size;
            CountOfWin = countOfWin;
            CountOfFail = countOfFail;
            Squares = new SudokuSquare(SudokuSize);
        }

        public SudokuSquare Squares { get; private set; }
        public int CountOfWin { get; set; }
        public int CountOfFail { get; set; }
        public BoardsEnum SudokuSize { get; set; }
    }
}
