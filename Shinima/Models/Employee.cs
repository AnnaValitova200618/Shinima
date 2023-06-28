using System;
using System.Collections.Generic;

namespace Shinima.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Lastname { get; set; } = null!;

    public virtual ICollection<Project> ProjectIdCreatorEmployeeNavigations { get; } = new List<Project>();

    public virtual ICollection<Project> ProjectIdResponsibleEmployees { get; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();

    public virtual ICollection<Сomment> Сomments { get; } = new List<Сomment>();
}
