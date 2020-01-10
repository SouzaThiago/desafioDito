using Dapper.Contrib.Extensions;
using System;

namespace DesafioDito.Model
{
    [Table("Evento")]
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
