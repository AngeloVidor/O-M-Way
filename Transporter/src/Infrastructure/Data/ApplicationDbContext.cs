using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Application.Models;
using src.Application.UseCases.GenerateVerificationCode.Implementations;
using src.Domain.Entities;

namespace src.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TransporterCompany> Transporters { get; set; }
        public DbSet<VerificationCodeModel> VerificationCodes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PendingRegistration> TransporterPreRegistrations { get; set; }
        public DbSet<PendingLocation> PendingLocations { get; set; }



    }
}