using Microsoft.AspNetCore.Identity;

namespace OpLawyers.Models;

public class Usuario : IdentityUser
{
    public Cliente? Cliente { get; set; }
    public Administrador? Administrador { get; set; }
}
