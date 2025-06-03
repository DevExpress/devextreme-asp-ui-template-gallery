using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.DAL
{
    public static class DemoDataFetcher
    {
        static Dictionary<string, string> apiMapping = new Dictionary<string, string>() {
            { nameof(DemoDbContext.Tasks), "/Employees/AllTasks" },
            { "GetContact", "/Users/Contacts/{0}" },
        };

        public static Task DownloadTask = null;

        public static async Task Download(HttpClient httpClient, IConfiguration config) {
            string baseUrlAPI = config.GetValue<string>(ConfigKeys.BaseUrlAPIKey);
            IList<EmployeeTask> allTasks = await FetchListFromApiAsync<EmployeeTask>(httpClient, baseUrlAPI + apiMapping[nameof(DemoDbContext.Tasks)]);
            for (int i = 0; i < allTasks.Count; i++) {
                allTasks[i].TaskId = i + 1;
            }
            Contact contact = await FetchEntityFromApiAsync<Contact>(httpClient, baseUrlAPI + string.Format(apiMapping["GetContact"], DemoConsts.DemoUserProfileId));
            contact.Activities = null;
            contact.Opportunities = null;

            Directory.CreateDirectory(config.GetValue<string>("DatabasePathDirectory"));

            string databasePath = string.Format("{0}/{1}",
                config.GetValue<string>(ConfigKeys.DatabasePathDirectoryKey),
                config.GetValue<string>(ConfigKeys.DatabaseFileNameKey));
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlite($"Data Source={databasePath}")
                .Options;

            using var db = new DemoDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Tasks.AddRange(allTasks);
            db.Contacts.Add(contact);

            await db.SaveChangesAsync();
        }

        public static async Task<IList<T>> FetchListFromApiAsync<T>(HttpClient httpClient, string actionUrl) {
            var response = await httpClient.GetAsync(actionUrl);

            if (response.IsSuccessStatusCode) {
                var allData = await response.Content.ReadFromJsonAsync<IList<T>>();
                return allData;
            }

            throw new HttpRequestException("Failed to fetch data from a remote endpoint");
        }

        public static async Task<T> FetchEntityFromApiAsync<T>(HttpClient httpClient, string actionUrl) {
            var response = await httpClient.GetAsync(actionUrl);
            if (response.IsSuccessStatusCode) {
                var entity = await response.Content.ReadFromJsonAsync<T>();
                return entity;
            }

            throw new HttpRequestException("Failed to fetch data from a remote endpoint");
        }
    }
}
