using System;
using System.Data;

namespace WebSocketCommunication.Models
{
    public interface IDataRepository
    {
        DataTable Data { get; }
        DataRow Update(int id);

    }
}