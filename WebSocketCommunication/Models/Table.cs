using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace WebSocketCommunication.Models
{
    public class Table
    {
        private int RowsAmount { get; set; }
        private static Random random = new Random();

        public Table(int rows)
        {
            RowsAmount = rows;
        }

        public DataTable GenerateTable()
        {
            var table = new DataTable();

            table.Columns.Add("id", typeof(int));
            table.Columns.Add("stringColumn", typeof(string));
            table.Columns.Add("date", typeof(string));

            for (var i = 0; i < RowsAmount; i++)
            {
                var date = DateTime.Now.ToString("d MMMM, hh:mm", CultureInfo.CreateSpecificCulture("ru-RU"));
                table.Rows.Add(i, RandomString(15), date);
            }

            return table;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
    }
}
