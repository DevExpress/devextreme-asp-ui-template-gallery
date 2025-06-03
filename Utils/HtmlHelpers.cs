using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;
using DevExtreme.AspNet.Mvc.Factories;
using DevExtremeVSTemplateMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevExtremeVSTemplateMVC.Utils
{
    public static class HtmlHelpers
    {
        public class SuperVisorModel
        {
            public string name { get; set; }
            public string image { get; set; }
        }

        public static FormBuilder<EmployeeProfile> BasicInfoCard(this IHtmlHelper Html, EmployeeProfile profile)
        {
            FormBuilder<EmployeeProfile> form = Html.DevExtreme().Form<EmployeeProfile>()
                .FormData(profile)
                .ColCount(4)
                .ShowColonAfterLabel(true)
                .LabelLocation(FormLabelLocation.Top)
                .LabelMode(FormLabelMode.Outside)
                .OnOptionChanged("formDataChanged")
                .Items(items =>
                {
                    items.AddSimpleFor(m => m.FirstName)
                        .ColSpan(2)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .ValueChangeEvent("input"));
                    items.AddSimpleFor(m => m.LastName)
                        .ColSpan(2)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .ValueChangeEvent("input"));
                    items.AddSimpleFor(m => m.Department)
                        .ColSpan(1)
                        .Editor(editor => editor.SelectBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .Items(new List<string> { "UI/UX", "Backend Developers" }));
                    items.AddSimpleFor(m => m.Position)
                        .ColSpan(1)
                        .Editor(editor => editor.SelectBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .Items(new List<string> { "Designer", "Developer", "Technical Writer" }));
                    items.AddSimpleFor(m => m.HiredDate)
                        .ColSpan(1)
                        .Editor(editor => editor.DateBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .Max(DateTime.Now));
                    items.AddSimpleFor(m => m.BirthDate)
                        .ColSpan(1)
                        .Editor(editor => editor.DateBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .Max(DateTime.Now));
                });

            return form;
        }


        public static FormBuilder<EmployeeProfile> ContactsCard(this IHtmlHelper Html, EmployeeProfile profile)
        {
            FormBuilder<EmployeeProfile> form = Html.DevExtreme().Form<EmployeeProfile>()
                .FormData(profile)
                .ColCount(2)
                .ShowColonAfterLabel(true)
                .LabelLocation(FormLabelLocation.Top)
                .LabelMode(FormLabelMode.Outside)
                .OnOptionChanged("formDataChanged")
                .Items(items =>
                {
                    items.AddSimpleFor(m => m.Phone)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled)
                            .Mask("+1(000)000-0000"));
                    items.AddSimpleFor(m => m.Email)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.DomainUsername)
                        .ColSpan(2)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.Status)
                        .ColSpan(2)
                        .Editor(editor => editor.SelectBox()
                            .Items(new List<string> { "Salaried", "Commission", "Terminated" })
                            .StylingMode(EditorStylingMode.Filled)
                            .LabelMode(EditorLabelMode.Hidden)
                            .Width("100%")
                            .ItemTemplate(new TemplateName("StatusItemTemplate"))
                            .FieldTemplate(new TemplateName("StatusFieldTemplate")));
                    items.AddSimpleFor(m => m.Supervisor)
                        .ColSpan(2)
                        .Editor(editor => editor.SelectBox()
                            .ElementAttr(new
                            {
                                @class = "pictured-item-select-box",
                            })
                            .ValueExpr("Name")
                            .StylingMode(EditorStylingMode.Filled)
                            .LabelMode(EditorLabelMode.Hidden)
                            .Width("100%")
                            .DataSource(d => d.Mvc().Controller("Supervisor").LoadAction("Get").Key("Id"))
                            .DropDownOptions(options => 
                                options.WrapperAttr(new
                                {
                                    @class= "pictured-item-select-box-dropdown"
                                })
                            )
                            .FieldTemplate(new TemplateName("SupervisorFieldTemplate"))
                            .ItemTemplate(new TemplateName("SupervisorItemTemplate"))
                        );
                });

            return form;
        }

        public static FormBuilder<EmployeeProfile> AddressCard(this IHtmlHelper Html, EmployeeProfile profile)
        {
            FormBuilder<EmployeeProfile> form = Html.DevExtreme().Form<EmployeeProfile>()
                .FormData(profile)
                .ColCount(2)
                .ShowColonAfterLabel(true)
                .LabelLocation(FormLabelLocation.Top)
                .LabelMode(FormLabelMode.Outside)
                .OnOptionChanged("formDataChanged")
                .Items(items =>
                {
                    items.AddSimpleFor(m => m.Country)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.City)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.State)
                        .ColSpan(2)
                        .Label(label=>label.Text("State/province/area"))
                        .Editor(editor => editor.TextBox()
                            .Label("State/province/area")
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.Address)
                        .ColSpan(2)
                        .Editor(editor => editor.TextBox()
                            .StylingMode(EditorStylingMode.Filled));
                    items.AddSimpleFor(m => m.ZipCode)
                        .ColSpan(2)
                        .Editor(editor => editor.NumberBox()
                            .StylingMode(EditorStylingMode.Filled));
                });

            return form;
        }

        public static string FormatPhone(this IHtmlHelper Html, string value)
        {
            if (value == null) return "";
            return System.Text.RegularExpressions.Regex.Replace(value, @"(\d{3})(\d{3})(\d{4})", "+1($1)$2-$3");
        }
    }
}
