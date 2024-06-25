using Posterr.Domain.Entities;
using Posterr.Domain.Interfaces;
using Posterr.Repository.Repositories;

namespace Posterr.Application.Services {
    public class UserService : ServiceBase<User>, IServiceBase<User> {

        public UserService(UserRepository userRepository) : base(userRepository) { }

    }
}
