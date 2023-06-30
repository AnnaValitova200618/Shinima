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

    public int? IdExecutiveEmployee { get; set; }

    public int? IdStatus { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? Deadline { get; set; }

    public DateTime? StartActualTime { get; set; }

    public DateTime? FinishActualTime { get; set; }

    public int? IdPreviousTask { get; set; }

    public virtual ICollection<HistoryUpdateStatus> HistoryUpdateStatuses { get; } = new List<HistoryUpdateStatus>();

    public virtual Employee? IdExecutiveEmployeeNavigation { get; set; }

    public virtual Task? IdPreviousTaskNavigation { get; set; }

    public virtual Project IdProjectNavigation { get; set; } = null!;

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<Task> InverseIdPreviousTaskNavigation { get; } = new List<Task>();

    public virtual ICollection<Сomment> Сomments { get; } = new List<Сomment>();
}
