using Directory.Constants;
using Directory.Interfaces;
using Directory.Models.Dtos;
using Directory.Models.Entities;
using Directory.Models.Enums;
using Directory.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Directory.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResponseDto<bool>> RegisterAsync(Register register)
        {
            var isExistsUser = await _userManager.FindByEmailAsync(register.Email);
            if (isExistsUser != null)
            {
                return new ResponseDto<bool>()
                {
                    IsSucceed = false,
                    Message = "Email already exist",
                    Status = ResultStatus.BadRequest,
                    Data = false,
                };
            };

            var identityUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = register.UserName,
                Email = register.Email,
                Role = UserRoles.USER,
                FirstName = register.FirstName,
                LastName = register.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(identityUser, register.Password);
            if (createUserResult.Succeeded)
            {
                var errorString = "User creation failed beacouse: ";
                createUserResult.Errors.ToList().ForEach((error) =>
                {
                    errorString += " # " + error.Description;
                });
                return new ResponseDto<bool>()
                {
                    IsSucceed = false,
                    Message = errorString,
                    Status = ResultStatus.BadRequest,
                    Data = false,
                };
            };

            return new ResponseDto<bool>()
            {
                IsSucceed = true,
                Message = "User Created Successfully",
                Status = ResultStatus.OK,
                Data = true,
            };
        }
    }
}