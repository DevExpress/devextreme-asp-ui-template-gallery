namespace DevExtremeVSTemplateMVC.Models
{
    static class TaskData
    {
        public static List<TaskModel> Tasks = new List<TaskModel>()
        {
            new TaskModel { Id = 1, Text = "Call to clarify customer requirements.", StartDate = DateTime.Parse("2020-11-27T12:00:00"), DueDate = DateTime.Parse("2020-12-10T14:00:00"), Status = "In Progress", Priority = "Normal", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Arthur Miller", Progress = 40, ParentId = 13 },
            new TaskModel { Id = 2, Text = "Send pictures/brochures of new products.", StartDate = DateTime.Parse("2020-11-27T06:26:40"), DueDate = DateTime.Parse("2020-12-05T14:00:00"), Status = "Open", Priority = "High", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Samantha Bright", Progress = 0, ParentId = 15 },
            new TaskModel { Id = 3, Text = "Follow up and discuss the offer.", StartDate = DateTime.Parse("2020-11-26T11:00:00"), DueDate = DateTime.Parse("2020-12-09T14:00:00"), Status = "Open", Priority = "Low", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Greta Sims", Progress = 0, ParentId = 16 },
            new TaskModel { Id = 4, Text = "Obtain CEO contact information.", StartDate = DateTime.Parse("2020-11-26T05:26:40"), DueDate = DateTime.Parse("2020-12-04T14:00:00"), Status = "In Progress", Priority = "Normal", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Arthur Miller", Progress = 25, ParentId = 14 },
            new TaskModel { Id = 5, Text = "Create requested product comparison report.", StartDate = DateTime.Parse("2020-11-25T10:00:00"), DueDate = DateTime.Parse("2020-12-08T14:00:00"), Status = "In Progress", Priority = "High", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Samantha Bright", Progress = 45, ParentId = 16 },
            new TaskModel { Id = 6, Text = "Generate a quote.", StartDate = DateTime.Parse("2020-11-25T04:26:40"), DueDate = DateTime.Parse("2020-12-03T14:00:00"), Status = "In Progress", Priority = "Low", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Greta Sims", Progress = 65, ParentId = 13 },
            new TaskModel { Id = 8, Text = "Apply discounts and generate a binding offer.", StartDate = DateTime.Parse("2020-11-24T09:00:00"), DueDate = DateTime.Parse("2020-12-07T14:00:00"), Status = "Completed", Priority = "Normal", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Greta Sims", Progress = 100, ParentId = 16 },
            new TaskModel { Id = 9, Text = "Obtain feedback on new equipment.", StartDate = DateTime.Parse("2020-11-24T03:26:40"), DueDate = DateTime.Parse("2020-12-02T14:00:00"), Status = "Completed", Priority = "High", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Robert Reagan", Progress = 100, ParentId = 14 },
            new TaskModel { Id = 10, Text = "Send SWAG to customer.", StartDate = DateTime.Parse("2020-11-23T08:00:00"), DueDate = DateTime.Parse("2020-12-06T14:00:00"), Status = "Completed", Priority = "Low", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Samantha Bright", Progress = 100, ParentId = 15 },
            new TaskModel { Id = 11, Text = "Ask if upgrade is required.", StartDate = DateTime.Parse("2020-11-23T02:26:40"), DueDate = DateTime.Parse("2020-12-01T14:00:00"), Status = "Completed", Priority = "Normal", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Arthur Miller", Progress = 100, ParentId = 14 },
            new TaskModel { Id = 7, Text = "Follow up and address recent feedback.", StartDate = DateTime.Parse("2020-11-26T07:00:00"), DueDate = DateTime.Parse("2020-12-05T14:00:00"), Status = "In Progress", Priority = "High", Owner = "Sammy Hill", Company = "ElectrixMax", Manager = "Robert Reagan", Progress = 85, ParentId = 14 },
        };
    }
}
