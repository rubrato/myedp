using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace WebApplication1.Hubs
{
	public class MarcadorHub : Hub
	{

		public async Task GetMarcador(double lat, double lng)
		{
			await Clients.All.SendAsync("getMarcador", lat, lng);
		}

		public async Task SendMarcador(double lat, double lng,string cpf, string nome, string telefone, string obs)
		{
			string pop = nome + " -   " + telefone + "<br> " + obs;
			await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
			
					
		}
	}
}
