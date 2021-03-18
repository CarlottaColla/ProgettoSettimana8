using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOGiocatoriRepos : IGiocatoriRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Giocatori obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO Giocatori VALUES (@nome,@ruolo)";

                    command.Parameters.AddWithValue("@nome", obj.Nome);
                    command.Parameters.AddWithValue("@ruolo", obj.Ruolo_ID);

                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore nell'inserimento del nuovo giocatore");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool Delete(Giocatori obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Giocatori> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Giocatori> listaGiocatori = new List<Giocatori>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Giocatori";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaGiocatori.Add(reader.ToGiocatori());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAll in giocatori");
                }
                finally
                {
                    connection.Close();
                }
                return listaGiocatori;
            }
        }

        public IEnumerable<Giocatori> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
        }

        public Giocatori GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Giocatori obj)
        {
            throw new NotImplementedException();
        }
    }
}
