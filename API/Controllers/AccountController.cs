using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using CORE.Entities.Identity;
using CORE.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;     
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindUserByEmailFromClaims(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.Displayname,

            };

        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) !=null;

        }

        [Authorize]

        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

              //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

              var user = await _userManager.FindUserByClaimsWithAddress(User);


            return _mapper.Map<Address, AddressDto>(user.Address);

        }


        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdatUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsWithAddress(HttpContext.User);

            user.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest("Could not update user");
        }
        
        

       


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto{
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.Displayname
            };
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if(CheckEmailExists(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult (new ApiValidationErrorResponse{Errors = new [] {"Email address already in use"}});
            }

            var user = new AppUser{
                Displayname = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return  BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.Displayname,
                Token = _tokenService.CreateToken(user),
                Email = user.Email

            };
        }
        
        
    }
}