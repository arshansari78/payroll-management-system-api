using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payrollmanagementsystem.Models;
using PayrollManagementSystem.Services;
using PayrollManagementSystem.ViewModels;
using PayrollManagementSystem.ViewModels.Common;
using PayrollManagementSystem.ViewModels.Params;
using PayrollManagementSystem.ViewModels.Result;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollManagementSystem.Repositories
{
    public class ApplicationUserRepository : IApplicationUser
    {
       

    public readonly JwtConfigVm jwtConfig;
    public readonly UserManager<ApplicationUser> userManager;
    public readonly ApplicationDbContext context;
    public ApplicationUserRepository(IOptions<JwtConfigVm> options,UserManager<ApplicationUser> _userManager, ApplicationDbContext _context)
    {
        jwtConfig = options.Value;
        userManager = _userManager;
        context = _context;
    }

    
        public async Task<ApiResultVm> Add(ApplicationUserVm model)
        {
            {
                ApiResultVm result = new ApiResultVm();
                try
                {
                    var hashed = new PasswordHasher<ApplicationUser>();
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        NormalizedUserName = model.UserName,
                        Email = model.Email,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        PasswordHash = hashed.HashPassword(null, model.Password)
                    };
                    var check = await userManager.CreateAsync(user);
                    if (check.Succeeded) result = new ApiResultVm { Succeeded = true, Message = "User created successfully" };
                    else result = new ApiResultVm { Succeeded = false, Message = "Server Error" };
                }
                catch (Exception ex)
                {
                    result = new ApiResultVm { Succeeded = false, Message = ex.Message };
                }
                return result;
            }
        }

        

        public async  Task<RLoginVm> Authenticate(PLoginVm model)
        {
            RLoginVm result = new RLoginVm();
            try
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var check = await userManager.CheckPasswordAsync(user, model.Password);
                    if (check)
                    {
                        var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Audience = jwtConfig.Audience,
                            Issuer = jwtConfig.Issuer,
                            Expires = DateTime.Now.AddMinutes(jwtConfig.ExpiresInMinutes),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                            Subject = new ClaimsIdentity(new[]
                            {
                        new Claim(JwtRegisteredClaimNames.Sub, jwtConfig.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                        new Claim("id", user.Id),
                        new Claim("userName", user.UserName),
                    })
                        };
                        var newToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(newToken);

                        result.Token = token;
                        result.UserName = user.UserName;
                        result.Result = new ApiResultVm { Succeeded = true, Message = "Authentication successful" };
                    }
                    else
                    {
                        result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                    }
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }

        public async Task<ApiResultVm> ChangePassword(PChangePassword model)
        {
            ApiResultVm result = new ApiResultVm();
            try
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var check = await userManager.ChangePasswordAsync(user, model.OldPass, model.NewPass);
                    if (check.Succeeded)
                    {
                        result = new ApiResultVm { Succeeded = true, Message = "Password changed successfully" };
                    }
                    else result = new ApiResultVm { Succeeded = false, Message = "Old password is incorrect" };
                }
                else result = new ApiResultVm { Succeeded = false, Message = "User Not Found" };
            }
            catch (Exception ex)
            {
                result = new ApiResultVm { Succeeded = false, Message = ex.Message };


            }
            return result;
        }

        public async Task<RApplicationUserListVm> GetAll()
        {
            RApplicationUserListVm result = new RApplicationUserListVm();
            try
            {
                var users = await context.Users.ToListAsync();

                if (users != null)
                {
                    List<ApplicationUserVm> Vms = new List<ApplicationUserVm>();
                    foreach (var item in users)
                    {
                        ApplicationUserVm vm = new ApplicationUserVm
                        {
                            Id = item.Id,
                            UserName = item.UserName,
                            Email = item.Email,
                            FullName = item.FullName,
                            PhoneNumber = item.PhoneNumber
                        };
                        Vms.Add(vm);

                    }
                    result.Data = Vms;
                    result.Result = new ApiResultVm { Succeeded = true, Message = "Users retrieved successfully" };
                }
                else
                {
                    result.Result = new ApiResultVm { Succeeded = false, Message = "No users found" };
                }
            }
            catch (Exception ex)
            {
                result.Result = new ApiResultVm { Succeeded = false, Message = ex.Message };
            }
            return result;
        }
    }
}
