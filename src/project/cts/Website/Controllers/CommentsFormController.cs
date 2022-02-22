using CTS.Project.CTS.Models;
using Sitecore.Data;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CTS.Project.CTS.Controllers
{
    public class CommentsFormController : Controller
    {
        // GET: CommentsForm - test
        [HttpGet]
        public ActionResult CommentsFormAction()
        {
            Comments comments = new Comments();
            return View("/Views/CTS/LeadershipProfile/CommentsFormAction.cshtml", comments);
        }
        [HttpPost]
        public ActionResult CommentsFormAction(Comments comment)
        {
            //TemplateID templateID = new TemplateID(new ID());

            //var parentitem = Sitecore.Context.Item;
            //var commentsitem=parentitem

            //Create a new comment item as child item for the article item
            //template
            TemplateID templateID = new TemplateID(new ID("{B3D5055C-AEC0-4762-A437-F33DD0209CE0}"));
            //parent item
            var parentItem = Sitecore.Context.Item;
            var masterDB = Sitecore.Configuration.Factory.GetDatabase("master");
            var webDb =Sitecore.Configuration.Factory.GetDatabase("web");
            var parentitemFromMasterDb = masterDB.GetItem(parentItem.ID);
            //create item
            using (new SecurityDisabler())
            {
               // var commentsItem = parentItem.Add(comment.Name, templateID);
               var commentsItem = parentitemFromMasterDb.Add(comment.Name, templateID);
                //update the fields 
                commentsItem.Editing.BeginEdit();
                commentsItem["Comments"] = comment.Comment;
                commentsItem["Name"] = comment.Name;
                commentsItem["EmailId"] = comment.EmailId;
                commentsItem.Editing.EndEdit();

                PublishOptions publishOptions = new PublishOptions(masterDB, webDb, PublishMode.SingleItem, Sitecore.Context.Language, DateTime.Now);
                Publisher publisher = new Publisher(publishOptions);
                publisher.Options.RootItem = commentsItem;
                publisher.Options.Deep = true;
                publisher.Options.Mode = PublishMode.SingleItem;
                publisher.Publish();
            }

           
            return View("/Views/CTS/LeadershipProfile/Thanksyou.cshtml");
        }

    }
}