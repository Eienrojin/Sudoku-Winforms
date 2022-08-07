namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SudokuBoardDGV = new System.Windows.Forms.DataGridView();
            this.IsCorrectLabel = new System.Windows.Forms.Label();
            this.CheckAnswerCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SudokuBoardDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.SudokuBoardDGV);
            this.flowLayoutPanel1.ForeColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(562, 562);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // SudokuBoardDGV
            // 
            this.SudokuBoardDGV.AllowUserToAddRows = false;
            this.SudokuBoardDGV.AllowUserToDeleteRows = false;
            this.SudokuBoardDGV.AllowUserToResizeColumns = false;
            this.SudokuBoardDGV.AllowUserToResizeRows = false;
            this.SudokuBoardDGV.BackgroundColor = System.Drawing.SystemColors.Control;
            this.SudokuBoardDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SudokuBoardDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SudokuBoardDGV.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe Print", 15.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SudokuBoardDGV.DefaultCellStyle = dataGridViewCellStyle1;
            this.SudokuBoardDGV.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SudokuBoardDGV.Location = new System.Drawing.Point(3, 3);
            this.SudokuBoardDGV.MultiSelect = false;
            this.SudokuBoardDGV.Name = "SudokuBoardDGV";
            this.SudokuBoardDGV.RowHeadersVisible = false;
            this.SudokuBoardDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SudokuBoardDGV.Size = new System.Drawing.Size(559, 559);
            this.SudokuBoardDGV.TabIndex = 0;
            this.SudokuBoardDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SudokuBoardDGV_CellContentClick);
            this.SudokuBoardDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SudokuBoardDGV_CellEndEdit);
            // 
            // IsCorrectLabel
            // 
            this.IsCorrectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IsCorrectLabel.AutoSize = true;
            this.IsCorrectLabel.Enabled = false;
            this.IsCorrectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.IsCorrectLabel.Location = new System.Drawing.Point(12, 602);
            this.IsCorrectLabel.Name = "IsCorrectLabel";
            this.IsCorrectLabel.Size = new System.Drawing.Size(121, 25);
            this.IsCorrectLabel.TabIndex = 1;
            this.IsCorrectLabel.Text = "Правильно";
            this.IsCorrectLabel.Visible = false;
            // 
            // CheckAnswerCheckBox
            // 
            this.CheckAnswerCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckAnswerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.CheckAnswerCheckBox.Location = new System.Drawing.Point(407, 586);
            this.CheckAnswerCheckBox.Name = "CheckAnswerCheckBox";
            this.CheckAnswerCheckBox.Size = new System.Drawing.Size(170, 58);
            this.CheckAnswerCheckBox.TabIndex = 4;
            this.CheckAnswerCheckBox.Text = "Проверять на правильность";
            this.CheckAnswerCheckBox.UseVisualStyleBackColor = true;
            this.CheckAnswerCheckBox.CheckedChanged += new System.EventHandler(this.CheckAnswerCheckBox_CheckedChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 689);
            this.Controls.Add(this.IsCorrectLabel);
            this.Controls.Add(this.CheckAnswerCheckBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SudokuBoardDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView SudokuBoardDGV;
        private System.Windows.Forms.Label IsCorrectLabel;
        private System.Windows.Forms.CheckBox CheckAnswerCheckBox;
    }
}

