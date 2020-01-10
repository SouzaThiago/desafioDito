using DesafioDito.Adapter;
using DesafioDito.Adapter.Model.Api;
using DesafioDito.Adapter.Model;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesafioDito.Service
{
    public class ManipulacaoDadosService
    {
        Dictionary<string, string> dicio;
        readonly List<Produto> produtos;
        readonly List<Loja> lojas;
        readonly IConfiguration configuration;

        public ManipulacaoDadosService()
        {
            dicio = new Dictionary<string, string>();
            produtos = new List<Produto>();
            lojas = new List<Loja>();

            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appSettings.json");
            configuration = builder.Build();
        }

        public IOrderedEnumerable<EventoCompra> ManipularDadosDito()
        {
            var resposta = ObterDadosDito();

            foreach (var evento in resposta.events)
            {
                dicio = new Dictionary<string, string>();
                foreach (var customData in evento.CustomData)
                {
                    dicio.Add(customData.Key, customData.Value);
                }

                if (evento.Revenue != null)
                {
                    var loja = new Loja()
                    {
                        Revenue = evento.Revenue,
                        Transaction_id = dicio["transaction_id"],
                        Store_name = dicio["store_name"]
                    };
                    lojas.Add(loja);
                }
                else
                {
                    var produto = new Produto()
                    {
                        Name = dicio["product_name"],
                        Price = dicio["product_price"],
                        Transaction_id = dicio["transaction_id"],
                        Timestamp = evento.Timestamp
                    };
                    produtos.Add(produto);
                }
            }

            return ObterComprasComLojas(lojas, produtos);
    }

        private Response ObterDadosDito()
        {
            var dito = RestService.For<IDadosDitoAdapter>(configuration.GetSection("BaseUrlDito").Value);
            return dito.GetEventsAsync().Result;
        }
        private IOrderedEnumerable<EventoCompra> ObterComprasComLojas(List<Loja> lojas, List<Produto> produtos)
        {
            List<EventoCompra> eventoCompras = new List<EventoCompra>();
            foreach (var loja in lojas)
            {
                List<Produto> p = new List<Produto>();
                var evento = new EventoCompra
                {
                    Revenue = loja.Revenue.Value,
                    TransactionId = loja.Transaction_id,
                    StorageName = loja.Store_name
                };

                foreach (var produto in produtos)
                {

                    if (loja.Transaction_id == produto.Transaction_id)
                    {
                        evento.Timestamp = produto.Timestamp;
                        evento.Produtos.Add(produto);
                    }
                }

                eventoCompras.Add(evento);
            }
            var order = eventoCompras.OrderBy(t => t.Timestamp);
            return order;
        }
    }
}

