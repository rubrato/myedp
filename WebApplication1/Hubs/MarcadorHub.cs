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

		public async Task SendMarcador(double lat, double lng, string obs)
		{
			await Clients.All.SendAsync("sendMarcador", lat, lng, obs);

		
			//Demo: Basic ODP.NET Core application for ASP.NET Core
			// to connect, query, and return results to a web page

			//Create a connection to Oracle			
			string conString = "User Id=system;Password=@Dmin123;" +

			//How to connect to an Oracle DB without SQL*Net configuration file
			//  also known as tnsnames.ora.
			"Data Source=rubrato.myddns.me:1521/pi;";

			//How to connect to an Oracle DB with a DB alias.
			//Uncomment below and comment above.
			//"Data Source=<service name alias>;";

			using (OracleConnection con = new OracleConnection(conString))
			{
				using (OracleCommand cmd = con.CreateCommand())
				{
					
					con.Open();
					cmd.BindByName = true;

					//Use the command to display employee names from 
					// the EMPLOYEES table
					cmd.CommandText = "select first_name from employees";



					//Execute the command and use DataReader to display the data
					OracleDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						string consulta = "reader.GetString(0)";
						await Clients.All.SendAsync("sendConsulta", consulta);
					}

					reader.Dispose();
				}
			}

			
		}
	}
}
