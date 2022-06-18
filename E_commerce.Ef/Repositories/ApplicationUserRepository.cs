using E_commerce.Core.Interfaces.IRepositories;
using E_commerce.Core.Models;
using E_commerce.Ef.Data;
using E_commerce.Ef.Repositores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Ef.Repositories
{
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<string> GetFirstNameByIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Select(user => user.FirstName).SingleOrDefaultAsync();

        public async Task<string> GetLastNameByIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Select(user => user.LastName).SingleOrDefaultAsync();

        public async Task<string> GetPhoneNumberByIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Select(user => user.PhoneNumber).SingleOrDefaultAsync();

        public async Task<City> GetUserCityByUserIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Include(user => user.City).Select(user => user.City).SingleOrDefaultAsync();

        public async Task<string> GetUserEmailByIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Select(user => user.Email).SingleOrDefaultAsync();

        public async Task<string> GetUserNameByIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(user => user.Id == id)
            .Select(user => user.UserName).SingleOrDefaultAsync();

        public bool ISActive(string id)
            => _context.Set<ApplicationUser>()
                .Where(user => user.Id == id)
                .Select(user => user.IsActive)
                .SingleOrDefault();

        public async Task<bool> ISActiveAsync(string id)
            => await _context.Set<ApplicationUser>()
                .Where(user => user.Id == id)
                .Select(user => user.IsActive)
            .SingleOrDefaultAsync();

        public async Task<bool> ISActiveByUserNameAsync(string username)
            => await _context.Set<ApplicationUser>()
                .Where(user => user.UserName == username)
                .Select(user => user.IsActive)
                .SingleOrDefaultAsync();
    }
}