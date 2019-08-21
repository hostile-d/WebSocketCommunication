using System;
using System.Data;

namespace WebSocketCommunication.Models
{
    public interface IDataRepository
    {
        DataTable Data { get; }
        void Delete(int id);
        DataRow Update(int id, DataRow row);
        DataRow Insert(DataRow row);

    }
}