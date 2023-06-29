using System;
using System.Collections.Generic;

namespace Shinima.Models;

public partial class Task
{
    public int Id { get; set; }

    public int IdProject { get; set; }

    public string FullTitle { get; set; } = null!;

    public string ShortTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdExecutiveEmployee { get; set; }

    public int IdStatus { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public DateTime? UpdateTime { get; set; } = DateTime.Now;

    public DateTime? DeletedTime { get; set; } = DateTime.Now;

    public DateTime Deadline { get; set; } = DateTime.Now;

    public DateTime? StartActualTime { get; set; } = DateTime.Now;

    public DateTime? FinishActualTime { get; set; } = DateTime.Now;

    public int? IdPreviousTask { get; set; }

    public virtual ICollection<HistoryUpdateStatus> HistoryUpdateStatuses { get; } = new List<HistoryUpdateStatus>();

    public virtual Employee IdExecutiveEmployeeNavigation { get; set; } = null!;

    public virtual Task? IdPreviousTaskNavigation { get; set; }

    public virtual Project IdProjectNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;

    public virtual ICollection<Task> InverseIdPreviousTaskNavigation { get; } = new List<Task>();

    public virtual ICollection<Сomment> Сomments { get; } = new List<Сomment>();
}
