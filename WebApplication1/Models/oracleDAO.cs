using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace MyEDP.Models
{
    public class oracleDAO
    {
        string conString;

        public oracleDAO(string usr, string pass)
        {
            conString = "User Id=" + usr + ";Password=" + pass + "; Data Source=rubrato.myddns.me:1521/pi;";
        }

        public String OracleSelectFirstName()
        {
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                        
                        cmd.CommandText = "select first_name from employees";

                        string pop = "<br>";
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            pop += reader.GetString(0) + "<br>";
                            }

                        return pop;
                    }
                    catch (System.Exception)
                    {
                        return "erro";
                    }
                }
            }
        }

        public void SalvarMarcador(double lat, double lng, string cpf, string nome, string telefone, string obs, string problema)
        {
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "INSERT INTO CHAMADO (CD_CHAMADO, LAT, LNG, ESTADO_CHAMADO, DS_CHAMADO) " +
                            "VALUES (CHAMADO_SEQ.NEXTVAL, '" + lat + "', '" + lng + "', '" + problema + "', '"+ obs + "');";
                        cmd.ExecuteReader();

                        cmd.CommandText = "INSERT INTO CLIENTE (CD_CLIENTE, NOME_CLIENTE, TELEF, CPF) " +
                            "VALUES (CLIENTE_SEQ.NEXTVAL, '" + nome+"', '"+telefone+"', '"+cpf+"');";
                        cmd.ExecuteReader();
                      
                    }
                    catch (System.Exception)
                    {
                       
                    }
                }
            }
        }

    }
}

    /*
    string conString = "User Id=system;Password=@Dmin123;" +

                //How to connect to an Oracle DB without SQL*Net configuration file
                //  also known as tnsnames.ora.
                "Data Source=localhost:1521/pi;";

            //How to connect to an Oracle DB with a DB alias.
            //Uncomment below and comment above.
            //"Data Source=<service name alias>;";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select first_name from employees";


                        pop += "<br>";
                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            pop += reader.GetString(0) + "<br>";
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                        }
await Clients.All.SendAsync("sendMarcador", lat, lng, pop);
reader.Dispose();
                    }
                    catch (System.Exception ex)
                    {
                        // context.Response.WriteAsync(ex.Message);
                    }
                }
            }
            */


    /*
     
     cmd.CommandText = "SELECT LAT, LNG, ESTADO_CHAMADO "+
                       "FROM CHAMADO"+
                       "";
     
     
     
     
     
     */