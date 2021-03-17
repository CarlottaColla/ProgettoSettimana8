using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOArmiRepos : IArmiRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Armi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Armi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Armi> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Armi";

                SqlDataReader reader = command.ExecuteReader();
                List<Armi> listaArmi = new List<Armi>();

                while (reader.Read())
                {
                    listaArmi.Add(reader.ToArmi());
                }

                reader.Close();
                connection.Close();
                return listaArmi;
            }
        }

        public IEnumerable<Armi> GetAllWithFilter(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Armi WHERE Classe_ID = @classe";

                command.Parameters.AddWithValue("@classe", ID);

                SqlDataReader reader = command.ExecuteReader();
                List<Armi> listaArmi = new List<Armi>();

                while (reader.Read())
                {
                    listaArmi.Add(reader.ToArmi());
                }

                reader.Close();
                connection.Close();
                return listaArmi;
            }
        }

        public Armi GetByID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Armi WHERE ID = @ID";

                command.Parameters.AddWithValue("@ID", ID);

                SqlDataReader reader = command.ExecuteReader();
                Armi arma = new Armi();

                reader.Read();
                arma = reader.ToArmi();

                reader.Close();
                connection.Close();
                return arma;
            }
        }

        public bool Update(Armi obj)
        {
            throw new NotImplementedException();
        }
    }
}
