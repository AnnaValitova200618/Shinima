using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shinima.Models;

public partial class Project
{
    public int Id { get; set; }
    
    public string FullTitle { get; set; } = null!;

    public string ShortTitle { get; set; } = null!;

    public byte[]? Icon { get; set; } 

    public DateTime CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime StartScheduleDate { get; set; }

    public DateTime? FinishScheduleDate { get; set; }

    public string Description { get; set; } = null!;

    public int IdCreatorEmployee { get; set; }

    public int IdResponsibleEmployeeId { get; set; }

    public virtual Employee IdCreatorEmployeeNavigation { get; set; } = null!;

    public virtual Employee IdResponsibleEmployee { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();

    [NotMapped]
    public string? Name { get; set; }
}
