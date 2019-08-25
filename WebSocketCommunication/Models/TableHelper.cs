using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace WebSocketCommunication.Models
{
    public static class TableHelper
    {
        private static readonly Random _random = new Random();


        public static DataTable GenerateTable(int rowsAmount)
        {
            var table = CreateTableSchema();
            var revision = 0;

            for (var i = 0; i < rowsAmount; i++)
            {
                GenerateRow(i, table, revision);
            }

            return table;
        }


        public static DataRow GenerateRow(int id, DataTable table, int revision)
        {
            var date = DateTime.Now.ToString("d MMMM, hh:mm:ss", CultureInfo.CreateSpecificCulture("ru-RU"));

            DataRow row;

            if (table.Rows.Contains(id))
            {
                row = table.Rows[id];

                row[0] = id;
                row[1] = revision;
                row[2] = RandomString(15);
                row[3] = date;
            }
            else
            {
                row = table.Rows.Add(id, revision, RandomString(15), date);
            }

            return row;
        }

        private static DataTable CreateTableSchema()
        {
            var table = new DataTable();

            table.Columns.Add("id", typeof(int));
            table.Columns.Add("revision", typeof(int));
            table.Columns.Add("stringColumn", typeof(string));
            table.Columns.Add("date", typeof(string));
            table.PrimaryKey = new DataColumn[] { table.Columns["id"] };

            return table;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(1, length).Select(_ => chars[_random.Next(chars.Length)]).ToArray());
        }

        public static DataTable FilterByRevision(DataTable table, int revision)
        {
            DataRow[] result = table.Select($"revision > {revision}");
            var filteredTable = CreateTableSchema();
            foreach (DataRow row in result)
            {
                filteredTable.ImportRow(row);
            }
            return filteredTable;
        }
    }
}
