using DevExtremeVSTemplateMVC.DAL;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace DevExtremeVSTemplateMVC.Services
{
    public interface IDataSeeder {
        Task SeedFromFileDbAsync(RwaContext context, string dbPath);
    }

    public class DataSeeder : IDataSeeder {
        public async Task SeedFromFileDbAsync(RwaContext context, string dbPath) {
            var masterOptions = new DbContextOptionsBuilder<RwaContext>().UseSqlite($"Data Source={dbPath}").Options;
            using var master = new RwaContext(masterOptions);
            var tasks = master.Tasks.AsNoTracking().ToList();
            var contacts = master.Contacts.AsNoTracking().ToList();
            context.Tasks.AddRange(tasks);
            context.Contacts.AddRange(contacts);
            await context.SaveChangesAsync();
        }
    }
}
