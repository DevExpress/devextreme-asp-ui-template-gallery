using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Services;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class DemoDbContext : DbContext {
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options) { }
    }
}
