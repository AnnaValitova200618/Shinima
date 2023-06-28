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
                Signal();
            }
        }
        public string Name { get; set; }
        public MainVM()
        {
            CurrentPage = new Dashboard();
            try
            {
                ListProjects = DBInstance.GetInstance().Projects.ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show("нет");
                return;
            }


            foreach (Project project in ListProjects)
            {
                string name = project.FullTitle;
                if (name.IndexOf(' ') > 0)
                {
                    string firstletter = name.Substring(0);
                    string secondletter = name.Substring(name.IndexOf(' ') + 1);
                    Name = $"{firstletter}{secondletter}";
                }
            }

            OpenDashboard = new CustomCommand(() =>
            {
                CurrentPage = new Dashboard();
            });
            OpenTasks = new CustomCommand(() =>
            {
                CurrentPage = new ListTasks();
            });
            OpenGant = new CustomCommand(() =>
            {
                CurrentPage = new Gant();
            });
        }
    }
}
