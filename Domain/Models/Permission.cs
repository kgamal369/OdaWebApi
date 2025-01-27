using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Permission
{
    public int Permissionid { get; set; }

    public string? Entityname { get; set; }

    public string? Action { get; set; }

    public int? Roleid { get; set; }

    public DateTime? Createdatetime { get; set; }

    public DateTime? Lastmodifieddatetime { get; set; }

    public virtual Role? Role { get; set; }
}
