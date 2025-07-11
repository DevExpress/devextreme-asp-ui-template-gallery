using DevExtremeVSTemplateMVC.DAL;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.Services
{
    public interface IDataSeeder {
        Task SeedFromFileDbAsync(DemoDbContext context, string dbPath);
    }

    public class DataSeeder : IDataSeeder {
        public async Task SeedFromFileDbAsync(DemoDbContext context, string dbPath) {
            var masterOptions = new DbContextOptionsBuilder<DemoDbContext>().UseSqlite($"Data Source={dbPath}").Options;
            using var master = new DemoDbContext(masterOptions);
            var tasks = master.Tasks.AsNoTracking().ToList();
            var contacts = master.Contacts.AsNoTracking().ToList();
            var taskLists = master.TaskLists.AsNoTracking().ToList();
            context.Tasks.AddRange(tasks);
            context.Contacts.AddRange(contacts);
            context.TaskLists.AddRange(taskLists);
            await context.SaveChangesAsync();
        }
    }
}
