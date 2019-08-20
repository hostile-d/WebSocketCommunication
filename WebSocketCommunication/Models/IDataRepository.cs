using System.Data;

namespace WebSocketCommunication.Models
{
    public interface IDataRepository
    {
        DataTable Data { get; }
    }
}