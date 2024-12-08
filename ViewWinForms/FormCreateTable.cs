using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;

namespace ViewWinForms
{
    public partial class FormCreateTable : Form
    {
        DatabaseManager databaseManager = new DatabaseManager();
        List<TableColumn> tableColumns;
        List<TableColumn> removedTableColumns;

        public FormCreateTable()
        {
            InitializeComponent();
            tableColumns = new List<TableColumn>() { new TableColumn() };
            removedTableColumns = new List<TableColumn>();

            SetupTable();
            RedrawTable();
        }

        private void SetupTable()
        {
            dataGridViewTable.AutoGenerateColumns = false;

            // Add a column for the Name property
            dataGridViewTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Название столбца",
                DataPropertyName = "Name", // Bind to TableColumn.Name
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Add a column for the Type property as a dropdown
            dataGridViewTable.Columns.Add(new DataGridViewComboBoxColumn
            {
                HeaderText = "Тип столбца",
                DataPropertyName = "Type", // Bind to TableColumn.Type
                DataSource = Enum.GetValues(typeof(TableColumnType)), // Populate with enum values
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Add a column for the Key property as a checkbox
            dataGridViewTable.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Основной ключ",
                DataPropertyName = "Key", // Bind to TableColumn.Key
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });
        }

        private void RedrawTable()
        {
            dataGridViewTable.DataSource = null;
            dataGridViewTable.AllowUserToAddRows = true;
            dataGridViewTable.DataSource = tableColumns;
        }

        private void numericUpDownAmountOfColumns_ValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(numericUpDownAmountOfColumns.Value);
            int previousValue = tableColumns.Count;

            if (value > previousValue)
            {
                if (removedTableColumns.Count > 0)
                {
                    TableColumn lastRemovedTableColumn = removedTableColumns.Last();
                    tableColumns.Add(lastRemovedTableColumn);
                    removedTableColumns.Remove(lastRemovedTableColumn);
                } else
                {
                    tableColumns.Add(new TableColumn());
                }
            }
            else if (value < previousValue)
            {
                TableColumn lastTableColumn = tableColumns.Last();
                removedTableColumns.Add(lastTableColumn);
                tableColumns.Remove(lastTableColumn);
            }
            RedrawTable();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string tableName = textBoxTableName.Text;
            if (tableName.Length < 1)
            {
                MessageBox.Show("Введите имя таблицы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tableColumns.Any(c => c.Name.Length < 1))
            {
                MessageBox.Show("Введите имя всем стобцам!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int amountOfKeys = tableColumns.Count(c => c.Key);
            if (amountOfKeys < 1)
            {
                MessageBox.Show("Установите ключ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (amountOfKeys > 1)
            {
                MessageBox.Show("Ключ должен быть только один!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            databaseManager.CreateTable(tableName, tableColumns);
            MessageBox.Show("Таблица создана!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
