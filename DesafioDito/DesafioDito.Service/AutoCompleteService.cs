using DesafioDito.Data;
using DesafioDito.Model;
using System;
using System.Collections.Generic;

namespace DesafioDito.Service
{
   public class AutoCompleteService
    {
        private readonly UnitOfWork unitOfWork;
        
        public AutoCompleteService()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<Evento> ListarEventos()
        {
            var lista = unitOfWork.ListarEventos();

            return lista;
        }
        public void InserirEvento(string nome, DateTime timestamp)
        {
            unitOfWork.InserirEvento(nome, timestamp);
        }

        public Evento ObterEventos(string nome)
        {
            return unitOfWork.ObterEventos(nome);
        }
    }
}
