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

            int openIndex = 1;
            int inProgressIndex = 1;
            int deferredIndex = 1;
            int completedIndex = 1;

            for (int i = 0; i < allTasks.Count; i++) {
                allTasks[i].TaskId = i + 1;

                if (allTasks[i].Status == "Open") {
                    allTasks[i].OrderIndex = openIndex++;
                } else if (allTasks[i].Status == "In Progress") {
                    allTasks[i].OrderIndex = inProgressIndex++;
                } else if (allTasks[i].Status == "Deferred") {
                    allTasks[i].OrderIndex = deferredIndex++;
                } else if (allTasks[i].Status == "Completed") {
                    allTasks[i].OrderIndex = completedIndex++;
                }
            }



            Contact contact = await FetchEntityFromApiAsync<Contact>(httpClient, baseUrlAPI + string.Format(apiMapping["GetContact"], DemoConsts.DemoUserProfileId));
            contact.Activities = null;
            contact.Opportunities = null;

            IList<TaskList> taskLists = new List<TaskList>
            {
                new TaskList { Id = 1, ListName = "Open", OrderIndex = 1},
                new TaskList { Id = 2, ListName = "In Progress", OrderIndex = 2},
                new TaskList { Id = 3, ListName = "Deferred", OrderIndex = 3},
                new TaskList { Id = 4, ListName = "Completed", OrderIndex = 4}
            };

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
            db.TaskLists.AddRange(taskLists);

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
