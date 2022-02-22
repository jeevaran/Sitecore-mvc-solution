using CTS.Project.CTS.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Project.CTS.Controllers
{
    public class CTSProfileController : Controller
    {
        // GET: CTSProfile
        public ActionResult GetProfileinfo()
        {

            var contxetitem = Sitecore.Context.Item;
            LinkField linkField = contxetitem.Fields["LeadershipProfile"];
            DateField dateField = contxetitem.Fields["DateofJoin"];
            CTSProfile cTSProfile = new CTSProfile() {

                LastName = new HtmlString(FieldRenderer.Render(contxetitem, "Lastname")),
            FirstName = new HtmlString(FieldRenderer.Render(contxetitem, "Firstname")),
            EmailId = new HtmlString(FieldRenderer.Render(contxetitem, "Email")),
                //DetailPageUrl = LinkManager.GetItemUrl(liked.),
                Dateofjoing = new HtmlString(FieldRenderer.Render(contxetitem, "DateofJoin")),
            JoiningDate=dateField.DateTime,

            };

            return View("/Views/CTS/CTSProfile/CTSProfile.cshtml", cTSProfile);
        }

        public ActionResult GetLeadershipArticle()
        {
            return View("/Views/CTS/CTSProfile/CTSProfile.cshtml");
        }
    }
}