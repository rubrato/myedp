using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using MyEDP.Models;

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
            //await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
            try
            {
                oracleDAO conexao = new oracleDAO("system", "@Dmin123");
				//conexao.SalvarMarcador(lat, lng, cpf, nome, telefone, obs);
				pop = conexao.OracleSelectFirstName();
            }
            catch(System.Exception)
            {
                await Clients.Caller.SendAsync("alert", "erro ao conectar");
            }
            await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
            await Clients.All.SendAsync("alert", "você enviou seu relato");



        }
	}
}
