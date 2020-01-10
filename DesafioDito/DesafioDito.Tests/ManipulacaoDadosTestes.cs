using DesafioDito.Adapter.Model;
using DesafioDito.Data;
using DesafioDito.Model;
using DesafioDito.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DesafioDito.Tests
{
    public class ManipulacaoDadosTestes
    {
        private readonly ManipulacaoDadosService manipulacaoDadosService;
        private readonly UnitOfWork unitOfWork;
        
        public ManipulacaoDadosTestes()
        {
            manipulacaoDadosService = new ManipulacaoDadosService();
            unitOfWork = new UnitOfWork();
        }

        [Fact]
        public void ServiceSucessoTeste()
        {
           var listaOrdenada =  manipulacaoDadosService.ManipularDadosDito();
           Assert.IsAssignableFrom<IOrderedEnumerable<EventoCompra>>(listaOrdenada);
        }

        [Fact]
        public void InserirEventoSucessoTeste()
        {
           var result =  unitOfWork.InserirEvento("venda", DateTime.Now);
            Assert.True(result);
        }

        [Fact]
        public void ListarEventosTeste()
        {
            var x = unitOfWork.ListarEventos();
            Assert.IsType<List<Evento>>(x);
        }

        [Fact]
        public void InserirEventoFalhaTeste()
        {
            var result = unitOfWork.InserirEvento(null, DateTime.Now);
            Assert.False(result);
        }
    }
}
