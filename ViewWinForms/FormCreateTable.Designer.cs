namespace ViewWinForms
{
    partial class FormCreateTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTableName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelTableName = new System.Windows.Forms.Label();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.numericUpDownAmountOfColumns = new System.Windows.Forms.NumericUpDown();
            this.labelAmountOfColumns = new System.Windows.Forms.Label();
            this.comboBoxConnect = new System.Windows.Forms.ComboBox();
            this.labelConnect = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountOfColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTableName
            // 
            this.textBoxTableName.Location = new System.Drawing.Point(15, 25);
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.Size = new System.Drawing.Size(100, 20);
            this.textBoxTableName.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(440, 267);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 38);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Создать";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelTableName
            // 
            this.labelTableName.AutoSize = true;
            this.labelTableName.Location = new System.Drawing.Point(12, 9);
            this.labelTableName.Name = "labelTableName";
            this.labelTableName.Size = new System.Drawing.Size(103, 13);
            this.labelTableName.TabIndex = 3;
            this.labelTableName.Text = "Название таблицы";
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Location = new System.Drawing.Point(15, 52);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.Size = new System.Drawing.Size(518, 209);
            this.dataGridViewTable.TabIndex = 4;
            // 
            // numericUpDownAmountOfColumns
            // 
            this.numericUpDownAmountOfColumns.Location = new System.Drawing.Point(207, 25);
            this.numericUpDownAmountOfColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAmountOfColumns.Name = "numericUpDownAmountOfColumns";
            this.numericUpDownAmountOfColumns.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAmountOfColumns.TabIndex = 5;
            this.numericUpDownAmountOfColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAmountOfColumns.ValueChanged += new System.EventHandler(this.numericUpDownAmountOfColumns_ValueChanged);
            // 
            // labelAmountOfColumns
            // 
            this.labelAmountOfColumns.AutoSize = true;
            this.labelAmountOfColumns.Location = new System.Drawing.Point(204, 9);
            this.labelAmountOfColumns.Name = "labelAmountOfColumns";
            this.labelAmountOfColumns.Size = new System.Drawing.Size(116, 13);
            this.labelAmountOfColumns.TabIndex = 6;
            this.labelAmountOfColumns.Text = "Количество столбцов";
            // 
            // comboBoxConnect
            // 
            this.comboBoxConnect.FormattingEnabled = true;
            this.comboBoxConnect.Location = new System.Drawing.Point(401, 25);
            this.comboBoxConnect.Name = "comboBoxConnect";
            this.comboBoxConnect.Size = new System.Drawing.Size(121, 21);
            this.comboBoxConnect.TabIndex = 7;
            // 
            // labelConnect
            // 
            this.labelConnect.AutoSize = true;
            this.labelConnect.Location = new System.Drawing.Point(398, 9);
            this.labelConnect.Name = "labelConnect";
            this.labelConnect.Size = new System.Drawing.Size(108, 13);
            this.labelConnect.TabIndex = 8;
            this.labelConnect.Text = "Связать с таблицей";
            // 
            // FormCreateTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 316);
            this.Controls.Add(this.labelConnect);
            this.Controls.Add(this.comboBoxConnect);
            this.Controls.Add(this.labelAmountOfColumns);
            this.Controls.Add(this.numericUpDownAmountOfColumns);
            this.Controls.Add(this.dataGridViewTable);
            this.Controls.Add(this.labelTableName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxTableName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormCreateTable";
            this.Text = "Создание таблицы";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountOfColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTableName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelTableName;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.NumericUpDown numericUpDownAmountOfColumns;
        private System.Windows.Forms.Label labelAmountOfColumns;
        private System.Windows.Forms.ComboBox comboBoxConnect;
        private System.Windows.Forms.Label labelConnect;
    }
}