using api.Data;
using api.Dtos.AccountDtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.DevTools.V132.FedCm;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountsController : Controller
{
    private readonly AppDbContext _dbContext;

    private readonly UserManager<AppUser> _userManager;

    private readonly SignInManager<AppUser> _signInManager;

    private readonly ITokenService _tokenService;
    public AccountsController
    (
        AppDbContext appDbContext,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService
    )
    {
        _dbContext = appDbContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] RegisterDto registerDto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();

            AppUser NewUserAccount = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var createdUser = await _userManager.CreateAsync(NewUserAccount, registerDto.Password);
            if(!createdUser.Succeeded) return StatusCode(500, "Internal Server Error at trying to create account");

            var roleResult = await _userManager.AddToRoleAsync(NewUserAccount, "User");
            if(!roleResult.Succeeded) return StatusCode(500, "Internal Server Error at trying to create account");


            var userToken = _tokenService.CreateToken(NewUserAccount);
            return Ok(
                new NewUserResponseDto
                (
                    NewUserAccount.UserName,
                    NewUserAccount.Email,
                    userToken
                )
            );
        }catch(Exception ex)
        {
            Console.WriteLine("An Exception has occurred:");
            Console.WriteLine($"Error: {ex}");
            return StatusCode(500, "Internal Server Error has occurred");
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest();

            var UserAccount = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == loginDto.Email);
            if(UserAccount == null) return Unauthorized("Invalid Email");

            var PasswordResult = await _signInManager.CheckPasswordSignInAsync(UserAccount, loginDto.Password, false);
            if(!PasswordResult.Succeeded) return Unauthorized("User email not found and/or password incorrect");

            var UserToken = _tokenService.CreateToken(UserAccount);
            
            return Ok(
                new NewUserResponseDto
                (
                    UserAccount.UserName,
                    UserAccount.Email,
                    UserToken
                )
            );
        }catch(Exception ex)
        {
            Console.WriteLine("An Exception has occurred:");
            Console.WriteLine($"Error: {ex}");
            return StatusCode(500, "Internal Server Error has occurred");
        }
    }

   


}
