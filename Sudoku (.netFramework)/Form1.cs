using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

/*
 * Метод проверки правильности
 * Метод авторешения
 * Метод загрузки сохраненных данных из папки с игрой
 * Метод заполения доски определенного размера
 */

namespace Sudoku
{
    public partial class Form1 : Form
    {
        bool _isPlayersAnswerCorrect = true;
        bool _shouldCheckAnswer = false;

        public Form1()
        {
            InitializeComponent();
            NewGame();
        }

        GameStates _gameStates;

        private void NewGame()
        {
            _gameStates = new GameStates(BoardsEnum.X9);
            PrepareBoard();
            InitStyles();
        }

        private void InitStyles()
        {
            int fontSize = SudokuBoardDGV.Columns[0].Width / 2;
            SudokuBoardDGV.DefaultCellStyle.Font = new Font("Segoe Script", fontSize);
        }

        private void PrepareBoard()
        {
            InitNewBoard(_gameStates.SudokuSize);
            FillCellsWithZero();
            MakeCellsSquare();
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

        //Делает ячейки DataGridView квадратными
        private void MakeCellsSquare()
        {
            int cellMaxSize = SudokuBoardDGV.Size.Width / SudokuBoardDGV.Rows.Count;

            for (int i = 0; i < SudokuBoardDGV.Rows.Count; i++)
            {
                SudokuBoardDGV.Rows[i].Height = cellMaxSize;
                SudokuBoardDGV.Columns[i].Width = cellMaxSize;
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

        private void PrintMessageOnCorrectLabel()
        {
            string message = _isPlayersAnswerCorrect ? "Правильно" : "Не правильно";
            IsCorrectLabel.Text = message;
        }

        private void SudokuBoardDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_shouldCheckAnswer || IsCorrectLabel.Visible)
            {
                _isPlayersAnswerCorrect = IsFitsOnLines();
                PrintMessageOnCorrectLabel();
            }
        }
        private void SudokuBoardDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CheckAnswerCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            _shouldCheckAnswer = CheckAnswerCheckBox.Checked;

            if (_shouldCheckAnswer)
            {
                IsCorrectLabel.Enabled = _isPlayersAnswerCorrect;
                IsCorrectLabel.Visible = _isPlayersAnswerCorrect;
            }
        }
    }
}