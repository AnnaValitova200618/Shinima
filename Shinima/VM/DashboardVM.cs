using Shinima.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Shinima.VM
{
    internal class DashboardVM
    {
        public List<IData> Data { get; set; } = new List<IData>();

        private Project selectProject;

        public DashboardVM(Project selectProject)
        {
            this.selectProject = selectProject;
            if (selectProject == null)
                return;

            // блоки
            Data.Add(new InfoTaskNotOver(selectProject));
            Data.Add(new InfoTaskDeadLineOver(selectProject));

            Data.Add(new InfoTaskDeadLineOver(selectProject));
            Data.Add(new InfoTaskDeadLineOver(selectProject));
        }
    }

    // классы блоков, для каждого свой template
    internal class InfoTaskDeadLineOver : IData
    {
        public int Count { get; set; }
        private Project selectProject;

        public InfoTaskDeadLineOver(Project selectProject)
        {
            this.selectProject = selectProject;
            Count = selectProject.Tasks.Where(s => s.Deadline < DateTime.Now).Count();
        }
    }

    internal class InfoTaskNotOver : IData
    {
        public int Count { get; set; }

        private Project selectProject;

        public InfoTaskNotOver(Project selectProject)
        {
            this.selectProject = selectProject;

            Count = selectProject.Tasks.Where(s => s.FinishActualTime == null).Count();
        }
    }

    public interface IData
    {
    }


}