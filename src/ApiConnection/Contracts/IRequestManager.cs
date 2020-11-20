using System.Threading.Tasks;
using Common.Models.RestApi;

namespace Communication.Contracts
{
    public interface IRequestManager
    {
        Task<RestApiModel> GetRequestAsync();
    }
}