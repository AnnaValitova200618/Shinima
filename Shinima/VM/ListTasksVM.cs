using Microsoft.EntityFrameworkCore;
using Shinima.DB;
using Shinima.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shinima.VM
{
    public class ListTasksVM : BaseVM
    {
        private List<Models.Task> tasks;
        private Task selectTask;

        public List<Models.Task> Tasks
        {
            get => tasks;
            set
            {
                tasks = value;
                Signal();
            }
        }
        public Task SelectTask 
        {
            get => selectTask;
            set
            {
                selectTask = value;
                Signal();
            }
        }
        public ListTasksVM()
        {
            Tasks = DBInstance.GetInstance().Tasks.Include(s=>s.IdExecutiveEmployeeNavigation).Where(s => s.IdStatus != 1).ToList();
        }
    }
}
