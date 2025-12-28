using EtiquetasLogistica.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace EtiquetasLogistica.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            string pastaApp = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "EtiquetasLogistica"
            );

            Directory.CreateDirectory(pastaApp);

            string caminhoDb = Path.Combine(pastaApp, "EtiquetasLogistica.db");
            _connectionString = $"Data Source={caminhoDb}";

            CriarBanco();
        }

        private void CriarBanco()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS HistoricoImpressoes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Destinatario TEXT NOT NULL,
                    NotaFiscal TEXT NOT NULL,
                    TotalVolumes INTEGER NOT NULL,
                    DataImpressao TEXT NOT NULL
                );
                ";

                cmd.ExecuteNonQuery();
            }
        }

        public void SalvarImpressao(
            string destinatario,
            string notaFiscal,
            int totalVolumes)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText =
                @"
                INSERT INTO HistoricoImpressoes
                (Destinatario, NotaFiscal, TotalVolumes, DataImpressao)
                VALUES ($dest, $nf, $total, $data)
                ";

                cmd.Parameters.AddWithValue("$dest", destinatario);
                cmd.Parameters.AddWithValue("$nf", notaFiscal);
                cmd.Parameters.AddWithValue("$total", totalVolumes);
                cmd.Parameters.AddWithValue("$data", DateTime.Now.ToString("dd/MM/yy HH:mm"));

                cmd.ExecuteNonQuery();
            }
        }

        public List<HistoricoImpressao> ObterHistorico()
        {
            var lista = new List<HistoricoImpressao>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText =
                @"
                SELECT Id, Destinatario, NotaFiscal, TotalVolumes, DataImpressao
                FROM HistoricoImpressoes
                ORDER BY Id DESC
                ";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new HistoricoImpressao
                        {
                            Id = reader.GetInt32(0),
                            Destinatario = reader.GetString(1),
                            NotaFiscal = reader.GetString(2),
                            TotalVolumes = reader.GetInt32(3),
                            DataImpressao = reader.GetString(4)
                        });
                    }
                }
            }

            return lista;
        }
    }
}
