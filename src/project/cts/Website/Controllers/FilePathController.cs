using CTS.Project.CTS.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CTS.Project.CTS.Controllers
{
    public class FilePathController : Controller
    {
        // GET: FilePath
        public ActionResult GetBreadCrumb()
        {
            var contextitem = Sitecore.Context.Item;

            //contextitem.Axes.

            var homeitem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);

            NavigationItem currentItemNav = new NavigationItem
            {
                NavTitle = contextitem.DisplayName,
                NavURL = LinkManager.GetItemUrl(contextitem)
            };
            var navitemlist = contextitem.Axes.GetAncestors()
               // .Where(x=>x.Template.BaseTemplates.Contains())
               
                .Where(x=>x.Axes.IsDescendantOf(homeitem))
                .Where(x => checkfornavigableoption(x))
                .Select(x => new NavigationItem
                {
                    NavTitle = x.DisplayName,
                    NavURL = LinkManager.GetItemUrl(x)
                })
           .ToList()
           .Concat(new List<NavigationItem> { currentItemNav });
            return View("/Views/CTS/Common/BreadCrumb.cshtml",navitemlist);
        }

        private bool checkfornavigableoption(Item item)
        {
            if (item.Fields["isNavigable"] != null)
            {
                CheckboxField checkbox = item.Fields["isNavigable"];
                return checkbox.Checked;
            }
            return false;
        }
    }
}