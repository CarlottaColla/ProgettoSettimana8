using MostriVsEroi.Core.Entità;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADO_Repository.Estensioni
{
    public static class SqlDataReaderExtension
    {
        public static Eroi ToEroe(this SqlDataReader reader)
        {
            return new Eroi()
            {
                ID = (int)reader["ID"],
                Nome = reader["Nome"].ToString(),
                Classe = (int)reader["Classe_ID"],
                Arma = (int)reader["Arma_ID"],
                Livello = (int)reader["Livello_ID"],
                PuntiVita = (int)reader["PuntiVita"],
                Giocatore = (int)reader["Giocatore_ID"],
                TempoTotale = (int)reader["TempoTotale"]
            };
        }

        public static Classi ToClasse(this SqlDataReader reader)
        {
            return new Classi()
            {
                ID = (int)reader["ID"],
                Nome = reader["Nome"].ToString(),
                IsEroe = (bool)reader["IsEroe"]
            };
        }

        public static Armi ToArmi(this SqlDataReader reader)
        {
            return new Armi()
            {
                ID = (int)reader["ID"],
                Nome = reader["Nome"].ToString(),
                ClassiID = (int)reader["Classe_ID"],
                PuntiDanno = (int)reader["PuntiDanno"]
            };
        }

        public static Giocatori ToGiocatori(this SqlDataReader reader)
        {
            return new Giocatori()
            {
                ID = (int)reader["ID"],
                Nome = reader["Nome"].ToString(),
                Ruolo_ID = (int)reader["Ruolo_ID"]
            };
        }
    }
}
