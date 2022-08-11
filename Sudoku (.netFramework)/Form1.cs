using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

/*
 * Back
 * (Метод проверки правильности) +
 * (Переполнение в SudokuBoardDGV_CellEndEdit когда вводится слишком большое число) +
 * Метод авторешения
 * Метод заполения доски определенного размера по сложности
 * Метод загрузки сохраненных данных из папки с игрой
 * Автоматически проставленные цифры пометить как readonly
 * 
 * Front
 * Автоматически проставленные цифры красить в чуть более тусклый цвет
 * Подсветка линий
 * Шахматная раскраска доски +
 *      Проблемы:
 *      В поле не помещаются двузначные цифры
 *      Слишком маленькие ячейки при игре на больших досках
 */

namespace Sudoku
{
    public partial class Form1 : Form
    {
        bool _isPlayersAnswerCorrect = true;
        bool _shouldCheckAnswer = false;
        GameStates _gameStates;

        public Form1()
        {
            InitializeComponent();
            NewGame();

#if DEBUG
            Test();
#endif
        }

        private void NewGame()
        {
            _gameStates = new GameStates(BoardsEnum.X16);
            PrepareBoard();
            InitStyles();
        }

        private void PrepareBoard()
        {
            InitNewBoard((int)_gameStates.SudokuSize);
            FillCellsWithZero();
            MakeCellsSquare();
            LimitMaxInputLength();
        }

        private void InitStyles()
        {
            int fontSize = (SudokuBoardDGV.Columns[0].Width / 2) - 3;
            SudokuBoardDGV.DefaultCellStyle.Font = new Font("Segoe Script", fontSize);

            //Заменить хардкод на выбор цвета позже
            SudokuBoardDGV.ForeColor = Color.Black;
            _gameStates.Squares.SquareColor = Color.BurlyWood;
            _gameStates.Squares.PaintDGVLikeChess(SudokuBoardDGV);
        }

        private void InitNewBoard(int size)
        {
            for (int i = 0; i < size; i++)
            {
                SudokuBoardDGV.Columns.Add(null, null);
                SudokuBoardDGV.Rows.Add();
            }
        }

        private void FillCellsWithZero()
        {
            for (int i = 0; i < SudokuBoardDGV.Rows.Count; i++)
            {
                for (int j = 0; j < SudokuBoardDGV.Columns.Count; j++)
                {
                    SudokuBoardDGV[j, i].Value = 0;
                }
            }
        }

        //Makes DataGridView's cells square
        private void MakeCellsSquare()
        {
            int cellMaxSize = SudokuBoardDGV.Size.Width / SudokuBoardDGV.Rows.Count;

            for (int i = 0; i < SudokuBoardDGV.Rows.Count; i++)
            {
                SudokuBoardDGV.Rows[i].Height = cellMaxSize;
                SudokuBoardDGV.Columns[i].Width = cellMaxSize;
            }
        }

        private void LimitMaxInputLength()
        {
            for (int i = 0; i < SudokuBoardDGV.ColumnCount; i++)
                for (int j = 0; j < SudokuBoardDGV.RowCount; j++)
                {
                    var cell = (DataGridViewTextBoxCell)SudokuBoardDGV[i,j];
                    cell.MaxInputLength = 2;
                }
        }

        //Проверка содержания числа по горизонтали и вертикали
        private bool IsFitsOnLines()
        {
            int currentCellColumn = SudokuBoardDGV.CurrentCell.ColumnIndex;
            int currentCellRow = SudokuBoardDGV.CurrentCell.RowIndex;
            bool isFit = true;

            for (int i = 0; i < SudokuBoardDGV.RowCount; i++)
            {
                if (i != currentCellColumn)
                {
                    //SudokuBoard[i, selectedRow] != selectedValue
                    isFit = Convert.ToInt16(SudokuBoardDGV[i, currentCellRow].Value) 
                        != Convert.ToInt16(SudokuBoardDGV.CurrentCell.Value);
                    if (isFit == false) return false;
                }
            }

            for (int i = 0; i < SudokuBoardDGV.ColumnCount; i++)
            {
                if (i != currentCellRow)
                {
                    //SudokuBoard[selectedColumn, i] != selectedValue
                    isFit = Convert.ToInt16(SudokuBoardDGV[currentCellColumn, i].Value) 
                        != Convert.ToInt16(SudokuBoardDGV.CurrentCell.Value);
                    if (isFit == false) return false;
                }
            }

            return isFit;
        }

        private bool IsFitsOnLines(int currentCellColumn, int currentCellRow, int CurrentCellValue)
        {
            bool isFit = true;

            for (int i = 0; i < SudokuBoardDGV.RowCount; i++)
            {
                if (i != currentCellColumn)
                {
                    //SudokuBoard[i, selectedRow] != selectedValue
                    isFit = Convert.ToInt16(SudokuBoardDGV[i, currentCellRow].Value)
                        != CurrentCellValue;
                    if (isFit == false) return false;
                }
            }

            for (int i = 0; i < SudokuBoardDGV.ColumnCount; i++)
            {
                if (i != currentCellRow)
                {
                    //SudokuBoard[selectedColumn, i] != selectedValue
                    isFit = Convert.ToInt16(SudokuBoardDGV[currentCellColumn, i].Value)
                        != CurrentCellValue;
                    if (isFit == false) return false;
                }
            }

            return isFit;
        }

        private void PrintCorrectnessOnLabel()
        {
            string message = _isPlayersAnswerCorrect ? "Правильно" : "Не правильно";
            MessageLabel.Text = message;
        }
        private void SudokuBoardDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (Convert.ToInt16(SudokuBoardDGV[e.ColumnIndex, e.RowIndex].Value) > _gameStates.Squares.MaxNumber)
                    SudokuBoardDGV[e.ColumnIndex, e.RowIndex].Value = _gameStates.Squares.MaxNumber;
            }
            catch (OverflowException) 
            {
                SudokuBoardDGV[e.ColumnIndex, e.RowIndex].Value = _gameStates.Squares.MaxNumber;
            }

            CheckUserAnswer();
            PrintCorrectnessOnLabel();
        }

        private void CheckUserAnswer()
        {
            if (_shouldCheckAnswer)
            {
                bool linesValidation = IsFitsOnLines();
                bool sudokuSquaresValidation = !_gameStates.Squares.DoesNumberInSudokuSquareRepeated(SudokuBoardDGV,
                                                                                               SudokuBoardDGV.CurrentCell.ColumnIndex,
                                                                                               SudokuBoardDGV.CurrentCell.RowIndex); 

                _isPlayersAnswerCorrect = linesValidation && sudokuSquaresValidation;
            }
        }

        private void CheckAnswerCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            _shouldCheckAnswer = CheckAnswerCheckBox.Checked;

            if (_shouldCheckAnswer == CheckAnswerCheckBox.Checked)
            {
                CheckUserAnswer();
                PrintCorrectnessOnLabel();
            }

            MessageLabel.Enabled = _shouldCheckAnswer;
            MessageLabel.Visible = _shouldCheckAnswer;

        }

#if DEBUG
        private void Test()
        {
            int[] testArray = { 7, 3, 5, 8, 1, 4, 2, 9, 6, 6, 9, 8, 3, 5, 2, 4, 1, 7, 1, 2, 4, 7, 9, 6, 5, 8, 3, 3, 7, 6, 5, 4, 1, 9, 2, 8, 5, 8, 1, 2, 7, 9, 6, 3, 4, 2, 4, 9, 6, 8, 3, 7, 5, 1, 4, 1, 3, 9, 2, 7, 8, 6, 5, 9, 5, 7, 1, 6, 8, 3, 4, 2, 8, 6, 2, 4, 3, 5, 1, 7, 9 };
            int testI = 0;

            for (int i = 0; i < SudokuBoardDGV.ColumnCount; i++)
            {
                for (int j = 0; j < SudokuBoardDGV.RowCount; j++)
                {
                    try
                    {
                        SudokuBoardDGV[i, j].Value = testArray[testI];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return;
                    }
                    testI++;
                }
            }
        }

        private bool CheckAllTable()
        {
            for (int i = 0; i < SudokuBoardDGV.ColumnCount; i++)
                for (int j = 0; j < SudokuBoardDGV.RowCount; j++)
                {
                    if (_shouldCheckAnswer)
                    {
                        bool linesValidation = IsFitsOnLines(i,j, Convert.ToInt16(SudokuBoardDGV[i,j].Value));
                        bool sudokuSquaresValidation = !_gameStates.Squares.DoesNumberInSudokuSquareRepeated(SudokuBoardDGV,
                                                                                                       SudokuBoardDGV.CurrentCell.ColumnIndex,
                                                                                                       SudokuBoardDGV.CurrentCell.RowIndex);

                        if ((linesValidation && sudokuSquaresValidation) == false)
                            return false;
                    }
                }

            return true;
        }

        private void Wtf()
        {
            _isPlayersAnswerCorrect = CheckAllTable();
            PrintCorrectnessOnLabel();
        }

#endif
        private void MessageLabel_Click(object sender, EventArgs e)
        {
            Wtf();
        }

        private void SudokuBoardDGV_KeyPress(object sender, KeyPressEventArgs e) 
            => e.Handled = !Char.IsDigit(e.KeyChar);

        private void SudokuBoardDGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var tb = (TextBox)e.Control;
            tb.KeyPress += new KeyPressEventHandler(SudokuBoardDGV_KeyPress);
        }
    }
}