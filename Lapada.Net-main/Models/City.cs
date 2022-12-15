using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class City
{
    public string CityId { get; set; } = null!;

    public string? CityName { get; set; }

    public string? CityType { get; set; }

    public string? CitySlug { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();

    public virtual ICollection<District> Districts { get; } = new List<District>();
}
