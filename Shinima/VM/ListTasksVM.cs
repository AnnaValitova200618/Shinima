using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shinima.DB;
using Shinima.Models;
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
        private Models.Task selectTask;
        private string search = "";
        private Visibility panel = Visibility.Collapsed;
        private Employee selectEmployee;
        private Status selectStatus;
        private Models.Task selectBeforTask;
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
        public Models.Task SelectTask
        {
            get => selectTask;
            set
            {
                selectTask = value;
                if (selectTask != null)
                {
                    Panel = Visibility.Visible;
                    Task = SelectTask;
                    SelectEmployee = SelectTask.IdExecutiveEmployeeNavigation;
                    SelectStatus = SelectTask.IdStatusNavigation;
                    SelectBeforTask = SelectTask.IdPreviousTaskNavigation;
                }
                Signal();
                Signal(nameof(Panel));
                Signal(nameof(Task));
                Signal(nameof(SelectEmployee));
                Signal(nameof(SelectStatus));
                Signal(nameof(SelectBeforTask));
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
        public CustomCommand AddTask { get; set; }
        public CustomCommand Save { get; set; }
        public CustomCommand Del { get; set; }
        public Models.Task Task { get; set; }
        public List<Employee> Employees { get; set; }
        public Employee? SelectEmployee
        {
            get => selectEmployee;
            set
            {
                selectEmployee = value;
                Signal();
            }
        }
        public List<Status> Statuses { get; set; }
        public Status? SelectStatus
        {
            get => selectStatus;
            set
            {
                selectStatus = value;
                Signal();
            }
        }
        public List<Models.Task> BeforTasks { get; set; }
        public Models.Task? SelectBeforTask 
        {
            get => selectBeforTask;
            set
            {
                selectBeforTask = value;
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
            Employees = DBInstance.GetInstance().Employees.ToList();
            Statuses = DBInstance.GetInstance().Statuses.ToList();
            BeforTasks = DBInstance.GetInstance().Tasks.Where(s=>s.DeletedTime == null).ToList();

            AddTask = new CustomCommand(() =>
            {
                Panel = Visibility.Visible;
                
                
                Task = new Models.Task();
                Signal(nameof(Task));
            });
            Save = new CustomCommand(() =>
            {
                
                try
                {
                 
                    if (string.IsNullOrEmpty(Task.Description) ||
                        string.IsNullOrEmpty(Task.FullTitle) ||
                        string.IsNullOrEmpty(Task.ShortTitle))
                    {
                        MessageBox.Show("Необходимо заполнить все данные");
                        return;
                    }

                    Task.IdProject = selectProject.Id;
                    Task.IdPreviousTask = SelectBeforTask?.Id;
                    Task.IdExecutiveEmployee = SelectEmployee?.Id;
                    Task.IdStatus = SelectStatus?.Id;

                    if (Task.Id == 0)
                    {
                        Task.CreatedTime = DateTime.Now;
                        DBInstance.GetInstance().Tasks.Add(Task);

                    }
                    DBInstance.GetInstance().SaveChanges();
                    MessageBox.Show("Сохранение прошло успешно");
                    Tasks = GetTasks(selectProject).ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Что-то пошло не так");
                    return;
                }
                
            });
            Del = new CustomCommand(() =>
            {
                if(SelectTask == null)
                {
                    MessageBox.Show("Необходимо выбрать конктретную задачу");
                    return;
                }
                else
                {
                    SelectTask.DeletedTime = DateTime.Now;
                    DBInstance.GetInstance().SaveChanges();
                    MessageBox.Show("Удаление прошло успешно");
                    Tasks = GetTasks(selectProject).ToList();
                }
            });
        }

        private static IQueryable<Models.Task> GetTasks(Models.Project selectProject)
        {
            return DBInstance.GetInstance().Tasks
                            .Include(s => s.IdExecutiveEmployeeNavigation)
                            .Include(s => s.IdProjectNavigation)
                            .Where(s => s.IdStatus != 1 
                            && s.IdProject == selectProject.Id
                            && s.DeletedTime == null);
        }

    }
}
