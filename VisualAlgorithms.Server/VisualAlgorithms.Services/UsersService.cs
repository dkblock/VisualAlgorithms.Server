﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using VisualAlgorithms.Domain;
using VisualAlgorithms.Entities;
using VisualAlgorithms.Identity;
using VisualAlgorithms.Mappers;

namespace VisualAlgorithms.Services
{
    public class UserService
    {
        private readonly ApplicationUserMapper _userMapper;
        private readonly UserManager<ApplicationUserEntity> _userManager;        

        public UserService(ApplicationUserMapper userMapper, UserManager<ApplicationUserEntity> userManager)
        {
            _userMapper = userMapper;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);
            var userRole = await _userManager.GetRoleAsync(userEntity);

            return _userMapper.ToDomain(userEntity, userRole);
        }

        public async Task<bool> IsPasswordValid(string email, string password)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);

            if (userEntity != null)
                return await _userManager.CheckPasswordAsync(userEntity, password);

            return false;
        }

        public async Task<ApplicationUserEntity> GetUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
    }
}
