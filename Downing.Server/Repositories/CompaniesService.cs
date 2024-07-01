using Downing.Server.DataContext;
using Downing.Server.Models;
using Downing.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Downing.Server.Repositories
{
    public class CompaniesService : ICompaniesService
    {
        private readonly DowningDbContext _context;
        public CompaniesService(DowningDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompaniesList()
        {
            return await _context.Companies
                            .OrderBy(c => c.CompanyName)
                            .ToListAsync();
        }

        public async Task<bool> CheckUniqueValue(string value)
        {
            var exists = await _context.Companies
            .AnyAsync(e => e.Code == value);

            return exists;
        }

        public async Task<Company> CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }
    }
}
