using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data.Entities;
using Server.Dtos;

namespace Server.Data
{
    public class DataContext: DbContext
    {
        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=user;Password=postgres;Database=TodoAppDb");
    }
}