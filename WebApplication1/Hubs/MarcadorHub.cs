using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApplication1.Hubs
{
	public class MarcadorHub : Hub
	{

		public async Task GetMarcador(double lat, double lng)
		{
			await Clients.All.SendAsync("getMarcador", lat, lng);
		}

		public async Task SendMarcador(double lat, double lng, string obs)
		{
			await Clients.All.SendAsync("sendMarcador", lat, lng, obs);

		}
	}
}
