using DesafioDito.Adapter.Model;
using Refit;
using System.Threading.Tasks;

namespace DesafioDito.Adapter
{
    public interface IDadosDitoAdapter
    {

        [Get("/dito-questions/events.json")]
        Task<Response>GetEventsAsync();
    }
}
