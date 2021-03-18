using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOLivelliRepos : ILivelliRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Livelli obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Livelli obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Livelli> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Livelli> listaLivelli = new List<Livelli>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Livelli";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaLivelli.Add(reader.ToLivelli());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAll in livelli");
                }
                finally
                {
                    connection.Close();
                }
                return listaLivelli;
            }
        }

        public IEnumerable<Livelli> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
        }

        public Livelli GetByID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Livelli livello = new Livelli();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Livelli WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    livello = reader.ToLivelli();

                    reader.Close();
                }
                catch (SqlException)
                { 
                    Console.WriteLine("Errore in GetAllWithFilter in livelli");
                }
                finally
                {
                    connection.Close();
                }
                return livello;
            }
        }

        public bool Update(Livelli obj)
        {
            throw new NotImplementedException();
        }
    }
}
