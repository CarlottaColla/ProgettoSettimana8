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
        public bool Create(Eroi obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "INSERT INTO Eroi VALUES (@Nome,@Classe,@Arma,@Livello,@PuntiVita,@Giocatore,@TempoTotale,@Punti);";

                    command.Parameters.AddWithValue("@Nome", obj.Nome);
                    command.Parameters.AddWithValue("@Classe", obj.Classe);
                    command.Parameters.AddWithValue("@Arma", obj.Arma);
                    command.Parameters.AddWithValue("@Livello", obj.Livello);
                    command.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);
                    command.Parameters.AddWithValue("@Giocatore", obj.Giocatore);
                    command.Parameters.AddWithValue("@TempoTotale", obj.TempoTotale);
                    command.Parameters.AddWithValue("@Punti", obj.Punti);

                    command.ExecuteNonQuery();
                    connection.Close();
                    Console.WriteLine("Eroe inserito correttamente");
                    return true;
                }
                catch (SqlException)
                {
                    connection.Close();
                    Console.WriteLine("Errore, eroe non inserito.");
                }
                return false;
            }
        }

        public bool Delete(Eroi obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "DELETE FROM Eroi WHERE ID = @ID";

                    command.Parameters.AddWithValue("@ID", obj.ID);

                    command.ExecuteNonQuery();
                    //Console.WriteLine("Eroe eliminato con successo");
                    return true;
                }
                catch (SqlException)
                {
                    Console.WriteLine("Impossibile eliminare l'eroe");
                }
                finally
                {
                    connection.Close();
                }
                return false;
            }
        }

        public IEnumerable<Eroi> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Eroi> listaEroi = new List<Eroi>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Eroi";

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaEroi.Add(reader.ToEroe());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAll in eroi");
                }
                finally
                {
                    connection.Close();
                }
                return listaEroi;
            }
        }

        public IEnumerable<Eroi> GetAllWithFilter(int ID)
        {
            //Prende la lista degli eroi di un determinato giocatore
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Eroi> listaEroi = new List<Eroi>();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Eroi WHERE Giocatore_ID = @ID";

                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listaEroi.Add(reader.ToEroe());
                    }
                    reader.Close();
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore in GetAllWithFilter in eroi");
                }
                finally
                {
                    connection.Close();
                }
                return listaEroi;
            }
        }

        public Eroi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Eroi obj)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "UPDATE Eroi SET Livello_ID = @Livello, PuntiVita = @PuntiVita, TempoTotale = @TempoTotale, Punti = @Punti WHERE ID = @id;";

                    command.Parameters.AddWithValue("@ID", obj.ID);
                    command.Parameters.AddWithValue("@Livello", obj.Livello);
                    command.Parameters.AddWithValue("@PuntiVita", obj.PuntiVita);
                    command.Parameters.AddWithValue("@TempoTotale", obj.TempoTotale);
                    command.Parameters.AddWithValue("@Punti", obj.Punti);

                    command.ExecuteNonQuery();
                    //Console.WriteLine("Eroe modificato correttamente");
                    return true;
                }
                catch (SqlException)
                {
                    Console.WriteLine("Errore, eroe non inserito.");
                }
                finally
                {
                    connection.Close();
                }
                return false;
            }
        }
    }
}

