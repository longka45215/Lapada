using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? AddressDetail { get; set; }

    public string? UserPassword { get; set; }

    public string? CustomerName { get; set; }

    public string? CityId { get; set; }

    public string? DistrictId { get; set; }

    public virtual City? City { get; set; }

    public virtual District? District { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
