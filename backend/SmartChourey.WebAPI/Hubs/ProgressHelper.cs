using Microsoft.AspNetCore.SignalR;

namespace SmartChourey.WebAPI.Hubs
{
    public class ProgressHelper
    {
        private readonly IHubContext<ProgressHub> _hubContext;

        public ProgressHelper(IHubContext<ProgressHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendProgress(int progressPercentage, string? connectionId = null)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.All.SendAsync("ProgressUpdated", progressPercentage);
            }
            else
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ProgressUpdated", progressPercentage);
            }
        }
    }
}
