using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TableColumn
    {
        public string Name { get; set; }
        public TableColumnType Type { get; set; }
        public bool Key { get; set; }
        public TableColumn()
        {
            this.Name = "";
            this.Type = TableColumnType.Int;
            this.Key = false;
        }
        public override string ToString()
        {
            string type = Type == TableColumnType.Int ? "INTEGER" : "TEXT(255)";
            string key = Key ? " PRIMARY KEY" : "";
            return $"[{Name}] {type}{key}";
        }
    }

    public enum TableColumnType
    {
        Int,
        String
    }
}
