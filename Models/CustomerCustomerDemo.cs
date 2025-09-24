using System;
using System.Collections.Generic;

namespace NWRestfulAPI.Models;

public partial class CustomerCustomerDemo
{
    public string CustomerId { get; set; } = null!;

    public string CustomerTypeId { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
