using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Dtos.Request;
using WebApi.Dtos.Requests;
using WebApi.Dtos.Respons;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/account")]
    public class AccountController: ApiControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            ITokenService jwtService,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized(new ApiResponse(401));

            return Ok(new AuthResultDto
            {
                Email = user.Email!,
                Name = user.Name,
                Token = _jwtService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return BadRequest(new ValidationResponse(new[]
                {
                    "email is already taken"
                }));

            var user = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email, UserName = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if(!result.Succeeded)
                return BadRequest(new ValidationResponse(
                    result.Errors.Select(e => e.Description)));

            return Ok(new AuthResultDto {
                Name = user.Name,
                Email = user.Email,
                Token = _jwtService.CreateToken(user)
            });
        }

        [HttpGet("isEmailUnique")]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email) == null);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<UserDto>(user));
        }   
        
        [HttpPut("address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(UpdateAddressDto dto)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound(new ApiResponse(404));

            user.Address = _mapper.Map<Address>(dto);
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
