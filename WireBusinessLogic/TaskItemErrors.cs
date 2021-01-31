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

        public List<Tuple<TaskItemConfig, bool>> ErrorItems { get; }

        public TaskItemErrors(string userName, int workItemId, string workItemTitle,
            string workItemWorkItemUrl)
        {
            UserName = userName;
            WorkItemID = workItemId;
            WorkItemTitle = workItemTitle;
            WorkItemURL = workItemWorkItemUrl;
            ErrorItems = new List<Tuple<TaskItemConfig, bool>>();
        }

        public void AddErrorItem(TaskItemConfig errorItem, bool hasValueForField)
        {
            ErrorItems.Add(new Tuple<TaskItemConfig, bool>(errorItem, hasValueForField));
        }

    }
}
