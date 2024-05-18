using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BodyBank.Models;
using BodyBank.Model;
using BodyBank.Authentification;

namespace BodyBank.Data
{
    public class MVCBodyBankContext : IdentityDbContext<IdentityUser>
    {
        public MVCBodyBankContext(DbContextOptions<MVCBodyBankContext> options)
            : base(options)
        {

        }
        public DbSet<Addresse> Addresse { get; set; }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<CommandeOrgane> CommandeOrgane { get; set; }
        public DbSet<Donneur> Donneur { get; set; }
        public DbSet<Organne> Organne { get; set; }
        public DbSet<Model.Type> Type { get; set; }
        public DbSet<Authentification.Utilisateur> Util { get; set; }
    }
}
