using System.Data;

namespace WebSocketCommunication.Models
{
    public class DataRepository : IDataRepository
    {
        private readonly DataTable _dataTable;
        public DataTable Data => _dataTable;
        public DataRepository()
        {
            if (_dataTable == null)
            {
                _dataTable = Table.GenerateTable(10000);
            }
        }
    }
}
