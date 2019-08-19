using System.Data;

namespace WebSocketCommunication.Models
{
    public class DataRepository : IDataRepository
    {
        public DataTable Table { get; set; }
        public DataRepository()
        {
            GenerateTable();
        }
        public void GenerateTable()
        {
            var tableInstance = new Table(10000);
            Table = tableInstance.GenerateTable();
        }
    }
}
