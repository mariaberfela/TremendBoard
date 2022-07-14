using System.Collections.Generic;
using System.Linq;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly IUserRepository _userRepository;

        public UserRepository(IUserRepository userRepository, TremendBoardDbContext context) : base(context)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<string> GetUsersLastNameByFirstName(string firstName)
        {
            var users = _userRepository.GetUsersLastNameByFirstName(firstName);
            return users;
        }

        public string GetUserFirstNameByLastName(string lastName)
        {
            var user = _userRepository.GetUserFirstNameByLastName(lastName);
            return user;
        }
    }
}
