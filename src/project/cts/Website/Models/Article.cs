using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Project.CTS.Models
{
    public class Article
    {
        public HtmlString ArticleTitle { get; set; }
        public HtmlString ArticleBrief { get; set; }
        public string ArticleUrl { get; set; }
    }
}