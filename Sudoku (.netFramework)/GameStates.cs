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
            SudokuSize = InitSize(size);
            Squares = new SudokuSquare(SudokuSize);
            CountOfWin = countOfWin;
            CountOfFail = countOfFail;
        }

        public SudokuSquare Squares { get; private set; }
        public int CountOfWin { get; set; }
        public int CountOfFail { get; set; }
        public int SudokuSize { get; set; }

        private static int InitSize(BoardsEnum sizes)
        {
            switch (sizes)
            {
                case BoardsEnum.X4:
                    return 4;
                case BoardsEnum.X9:
                    return 9;
                case BoardsEnum.X16:
                    return 16;
                case BoardsEnum.X25:
                    return 25;
                default:
                    return 0;
            }
        }
    }
}
