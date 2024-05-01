using Identity.Abstrations;
using Identity.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(
    ITokenService tokenService,
    UserManager<IdentityUser> userManager,
    IConfiguration configuration) : ControllerBase
{
    private readonly ITokenService TokenService = tokenService;
    private readonly UserManager<IdentityUser> UserManager = userManager;
    private readonly IConfiguration Configuration = configuration;

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthLogin authLogin)
    {
        IdentityUser? user = await UserManager.FindByEmailAsync(authLogin.Email);

        if (user is null)
            return NotFound("User not found");

        if (!await UserManager.CheckPasswordAsync(user, authLogin.Password))
            return BadRequest("Invalid credential");

        IList<string> userRoles = await UserManager.GetRolesAsync(user);

        List<Claim> authClaims =
        [
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        foreach (string userRole in userRoles)
            authClaims.Add(new(ClaimTypes.Role, userRole));

        JwtSecurityToken token = TokenService.GenerateAccessToken(authClaims, Configuration);
        string refreshToken = TokenService.GenerateRefreshToken();
        int.TryParse(Configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

        await UserManager.UpdateAsync(user);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        });
    }
}
