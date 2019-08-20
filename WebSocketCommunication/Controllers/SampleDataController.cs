using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSocketCommunication.Models;
using Newtonsoft.Json;

namespace WebSocketCommunication.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly IDataRepository _dataRepository;

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
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await GetTable(context, webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task GetTable(HttpContext context, WebSocket webSocket)
        {
            var table = _dataRepository.Data;
            var json = JsonConvert.SerializeObject(table);
            var bytes = Encoding.UTF8.GetBytes(json);
            var arraySegment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
