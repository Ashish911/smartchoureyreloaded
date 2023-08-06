using Microsoft.AspNetCore.SignalR;

namespace SmartChourey.WebAPI.Hubs
{
    public class SpaceHub : Hub
    {
        private static string _connectionId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpaceHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            _connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }

        public async Task UpdateConnectionId(string connectionId)
        {
            _connectionId = connectionId;
            _httpContextAccessor.HttpContext.Session.SetString("SpaceHubConnectionId", connectionId);
            await Clients.Client(connectionId).SendAsync("ConnectionIdUpdated", connectionId);
        }
    }
}
