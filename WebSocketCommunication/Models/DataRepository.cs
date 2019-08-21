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
        private Table _tableFactory;
        private DataTable _dataTable;
        private static readonly Random _random = new Random();
        public DataTable Data => _dataTable;
        private readonly ILogger _logger;
        private Timer _timer;

        public DataRepository(ILogger<DataRepository> logger)
        {
            if (_dataTable == null)
            {
                _tableFactory = new Table();
                _dataTable = _tableFactory.GenerateTable(10000);
                _logger = logger;
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public DataRow Update(int id)
        //{
        //    return _tableFactory.GenerateRow(id);
        //}

        public DataRow Insert(DataRow row)
        {
            throw new NotImplementedException();
        }

        private void UpdateRandomRows()
        {
            for (var i = 0; i < 100; i++)
            {
                var randomId = _random.Next(0, _dataTable.Rows.Count);
                //Update(randomId);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            UpdateRandomRows();
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

