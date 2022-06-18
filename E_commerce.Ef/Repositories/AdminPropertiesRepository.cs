using E_commerce.Core.Interfaces.IRepositories;
using E_commerce.Core.Models;
using E_commerce.Ef.Data;
using E_commerce.Ef.Repositores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Ef.Repositories
{
    public class AdminPropertiesRepository : BaseRepository<AdminProperties>, IAdminPropertiesRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminPropertiesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AdminActivity>> GetAdminAcitviesByEmailAsync(string email, int year, int month, int day)
        {
            var adminId = await _context.Set<ApplicationUser>().Where(admin 
                => admin.Email == email).Select(admin 
                    => admin.AdminProperties.Id).SingleOrDefaultAsync();
            
            IQueryable<AdminActivity> query = _context.Set<AdminActivity>().Where(active 
                => active.AdminId == adminId);

            if (year == 0)
                return await query.ToListAsync();

            if (month == 0)
                return await query.Where(active => active.DateTime.Year == year).ToListAsync();

            if (day == 0)
                return await query.Where(active => active.DateTime.Year == year
                    && active.DateTime.Month == month).ToListAsync();

            return await query.Where(active => active.DateTime.Year == year
                    && active.DateTime.Month == month
                    && active.DateTime.Day == day).ToListAsync();
        }

        public async Task<Branch> GetAdminBranchByAdminIdAsync(string id)
            => await _context.Set<ApplicationUser>().Where(admin => admin.Id == id)
                .Include(user => user.AdminProperties)
                .ThenInclude(admin => admin.Branch)
                .Select(admin => admin.AdminProperties.Branch).SingleOrDefaultAsync();
        
        public async Task<ApplicationUser> GetAdminDetailsByIdAsync(string email)
            => await _context.Set<ApplicationUser>().Where(admin => admin.Email == email)
                .Include(admin => admin.AdminProperties)
                .ThenInclude(admin => admin.Activities.Where(active
                    => active.DateTime.Month == DateTime.Now.Month
                    && active.DateTime.Year == DateTime.Now.Year))
                .Include(admin => admin.AdminProperties)
                .ThenInclude(admin => admin.Branch)
                .Include(admin => admin.City)
                .ThenInclude(admin => admin.Country)
                .SingleOrDefaultAsync();

        public async Task<byte[]> GetProfilePictureByIdAsync(string id)
            => await _context.Set<AdminProperties>()
                .Where(admin => admin.UserId == id)
                .Select(admin => admin.ProfilePicture)
                .SingleOrDefaultAsync();

        public byte[] GetProfilePictureByEmailAsync(string email)
        {
            string id = _context.Set<ApplicationUser>().Where(user => user.Email == email).Select(user => user.Id).SingleOrDefault();

            byte[] picture = _context.Set<AdminProperties>()
                .Where(admin => admin.UserId == id)
                .Select(admin => admin.ProfilePicture)
                .SingleOrDefault();

            return picture;
        }
            
    }
}