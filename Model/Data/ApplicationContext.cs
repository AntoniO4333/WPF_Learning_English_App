using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheremushkinae_107d2.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DbContextConnectionString") { }

        public DbSet<User> Users { get; set; }
        public DbSet<LearnDict> LearnDicts { get; set; }
        public DbSet<KnowDict> KnowDicts { get; set; }

        public ApplicationContext(DbSet<User> users, DbSet<LearnDict> learnDicts, DbSet<KnowDict> knowDicts)
        {
            Users = users;
            LearnDicts = learnDicts;
            KnowDicts = knowDicts;
        }
    }
}
