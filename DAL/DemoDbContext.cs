using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Services;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.DAL
{
    public class DemoDbContext : DbContext {
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<TaskList> TaskLists { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options) { }

        public override int SaveChanges() {
            var newTasks = ChangeTracker.Entries<EmployeeTask>()
                .Where(e => e.State == EntityState.Added);
            foreach (var entry in newTasks) {
                if (entry.Entity.TaskId == 0) {
                    var maxOrder = Math.Max(1000, Tasks.Max(t => t.TaskId));
                    entry.Entity.TaskId = maxOrder + 1;
                }
            }
            return base.SaveChanges();
        }
    }
}
