using System;
using System.Collections.Generic;

namespace NWRestfulAPI.Models;

public partial class EmployeeTerritory
{
    public int EmployeeId { get; set; }

    public string TerritoryId { get; set; } = null!;
}
