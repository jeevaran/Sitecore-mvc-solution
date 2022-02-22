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
    public class StartupJoinersController : Controller
    {
        // GET: StartupJoiners
        public ActionResult GetListOfStartUpJoiners()
        {
            var contextItem = Sitecore.Context.Item;

            var startupjoinersSettingItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{6E78D57C-C634-4527-85AA-F3BE2AC79278}"));

            var startUpJoinersList = contextItem.GetChildren()
                                        .Where(x => x.TemplateName == "CTSProfileInfo")
                                        .Where(x => CheckJoinerForStartUp(x))
                                        .Select(x => new LeadershipCard
                                        {
                                            LeaderName = x.Fields["Firstname"].Value,
                                            LeaderProfile = x.Fields["Email"].Value,
                                            LeaderProfileUrl = LinkManager.GetItemUrl(x),
                                        }).ToList();
            return View("/Views/Cts/Listing/StartUpJoiners.cshtml", startUpJoinersList);
        }

        private bool CheckJoinerForStartUp(Item joinerItem)
        {
            var startupjoinersSettingItem = Sitecore.Context.Database.GetItem(new Sitecore.Data.ID("{6E78D57C-C634-4527-85AA-F3BE2AC79278}"));
            DateField startDate = startupjoinersSettingItem.Fields["StartDate"];
            DateField endDate = startupjoinersSettingItem.Fields["EndDate"];


            LinkField profileField = joinerItem.Fields["ProfileDetail"];
            if (profileField.IsInternal)
            {
                var profileItem = profileField.TargetItem;
                if (profileItem.TemplateName == "LeaderProfile")
                {
                    DateField profileJoiningDate = profileItem.Fields["DateofJoin"];
                    if ((profileJoiningDate.DateTime > startDate.DateTime)
                        && (profileJoiningDate.DateTime < endDate.DateTime))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
           
        }
    }
}