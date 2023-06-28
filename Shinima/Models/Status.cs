using System;
using System.Collections.Generic;

namespace Shinima.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ColorNex { get; set; }

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
