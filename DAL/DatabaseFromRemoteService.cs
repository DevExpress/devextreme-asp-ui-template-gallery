using DevExtremeVSTemplateMVC.Models;
using DevExtremeVSTemplateMVC.Utils;
using Microsoft.EntityFrameworkCore;

namespace DevExtremeVSTemplateMVC.DAL
{
    public static class DatabaseFromRemoteService
    {
        static Dictionary<string, string> apiMapping = new Dictionary<string, string>() {
            { nameof(RwaContext.Tasks), "/Employees/AllTasks" },
            { nameof(RwaContext.Contacts), "/Users/Contacts" },
            { "GetContact", "/Users/Contacts/{0}" },
        };

        const int CONTACT_ID = 22;

        public static async Task Download(HttpClient httpClient) {
            IList<EmployeeTask> allTasks = await FetchListFromApiAsync<EmployeeTask>(httpClient, apiMapping[nameof(RwaContext.Tasks)]);
            for (int i = 0; i < allTasks.Count; i++) {
                allTasks[i].TaskId = i + 1;
            }
            Contact contact = await FetchEntityFromApiAsync<Contact>(httpClient, string.Format(apiMapping["GetContact"], CONTACT_ID));
            contact.Activities = null;
            contact.Opportunities = null;

            Directory.CreateDirectory(Constants.DatabasePathDirectory);

            var options = new DbContextOptionsBuilder<RwaContext>()
                .UseSqlite($"Data Source={Constants.DatabasePath}")
                .Options;

            using var db = new RwaContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Tasks.AddRange(allTasks);
            db.Contacts.Add(contact);

            await db.SaveChangesAsync();
        }

        public static async Task<IList<T>> FetchListFromApiAsync<T>(HttpClient httpClient, string actionUrl) {
            var response = await httpClient.GetAsync(Constants.BaseUrlAPI + actionUrl);

            if (response.IsSuccessStatusCode) {
                var allData = await response.Content.ReadFromJsonAsync<IList<T>>();
                return allData;
            }

            throw new HttpRequestException("Failed to fetch data from a remote endpoint");
        }

        public static async Task<T> FetchEntityFromApiAsync<T>(HttpClient httpClient, string actionUrl) {
            var response = await httpClient.GetAsync(Constants.BaseUrlAPI + actionUrl);
            if (response.IsSuccessStatusCode) {
                var entity = await response.Content.ReadFromJsonAsync<T>();
                return entity;
            }

            throw new HttpRequestException("Failed to fetch data from a remote endpoint");
        }
    }
}
