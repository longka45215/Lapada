using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? ProductAmount { get; set; }

    public double? OrderTotalPrice { get; set; }

    public int? OrderStatus { get; set; }

    public string? UserId { get; set; }

    public int? AddressId { get; set; }

    public int? ProductId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
