using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace WebSocketCommunication.Models
{
    public class Table
    {
        private static readonly Random _random = new Random();


        public DataTable GenerateTable(int rowsAmount)
        {
            var table = new DataTable();

            table.Columns.Add("id", typeof(int));
            table.Columns.Add("stringColumn", typeof(string));
            table.Columns.Add("date", typeof(string));
            table.PrimaryKey = new DataColumn[] { table.Columns["id"] };


            for (var i = 0; i < rowsAmount; i++)
            {
                GenerateRow(i, table);
            }

            return table;
        }


        public static DataRow GenerateRow(int id, DataTable table)
        {
            var date = DateTime.Now.ToString("d MMMM, hh:mm", CultureInfo.CreateSpecificCulture("ru-RU"));

            var row = table.Rows.Contains(id) ? table.Rows[id] : table.NewRow();

            row[0] = id;
            row[1] = RandomString(15);
            row[2] = date;

            table.Rows.Add(row);

            return row;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(1, length).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }
    }
}
