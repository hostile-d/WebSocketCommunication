using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketCommunication.Models
{
    public class DataRepository : IDataRepository, IHostedService, IDisposable
    {
        private DataTable _dataTable;
        private static readonly Random _random = new Random();
        public DataTable Data => _dataTable;
        private Action<DataTable> ControllerCallback { get; set; }
        private readonly ILogger _logger;
        private Timer _timer;

        public DataRepository(ILogger<DataRepository> logger)
        {
            if (_dataTable == null)
            {
                _dataTable = Table.GenerateTable(10000);
                _logger = logger;
            }
        }

        public void Subscribe(Action<DataTable> callback)
        {
            ControllerCallback = callback;
        }


        public DataRow Update(int id)
        {
            return Table.GenerateRow(id, _dataTable);
        }

        private void UpdateRandomRows()
        {
            for (var i = 0; i < 5000; i++)
            {
                var randomId = _random.Next(0, _dataTable.Rows.Count);
                Update(randomId);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            UpdateRandomRows();
            if (_dataTable != null && ControllerCallback != null)
            {
                ControllerCallback(_dataTable);
            }
            _logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

