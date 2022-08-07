using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Класс, разбивающий доску судоку на квадраты как в реальной игре
//Работа класса:
/*
 * Определяется размер квадрата по размеру судоку
 * Вычисляются индексы начала квадратов
 * Во время игры проверяется, к какому квадрату принадлежит число с помощью индексов начала
 */


/*
 * Как работает массив Indexes
 * По индексу [,0] - находится индекс начала квадрата по вертикали
 * По индексу [,1] - находится индекс начала квадрата по горизонтали
 * По [этому, ] индексу находится индекс квадрата, начиная с первого квадрата
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
        public SudokuSquare(int sudokuSizeX)
        {
            SudokuSizeX = sudokuSizeX;
            SudokuSquareSize = (int)Math.Log(sudokuSizeX, 2);
            Indexes = new byte[SudokuSizeX, 2];

            CalculateParentIndexes();
        }

        public byte[,] Indexes { get; set; }
        public int SudokuSquareSize { get; private set; }
        public int SudokuSizeX { get; private set; }
        public int MaxNumber { get; private set; }

        private void CalculateParentIndexes()
        {
            byte skipStep = (byte)SudokuSquareSize;

            InitVerticalIndexes(skipStep);
            InitHorizontalIndexes(skipStep);
        }

        private void InitHorizontalIndexes(byte skipStep)
        {
            byte vertLength = (byte)SudokuSquareSize;
            byte verticalIndex = 0;
            const byte row = 1;

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

        //Потенциально можно упростить до одного цикла
        private void InitVerticalIndexes(byte skipStep)
        {
            byte vertLength = (byte)SudokuSquareSize;
            byte verticalSudokuIndex = 0;
            byte verticalIndex = 0;
            const byte column = 0;

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

        public int[] FindParentIndex(int currentCellColumn, int currentCellRow)
        {
            byte skipStep = (byte)SudokuSquareSize;
            byte parentIndexX = 0;
            byte parentIndexY = 0;
            byte numOfSquareY = 0;
            const byte column = 0;
            const byte row = 1;

            for (int squareNumX = 0; squareNumX < SudokuSquareSize; squareNumX++)
            {
                if (Indexes[numOfSquareY, column] <= currentCellColumn)
                    parentIndexY = Indexes[numOfSquareY, column];

                if (Indexes[squareNumX, row] <= currentCellRow)
                    parentIndexX = Indexes[squareNumX, row];

                numOfSquareY += skipStep;
            }

            return new int[] { parentIndexY, parentIndexX };
        }
    }
}
