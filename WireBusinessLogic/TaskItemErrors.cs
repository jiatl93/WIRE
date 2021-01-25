using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireConfig;

namespace WireBusinessLogic
{
    public class TaskItemErrors
    {
        public string UserName { get; set; }

        public int WorkItemID { get; set; }

        public string WorkItemTitle { get; set; }

        public string WorkItemURL { get; set; }

        public bool ItemPresent { get; set; }

        public List<TaskItemConfig> ErrorItems { get; set; }

        public TaskItemErrors(string userName, int workItemId, string workItemTitle,
            string workItemWorkItemUrl)
        {
            UserName = userName;
            WorkItemID = workItemId;
            WorkItemTitle = workItemTitle;
            WorkItemURL = workItemWorkItemUrl;
            ErrorItems = new List<TaskItemConfig>();
        }
    }
}
