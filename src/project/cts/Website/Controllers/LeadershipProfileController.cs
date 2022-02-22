using CTS.Project.CTS.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Project.CTS.Views.CTS
{
    public class LeadershipProfileController : Controller
    {
        // GET: LeadershipProfile
        public ActionResult GetLeadershipProfileInfo()
        {

            var contxetitem = Sitecore.Context.Item;
            LeadershipProfile leadershipProfile = new LeadershipProfile();
            leadershipProfile.LeaderName= new HtmlString(FieldRenderer.Render(contxetitem, "LeaderName"));
            leadershipProfile.Brief = new HtmlString(FieldRenderer.Render(contxetitem, "Brief"));
            leadershipProfile.Designation = new HtmlString(FieldRenderer.Render(contxetitem, "Designation"));
            leadershipProfile.LeaderImage = new HtmlString(FieldRenderer.Render(contxetitem, "LeaderImage"));

            //leadershipProfile.DetailPageUrl = contxetitem.Fields["ProfileDetail"].Value;
            LinkField linkField = contxetitem.Fields["DetailPageUrl"];
            var targetItem = linkField.TargetItem;
            leadershipProfile.DetailPageUrl = LinkManager.GetItemUrl(targetItem);
            return View("/Views/Cts/LeadershipProfile/LeaderProfile.cshtml");
        }
        public ActionResult GetLeadershipArticle()
        {
            List<Article> articleList = new List<Article>();
            var contextItem = Sitecore.Context.Item;
            MultilistField multilistField = contextItem.Fields["Articles"];
            articleList = multilistField.GetItems()
                            .Select(x => new Article
                            {
                                ArticleTitle = new HtmlString(FieldRenderer.Render(x, "Title")),
                                ArticleBrief = new HtmlString(FieldRenderer.Render(x, "Brief")),
                                ArticleUrl = LinkManager.GetItemUrl(x)
                            }).ToList();
            return View("/Views/Cts/LeadershipProfile/Articles.cshtml", articleList);
            
        }
    }
}