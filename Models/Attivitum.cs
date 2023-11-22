using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Attivitum
{
    public int Id { get; set; }

    public DateTime? Data { get; set; }

    public string? TipoAttivita { get; set; }

    public int? Ore { get; set; }

    public string Matricola { get; set; } = null!;

    public virtual Dipendenti? MatricolaNavigation { get; set; } = null!;
}
