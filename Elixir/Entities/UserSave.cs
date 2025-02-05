using System;

namespace Elixir.Entities;

public class UserSave : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

}
