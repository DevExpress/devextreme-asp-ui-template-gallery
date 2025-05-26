using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Services;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class RwaContext : DbContext {
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public RwaContext(DbContextOptions<RwaContext> options)
            : base(options) { }
    }
}
