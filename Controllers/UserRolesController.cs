using Identity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Policy = "Admin")]
public class UserRolesController(UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> UserManager = userManager;

    [HttpPost]
    public async Task<IActionResult> Create(UserRolesCreate userRolesCreate)
    {
        IdentityUser? user = await UserManager.FindByEmailAsync(userRolesCreate.Email);
        if (user is null)
            return BadRequest("User not found");

        await UserManager.AddToRoleAsync(user, userRolesCreate.RoleName);

        return Created();
    }
}
