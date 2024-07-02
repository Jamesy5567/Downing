using Downing.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Downing.Server.Interfaces
{
    // Interface to call functions in the repo.
    public interface ICompaniesService
    {
        Task<IEnumerable<Company>> GetCompaniesList();
        Task<bool> CheckUniqueValue(string value);
        Task<Company> CreateCompany(Company company);
    }
}
