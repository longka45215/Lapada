using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? ProductAmount { get; set; }

    public string? UserId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
