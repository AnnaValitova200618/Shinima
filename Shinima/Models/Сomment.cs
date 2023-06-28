using System;
using System.Collections.Generic;

namespace Shinima.Models;

public partial class Сomment
{
    public int Id { get; set; }

    public int IdEmployee { get; set; }

    public int IdTask { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Time { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual Task IdTaskNavigation { get; set; } = null!;
}
