using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

//Класс, разбивающий доску судоку на квадраты
//Работа класса:
/*
 * Определяется размер квадрата по размеру судоку
 * Вычисляются индексы начала квадратов
 * Во время игры проверяется, к какому квадрату принадлежит число с помощью индексов начала квадрата
 */


/*
 * Как работает массив Indexes
 * По индексу [,0] - находится индекс начала квадрата по вертикали
 * По индексу [,1] - находится индекс начала квадрата по горизонтали
 * По [этому, ] индексу находится индекс квадрата, начиная с первого
 */

/*
 * ._._.
 * |_|_|
 * |_|X|
 *  Индекс X можно найти в массиве на позициях 
 *  [3, 0] - высота
 *  [3, 1] - ширина
 */
namespace Sudoku
{
    internal class SudokuSquare
    {
        public SudokuSquare(BoardsEnum sudokuSize)
        {
            VerticalSize = (int)sudokuSize;
            SizeOfSquare = (int)Math.Sqrt((int)sudokuSize);
            BoardSize = sudokuSize;
            MaxNumber = (int)sudokuSize;
            Indexes = new byte[VerticalSize, 2];

            CalculateParentIndexes();
        }

        public byte[,] Indexes { get; set; }
        public int MaxNumber { get; private set; }
        public int SizeOfSquare { get; private set; }
        public int VerticalSize { get; private set; }
        public static int NullValue { get; } = 0;
        public BoardsEnum BoardSize { get; private set; }
        public Color SquareColor { get; set; }


        private void CalculateParentIndexes()
        {
            byte skipStep = (byte)SizeOfSquare;

            InitVerticalIndexes(skipStep);
            InitHorizontalIndexes(skipStep);
        }

        private void InitHorizontalIndexes(byte skipStep)
        {
            byte vertLength     = (byte)SizeOfSquare;
            byte verticalIndex  = 0;
            const byte row      = 1;

            for (int j = 0; j < vertLength; j++)
            {
                byte horizontalSudokuIndex = 0;

                for (int sudokuHorizIndex = 0; sudokuHorizIndex < vertLength; sudokuHorizIndex++)
                {
                    Indexes[verticalIndex, row] = horizontalSudokuIndex;
                    verticalIndex++;
                    horizontalSudokuIndex += skipStep;
                }
            }
        }

        private void InitVerticalIndexes(byte skipStep)
        {
            byte vertLength          = (byte)SizeOfSquare;
            byte verticalSudokuIndex = 0;
            byte verticalIndex       = 0;
            const byte column        = 0;

            for (byte i = 0; i < vertLength; i++)
            {
                for (byte sudokuVerticalIndex = 0; sudokuVerticalIndex < vertLength; sudokuVerticalIndex++)
                {
                    Indexes[verticalIndex, column] = verticalSudokuIndex;
                    verticalIndex++;
                }

                verticalSudokuIndex += skipStep;
            }
        }

        /// <summary>
        /// Finds nearest parent sudoku-square's index to cell
        /// </summary>
        /// <param name="currentCellColumn">Cell's column index</param>
        /// <param name="currentCellRow">Cell's row index</param>
        /// <returns>array with [0] - column, [1] - row</returns>
        private (int, int) FindParentIndex(int currentCellColumn, int currentCellRow)
        {
            byte skipStep     = (byte)SizeOfSquare;
            byte parentIndexX = 0;
            byte parentIndexY = 0;
            byte numOfSquareY = 0;
            const byte column = 0;
            const byte row    = 1;

            for (int squareNumX = 0; squareNumX < SizeOfSquare; squareNumX++)
            {
                if (Indexes[numOfSquareY, column] <= currentCellColumn)
                    parentIndexY = Indexes[numOfSquareY, column];

                if (Indexes[squareNumX, row] <= currentCellRow)
                    parentIndexX = Indexes[squareNumX, row];

                numOfSquareY += skipStep;
            }

            (int, int) parentIndexes = (parentIndexY, parentIndexX);

            return parentIndexes;
        }

        /// <summary>
        /// Paints DataGridView cells with chess pattern.
        /// Works only with not even-size boards.
        /// For even-size boards use PaintDGVWithPattern()
        /// </summary>
        /// <param name="dgv">DataGridView to paint</param>
        public void PaintDGVLikeChess(DataGridView dgv)
        {
            if (VerticalSize % 2 == 0)
                PaintDGVWithPattern(dgv, InitArrayWithChessPatternIndexes());
            else
                for (int squareIndex = 1; squareIndex < Indexes.GetLength(0); squareIndex++)
                    if (squareIndex % 2 != 0)
                        Painting(dgv, Indexes[squareIndex, 0], Indexes[squareIndex, 1]);
        }

        /// <summary>
        /// Paints DataGridView with prepared pattern. Takes color from SquareColor
        /// </summary>
        /// <param name="dgv">DataGridView to paint</param>
        /// <param name="squaresToPaint">Sorted array with sudoku-squares indexes</param>
        private void PaintDGVWithPattern(DataGridView dgv, int[] squaresToPaint)
        {
            for (int i = 0; i < squaresToPaint.Length; i++)
                Painting(dgv, Indexes[squaresToPaint[i], 0], Indexes[squaresToPaint[i], 1]);
        }

        /// <summary>
        /// Inits hardcoded array for chess paint pattern
        /// </summary>
        /// <returns>array which contains sudoku-square indexes</returns>
        /// <exception cref="Exception"></exception>
        private int[] InitArrayWithChessPatternIndexes()
        {
            switch (BoardSize)
            {
                case BoardsEnum.X16:
                    return new int[] { 1, 3, 4, 6, 9, 11, 12, 14 };
                case BoardsEnum.X4:
                    return new int[] { 1, 2 };
                default:
                    throw new Exception("Method can't find any correct size from BoardsEnum");
            }
        }

        /// <summary>
        /// Paints one sudoku-square.
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="startX">X index from which the painting should start</param>
        /// <param name="startY">Y index from which the painting should start</param>
        private void Painting(DataGridView dgv, int startX, int startY)
        {
            int limitationY = SizeOfSquare + startY;
            int limitationX = SizeOfSquare + startX;
            int tempStartX  = startX;

            for (; startY < limitationY; startY++)
            {
                for (; startX < limitationX; startX++)
                {
                    dgv[startY, startX].Style.BackColor = SquareColor;
                }
                startX = tempStartX;
            }
        }

        public bool DoesNumberInSudokuSquareRepeated(DataGridView dgv, int currentCellColumn, int currentCellRow)
        {
            var nearestParentIndex = FindParentIndex(currentCellColumn, currentCellRow);
            int currentValue = Convert.ToInt16(dgv[currentCellColumn, currentCellRow].Value);
            int limitationY = nearestParentIndex.Item1 + SizeOfSquare;
            int limitationX = nearestParentIndex.Item2 + SizeOfSquare;

            for (int i = nearestParentIndex.Item1; i < limitationY; i++)
                for (int j = nearestParentIndex.Item2; j < limitationX; j++)
                {
                    if (Convert.ToInt16(dgv[i, j].Value) == NullValue) continue;
                    if (currentCellColumn == i || currentCellRow == j) continue;

                    if (currentValue == Convert.ToInt16(dgv[i, j].Value))
                        return true;
                }

            return false;
        }
    }
}
