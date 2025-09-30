using System;
using System.Collections.Generic;

namespace NWRestfulAPI.Models;

public partial class ProductSalesFor1997
{
    public long Rowid { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? ProductSales { get; set; }
}
