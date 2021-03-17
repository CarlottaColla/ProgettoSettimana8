using MostriVsEroi.ADORepository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADORepository
{
    public class ADOClassiRepository : IClassiRepository
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
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Classi";

                SqlDataReader reader = command.ExecuteReader();
                List<Classi> listaClassi = new List<Classi>();

                while (reader.Read())
                {
                    listaClassi.Add(reader.ToClasse());
                }

                reader.Close();
                connection.Close();
                return listaClassi;
            }
        }

        public IEnumerable<Classi> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
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
