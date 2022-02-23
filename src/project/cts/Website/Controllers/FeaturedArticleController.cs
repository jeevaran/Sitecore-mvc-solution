using CTS.Project.CTS.Models;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Project.CTS.Controllers
{
    public class FeaturedArticleController : Controller
    {
        // GET: FeaturedArticle
        public ActionResult GetFeatureArticle()
        {
            var dataSourceItem = RenderingContext.Current.Rendering.Item;
            Article article = new Article
            {
                ArticleTitle = new HtmlString(FieldRenderer.Render(dataSourceItem, "Title")),
                ArticleBrief = new HtmlString(FieldRenderer.Render(dataSourceItem, "Brief")),
                ArticleUrl = LinkManager.GetItemUrl(dataSourceItem)
            };
            return View("/Views/CTS/Common/FeatureArticle.cshtml", article);
        }
    }
}