using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        IEnumerable<string> GetUsersLastNameByFirstName(string firstName);
        string GetUserFirstNameByLastName(string lastName);
    }
}