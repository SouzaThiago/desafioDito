using System;
using System.Collections.Generic;

namespace DesafioDito.Adapter.Model
{
    public class EventoCompra
    {
        public DateTime Timestamp { get; set; }
        
        public int Revenue { get; set; }

        public string StorageName { get; set; }

        public string TransactionId { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
