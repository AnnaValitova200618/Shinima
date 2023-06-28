using Microsoft.EntityFrameworkCore;
using Shinima.DB;
using Shinima.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shinima.VM
{
    public class ListTasksVM : BaseVM
    {
        private List<Models.Task> tasks;
        private Task selectTask;
        private string search = "";
        private Visibility panel = Visibility.Collapsed;
        private readonly Models.Project selectProject;

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
                if(selectTask != null)
                {
                    Panel = Visibility.Visible;
                }
                Signal();
                Signal(nameof(Panel));
                
            }
        }
        public string Search
        {
            get => search;
            set
            {
                search = value;
                DoSearch();
            }
        }
        public Visibility Panel 
        {
            get => panel;
            set
            {
                panel = value;
                Signal();
            }
        }

        private void DoSearch()
        {
            var taskList = GetTasks(selectProject).Where(s => s.FullTitle.Contains(Search) ||
                                                              s.Description.Contains(Search)).ToList();
            Tasks = taskList;

        }

        public ListTasksVM(Models.Project selectProject)
        {
            this.selectProject = selectProject;
            Tasks = GetTasks(selectProject).ToList();
            

        }

        private static IQueryable<Models.Task> GetTasks(Models.Project selectProject)
        {
            return DBInstance.GetInstance().Tasks
                            .Include(s => s.IdExecutiveEmployeeNavigation)
                            .Include(s => s.IdProjectNavigation)
                            .Where(s => s.IdStatus != 1 && s.IdProject == selectProject.Id);
        }

    }
}
