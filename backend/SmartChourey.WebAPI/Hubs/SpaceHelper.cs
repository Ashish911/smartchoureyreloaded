using Microsoft.AspNetCore.SignalR;

namespace SmartChourey.WebAPI.Hubs
{
    public class SpaceHelper
    {
        private readonly IHubContext<ProgressHub> _hubContext;

        public SpaceHelper(IHubContext<ProgressHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task UpdateSpaceUsed(float spaceUsed, string? connectionId = null)
        {
            if (String.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.All.SendAsync("SpaceUpdated", spaceUsed);
            }
            else
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("SpaceUpdated", spaceUsed);
            }
        }
    }
}
