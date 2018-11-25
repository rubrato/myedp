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

		public async Task GetMarcadores()
		{
			await Clients.Caller.SendAsync("alert", "Recuperando marcadores");
		}

		public async Task SendMarcador(double lat, double lng,string cpf, string nome, string telefone, string obs, int problema)
		{
			string pop = nome + " -   " + telefone + "<br> " + problema + "<br>" + obs;
            //await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
            try
            {
                oracleDAO conexao = new oracleDAO("FUNCIONARIO", "123456");
                conexao.SalvarMarcador(lat, lng, cpf, nome, telefone, obs, problema.ToString());
            }
            catch (System.Exception)
            {
                await Clients.Caller.SendAsync("alert", "erro ao conectar");
            }

			if (problema == 0)
				await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
			if (problema == 1)
				await Clients.All.SendAsync("sendMarcador1", lat, lng, pop);
			if (problema == 2)
				await Clients.All.SendAsync("sendMarcador2", lat, lng, pop);
			if (problema == 3)
				await Clients.All.SendAsync("sendMarcador3", lat, lng, pop);
			if (problema == 4)
				await Clients.All.SendAsync("sendMarcador4", lat, lng, pop);
            if (problema == 5)
                await Clients.All.SendAsync("sendMarcador5", lat, lng, pop);
            if (problema == 6)
                await Clients.All.SendAsync("sendMarcador6", lat, lng, pop);
            if (problema == 7)
                await Clients.All.SendAsync("sendMarcador7", lat, lng, pop);
            //await Clients.All.SendAsync("alert", "você enviou seu relato");



        }
	}
}
