using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using devextreme_asp_ui_template_gallery.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace devextreme_asp_ui_template_gallery.Controllers {

    [Route("api/[controller]")]
    public class SampleDataController : Controller {

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions) {
            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

    }
}