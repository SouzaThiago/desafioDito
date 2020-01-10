using Dapper;
using Dapper.Contrib.Extensions;
using DesafioDito.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace DesafioDito.Data
{
    public class UnitOfWork
    {
        private readonly string dataDirectory = string.Empty;
        private readonly string stringConnection;
        private readonly string basePath;
        private readonly string pathDataBase;
        readonly IConfiguration configuration;

        public UnitOfWork()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appSettings.json");

            configuration = builder.Build();

            basePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            pathDataBase = configuration.GetSection("PathDataBase").Value;
            dataDirectory = basePath + pathDataBase;
            stringConnection = string.Format($@"Server=(localdb)\mssqllocaldb; Integrated Security=true; AttachDbFileName={dataDirectory};");
        }

        public bool InserirEvento(string nome, DateTime timestamp)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(stringConnection))
                {
                    Evento evento = new Evento
                    {
                        Nome = nome,
                        Timestamp = timestamp
                    };

                    var newTeste = db.Insert(new Evento { Nome = nome, Timestamp = timestamp });
                }
                return true;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public List<Evento> ListarEventos()
        {
            List<Evento> listaEventos = new List<Evento>();

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                listaEventos = db.GetAll<Evento>().ToList();
            }

            return listaEventos;
        }

        public Evento ObterEventos(string nome)
        {
            Evento evento = new Evento
            {
                Nome = nome,
            };

            using (IDbConnection db = new SqlConnection(stringConnection))
            {
                evento = db.QueryFirst<Evento>("Select Id as Id, Timestamp as Timestamp, Nome as Nome From Evento where Nome = @nome", evento);
            }

            return evento;
        }
    }
}
