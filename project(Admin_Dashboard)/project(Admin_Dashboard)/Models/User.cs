using System;
using System.Collections.Generic;

namespace project_Admin_Dashboard_.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public Role? Role { get; set; } = null!;
}
