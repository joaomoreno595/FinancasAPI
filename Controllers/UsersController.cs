using Identity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(Policy = "Admin")]
public class UsersController(UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> UserManager = userManager;

    [HttpPost]
    public async Task<IActionResult> Create(UserCreate userCreate)
    {
        IdentityUser? userExists = await UserManager.FindByEmailAsync(userCreate.Email);

        if (userExists is not null)
            return BadRequest("User email already in use");

        IdentityUser user = new()
        {
            UserName = userCreate.UserName,
            Email = userCreate.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        await UserManager.CreateAsync(user, userCreate.Password);
        await UserManager.AddToRoleAsync(user, "User");

        return Created();
    }
}
