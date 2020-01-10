using DesafioDito.Adapter.Model.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioDito.Adapter.Model
{
    public class Response
    {
        public IEnumerable<Events> events { get; set; }
    }
}
