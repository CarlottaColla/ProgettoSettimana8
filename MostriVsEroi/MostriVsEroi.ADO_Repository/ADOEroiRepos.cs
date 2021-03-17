using MostriVsEroi.ADO_Repository.Estensioni;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository
{
    public class ADOEroiRepos : IEroiRepository
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = true; Initial Catalog=MostriVsEroi; Server = .\SQLEXPRESS";
        public void Create(Eroi obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO Eroi VALUES (@Nome,@Classe,@Arma,@Livello,@PuntiVita,@Giocatore,@TempoTotale);";

                command.Parameters.AddWithValue("@Nome", obj.Nome);
                command.Parameters.AddWithValue("@Classe", obj.Classe);
                command.Parameters.AddWithValue("@Arma", obj.Arma);
                command.Parameters.AddWithValue("@Livello", obj.Livello);
                command.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);
                command.Parameters.AddWithValue("@Giocatore", obj.Giocatore);
                command.Parameters.AddWithValue("TempoTotale", obj.TempoTotale);

                int eseguita = command.ExecuteNonQuery();
                if (eseguita == 1)
                {
                    Console.WriteLine("Eroe inserito correttamente");
                }
                else
                {
                    Console.WriteLine("Errore, eroe non inserito.");
                }
            }
        }

        public bool Delete(Eroi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Eroi> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Eroi";

                SqlDataReader reader = command.ExecuteReader();
                List<Eroi> listaEroi = new List<Eroi>();

                while (reader.Read())
                {
                    listaEroi.Add(reader.ToEroe());
                }

                reader.Close();
                connection.Close();
                return listaEroi;
            }
        }

        public IEnumerable<Eroi> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
        }

        public Eroi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Eroi obj)
        {
            throw new NotImplementedException();
        }
    }
}

