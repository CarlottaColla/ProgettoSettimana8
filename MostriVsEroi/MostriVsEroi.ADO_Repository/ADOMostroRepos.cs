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
        public bool Create(Mostri obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO Mostri VALUES (@Nome,@Classe,@Arma,@Livello,@PuntiVita)";

                    command.Parameters.AddWithValue("@Nome", obj.Nome);
                    command.Parameters.AddWithValue("@Classe", obj.Classe);
                    command.Parameters.AddWithValue("@Arma", obj.Arma);
                    command.Parameters.AddWithValue("@Livello", obj.Livello);
                    command.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Mostro inserito correttamente");
                    connection.Close();
                    return true;
                }
                catch (SqlException)
                {
                    connection.Close();
                    Console.WriteLine("Inserimento mostro non riusicto");
                }
                return false;
            }
        }

        public bool Delete(Mostri obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "DELETE FROM Mostri WHERE ID = @ID";

                    command.Parameters.AddWithValue("ID", obj.ID);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Mostro eliminato");
                    return true;
                }
                catch (SqlException)
                {
                    Console.WriteLine("Eliminazione del mostro non riusicta");
                }
                finally
                {
                    connection.Close();
                }
                return false;
            }
        }

        public IEnumerable<Mostri> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Mostri> listaMostri = new List<Mostri>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Mostri";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaMostri.Add(reader.ToMostro());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAll in mostri");
                }
                finally
                {
                    connection.Close();
                }
                return listaMostri;
            }
        }

        //Non sono implementate perchè non è possibile eseguire queste operazioni
        public IEnumerable<Mostri> GetAllWithFilter(int filtro)
        {
            throw new NotImplementedException();
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
