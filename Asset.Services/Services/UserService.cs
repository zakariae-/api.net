using System;
using System.Threading.Tasks;
using Asset.Core.Interfaces;
using Asset.Services.Interfaces;

namespace Asset.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddAsync()
        {
            throw new NotImplementedException();
        }
    }
}
