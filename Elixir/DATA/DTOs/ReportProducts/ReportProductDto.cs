using System;
using Elixir.DATA.DTOs.Product;
using Elixir.Entities;

namespace Elixir.DATA.DTOs.ReportProducts;

public class ReportProductDto : BaseDto<Guid>
{
    public ProductDto? Product { get; set; }
    public AppUser? User { get; set; }
    public AppUser? Admin { get; set; }
    public string? Status { get; set; }
    public string? Reason { get; set; }
    public string? AdminNote { get; set; }

}
