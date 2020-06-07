﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NYSCFileRecord.Models;

namespace NYSCFileRecord.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FileRecord> FileRecord { get; set; }
        public DbSet<CDSRecord> CDSRecord { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<UserAppRole> UserAppRole { get; set; }
    }
}
