using System.Threading.Tasks;
using Common.Models;
using Common.Models.RestApi;

namespace ApiConnection.Contracts
{
    public interface IRequestManager
    {
        Task<Root> GetRequestAsync(ConfigurationModel configuration);
    }
}