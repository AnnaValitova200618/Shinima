using Shinima.DB;
using Shinima.Models;
using Shinima.Tools;
using Shinima.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Shinima.VM
{
    public class MainVM : BaseVM
    {
        private Page currentPage;
        private List<Project> listProjects;
        private Project selectProject;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }
        public CustomCommand OpenTasks { get; set; }
        public CustomCommand OpenDashboard { get; set; }
        public CustomCommand OpenGant { get; set; }
        public List<Project> ListProjects
        {
            get => listProjects;
            set
            {
                listProjects = value;
                Signal();
            }
        }
        public Project SelectProject 
        {
            get => selectProject;
            set
            {
                selectProject = value;
                OpenLastPage();
                Signal();
            }
        }
        int lastpage;
        public MainVM()
        {
            

            try
            {
                ListProjects = DBInstance.GetInstance().Projects.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("нет");
                return;
            }
            SelectProject = ListProjects.First();
            OpenLastPage();

            foreach (Project project in ListProjects)
            {
                string name = project.FullTitle;
                if (name.IndexOf(' ') > 0)
                {
                    string firstletter = name.Substring(0, 1);
                    string secondletter = name.Substring(name.IndexOf(' ') + 1, 1);
                    project.Name = $"{firstletter}{secondletter}";

                }
                else
                {
                    string firstletter = name.Substring(0, 2);
                    project.Name = $"{firstletter}";

                }
            }

            
            OpenDashboard = new CustomCommand(() =>
            {
                CurrentPage = new Dashboard(SelectProject);
                Properties.Settings.Default.lastpage = 0;
                Properties.Settings.Default.Save();
            });
            OpenTasks = new CustomCommand(() =>
            {

                CurrentPage = new ListTasks(SelectProject);
                Properties.Settings.Default.lastpage = 1;
                Properties.Settings.Default.Save();

            });
            OpenGant = new CustomCommand(() =>
            {
                CurrentPage = new Gant(SelectProject);
                Properties.Settings.Default.lastpage = 2;
                Properties.Settings.Default.Save();
            });
        }

        private void OpenLastPage()
        {
            lastpage = Properties.Settings.Default.lastpage;
            
            switch (lastpage)
            {
                case 0: CurrentPage = new Dashboard(SelectProject); break;
                case 1: CurrentPage = new ListTasks(SelectProject); break;
                case 2: CurrentPage = new Gant(SelectProject); break;
            }
        }
    }
}
