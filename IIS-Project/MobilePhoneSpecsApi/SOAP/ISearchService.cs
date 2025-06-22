using MobilePhoneSpecsApi.DTOs;
using System.ServiceModel;

namespace MobilePhoneSpecsApi.SOAP
{
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        Task<IEnumerable<SpecificationDto>> Search(string query);
    }
}
