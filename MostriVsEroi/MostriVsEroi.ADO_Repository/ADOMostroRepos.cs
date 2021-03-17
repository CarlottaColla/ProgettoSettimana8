using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOMostroRepos : IMostroRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Mostri obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Mostri obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mostri> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Mostri";

                SqlDataReader reader = command.ExecuteReader();
                List<Mostri> listaMostri = new List<Mostri>();

                while (reader.Read())
                {
                    listaMostri.Add(reader.ToMostro());
                }

                reader.Close();
                connection.Close();
                return listaMostri;
            }
        }

        //Gli passo il livello dell'eroe
//TODO: Da cambiare ID in filtro!!!
        public IEnumerable<Mostri> GetAllWithFilter(int ID)
        {
            List<Mostri> mostri = new List<Mostri> (GetAll());
            List<Mostri> mostriUtilizzabili = new List<Mostri>();
            foreach(var item in mostri)
            {
                if(item.Livello == ID)
                {
                    mostriUtilizzabili.Add(item);
                }
            }
            return mostriUtilizzabili;
        }

        public Mostri GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Mostri obj)
        {
            throw new NotImplementedException();
        }
    }
}
