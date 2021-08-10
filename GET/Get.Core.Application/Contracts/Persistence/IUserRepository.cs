using GET.Core.Application.Models.Authentication;
using GET.Core.Domain;
using System.Threading.Tasks;

namespace Get.Core.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
        AuthenticationResponse AuthenticateAsync(User user);
    }
}
