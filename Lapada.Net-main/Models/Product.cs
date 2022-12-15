using System;
using System.Collections.Generic;

namespace WEB_Shop_PRN.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public int? ProductNumber { get; set; }

    public double? ProductPrice { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
