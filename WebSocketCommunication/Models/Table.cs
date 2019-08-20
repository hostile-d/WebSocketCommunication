using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace WebSocketCommunication.Models
{
    public class Table
    {
        private static readonly Random _random = new Random();

        public static DataTable GenerateTable(int rowsAmount)
        {
            var table = new DataTable();

            table.Columns.Add("id", typeof(int));
            table.Columns.Add("stringColumn", typeof(string));
            table.Columns.Add("date", typeof(string));

            for (var i = 0; i < rowsAmount; i++)
            {
                var date = DateTime.Now.ToString("d MMMM, hh:mm", CultureInfo.CreateSpecificCulture("ru-RU"));
                table.Rows.Add(i, RandomString(15), date);
            }

            return table;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(1, length).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }
    }
}
