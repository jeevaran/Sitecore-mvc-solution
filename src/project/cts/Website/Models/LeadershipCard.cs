using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Project.CTS.Models
{
    public class LeadershipCard
    {
        public string LeaderName { get; set; }
        public string LeaderProfile { get; set; }
        public string LeaderProfileUrl { get; set; }

        public DateTime LeaderJoiningDate { get; set; }
    }
}