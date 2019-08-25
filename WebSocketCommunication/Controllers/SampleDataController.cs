using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSocketCommunication.Models;
using Newtonsoft.Json;
using System.Data;

namespace WebSocketCommunication.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IDataRepository _dataRepository;

        private DataTable _data = null;

        public SampleDataController(IDataRepository repo)
        {
            _dataRepository = repo;
        }

        [HttpGet]
        public async Task Get()
        {
            var context = ControllerContext.HttpContext;
            var isSocketRequest = context.WebSockets.IsWebSocketRequest;

            if (isSocketRequest)
            {
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                SubscribeToRepository();
                await SendData(context, webSocket);

            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private void SubscribeToRepository()
        {
            _dataRepository.Subscribe(ServiceCallback);
        }

        private void ServiceCallback(DataTable table)
        {
            _data = table;
        }

        private async Task SendData(HttpContext context, WebSocket webSocket)
        {
            if (webSocket != null && webSocket.State == WebSocketState.Open)
            {
                while (!context.RequestAborted.IsCancellationRequested)
                {
                    if (_data != null)
                    {
                        var json = JsonConvert.SerializeObject(_data);
                        var bytes = Encoding.UTF8.GetBytes(json);
                        var arraySegment = new ArraySegment<byte>(bytes);
                        await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    _data = null;

                    await Task.Delay(2000);
                }
            }

        }
    }
}
