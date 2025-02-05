﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Downing.Server.DataContext;
using Downing.Server.Models;
using Downing.Server.Interfaces;
using System.Numerics;

namespace Downing.Server.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DowningDbContext _context;
        private readonly ICompaniesService _companiesService;

        public CompaniesController(DowningDbContext context, ICompaniesService companiesService)
        {
            _context = context;
            _companiesService = companiesService;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return await _companiesService.GetCompaniesList();
        }

        [HttpGet("check-unique/{value}")]
        public async Task<IActionResult> CheckUniqueValue(string value)
        {
            var exists = await _companiesService.CheckUniqueValue(value);
            return Ok(new { exists });
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany([FromBody] Company company)
        {
            try {
                await _companiesService.CreateCompany(company);

                return Ok(new { message = "Record created successfully" });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred while creating the record", details = ex.Message });
            }                     
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
