using MostriVsEroi.Core.Entità;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MostriVsEroi.ADORepository.Estensioni
{
    public static class DataReaderExtension
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
    }
}
