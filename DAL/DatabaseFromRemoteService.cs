using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.DAL
{
    public static class DatabaseFromRemoteService
    {

        static Dictionary<string, string> apiMapping = new Dictionary<string, string>() {
            { nameof(RwaContext.Tasks), "/Employees/AllTasks" },
            { nameof(RwaContext.FilteredTasks), "/Employees/FilteredTasks" }
        };

        public static async Task Download(HttpClient httpClient) {
            IList<EmployeeTask> allTasks = await FetchFromApiAsync<EmployeeTask>(httpClient, apiMapping[nameof(RwaContext.Tasks)]);
            for (int i = 0; i < allTasks.Count; i++) {
                allTasks[i].TaskId = i + 1;
            }

            IList<FilteredTask> filteredTasks = await FetchFromApiAsync<FilteredTask>(httpClient, apiMapping[nameof(RwaContext.FilteredTasks)]);

            Directory.CreateDirectory(Constants.DatabasePathDirectory);

            var options = new DbContextOptionsBuilder<RwaContext>()
                .UseSqlite($"Data Source={Constants.DatabasePath}")
                .Options;

            using var db = new RwaContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Tasks.AddRange(allTasks);
            db.FilteredTasks.AddRange(filteredTasks);

            await db.SaveChangesAsync();
        }

        public static async Task<IList<T>> FetchFromApiAsync<T>(HttpClient httpClient, string actionUrl) {
            var response = await httpClient.GetAsync(Constants.BaseUrlAPI + actionUrl); // Unified API for all data

            if (response.IsSuccessStatusCode) {
                var allData = await response.Content.ReadFromJsonAsync<IList<T>>();
                return allData;
            }

            throw new HttpRequestException("Failed to fetch data from a remote endpoint");
        }
    }
}
