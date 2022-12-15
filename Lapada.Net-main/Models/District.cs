using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class District
{
    public string DistrictId { get; set; } = null!;

    public string? DistrictName { get; set; }

    public string? DistrictType { get; set; }

    public string? CityId { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual City? City { get; set; }
}
