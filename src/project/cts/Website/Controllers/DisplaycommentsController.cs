using CTS.Project.CTS.Models;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Project.CTS.Controllers
{
    public class DisplaycommentsController : Controller
    {
        // GET: Displaycomments
        public ActionResult Displaycomments()
        {
            var contextItem = Sitecore.Context.Item;

            

            var commentList = contextItem.GetChildren()
                                        .Where(x => x.TemplateName == "Comment")
                                       
                                        .Select(x => new Comments
                                        {
                                            Comment = x.Fields["Comment"].Value,
                                            EmailId = x.Fields["EmailID"].Value,
                                            Name = x.Fields["Name"].Value,
                                        }).ToList();
            return View("/Views/CTS/LeadershipProfile/DisplayComments.cshtml", commentList);
        }


    }
}