using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOClassiRepos : IClassiRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Classi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Classi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Classi> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Classi> listaClassi = new List<Classi>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Classi";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaClassi.Add(reader.ToClasse());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAll in classi");
                }
                finally
                {
                    connection.Close();
                }
                return listaClassi;
            }
        }

        public IEnumerable<Classi> GetAllWithFilter(int filtro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Classi> listaClassi = new List<Classi>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Classi WHERE IsEroe = @eroe";

                    command.Parameters.AddWithValue("@eroe", filtro);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaClassi.Add(reader.ToClasse());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAllWithFilter in classi");
                }
                finally
                {
                    connection.Close();
                }                
                return listaClassi;
            }
        }

        public Classi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Classi obj)
        {
            throw new NotImplementedException();
        }
    }
}

