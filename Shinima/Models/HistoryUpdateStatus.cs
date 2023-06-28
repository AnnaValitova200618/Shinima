using System;
using System.Collections.Generic;

namespace Shinima.Models;

public partial class HistoryUpdateStatus
{
    public int Id { get; set; }

    public int IdTask { get; set; }

    public int OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public DateTime TimeUpdate { get; set; }

    public virtual Task IdTaskNavigation { get; set; } = null!;
}
