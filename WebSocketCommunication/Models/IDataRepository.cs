using System;
using System.Data;
using System.Threading.Tasks;

namespace WebSocketCommunication.Models
{
    public interface IDataRepository
    {
        void Subscribe(Action<DataTable> callback);
    }
}