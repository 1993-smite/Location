using Dadata.Model;
using System.Threading.Tasks;

namespace DadataLocation.Services.Interfaces
{
    interface IPhoneService
    {
        Task<DadataLocation.Models.Phone> GetPhone(string number);
    }
}
