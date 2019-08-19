using System.Data;

namespace WebSocketCommunication.Models
{
    public interface IDataRepository
    {
        DataTable Table { get; set; }
        void GenerateTable();
    }
}