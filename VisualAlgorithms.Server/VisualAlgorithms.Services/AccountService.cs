﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VisualAlgorithms.Common.Constants;
using VisualAlgorithms.Entities;
using VisualAlgorithms.Identity;
using VisualAlgorithms.Mappers;
using VisualAlgorithms.Models.Account;

namespace VisualAlgorithms.Services
{
    public class AccountService
    {
        private readonly AuthenticationService _authService;
        private readonly ApplicationUsersMapper _usersMapper;
        private readonly UserManager<ApplicationUserEntity> _userManager;

        public AccountService(
            AuthenticationService authService,
            ApplicationUsersMapper usersMapper,
            UserManager<ApplicationUserEntity> userManager)
        {
            _authService = authService;
            _usersMapper = usersMapper;
            _userManager = userManager;
        }

        public async Task<AuthModel> Authenticate(string authorization)
        {
            var accessToken = authorization.Replace("Bearer ", "");
            var authModel = _authService.CheckAuth(accessToken);
            var userEntity = await _userManager.FindByIdAsync(authModel.CurrentUser.Id);

            return _usersMapper.ToModel(userEntity, authModel.CurrentUser.Role, authModel.AccessToken);
        }

        public async Task<AuthModel> Login(LoginModel loginModel)
        {
            var userEntity = await _userManager.FindByEmailAsync(loginModel.Email);
            var userRole = await _userManager.GetRoleAsync(userEntity);
            var user = _usersMapper.ToDomain(userEntity, userRole);
            var accessToken = _authService.Authenticate(user);

            return _usersMapper.ToModel(userEntity, userRole, accessToken);
        }

        public async Task<AuthModel> Register(RegisterModel registerModel)
        {
            var userEntity = _usersMapper.ToEntity(registerModel);
            var result = await _userManager.CreateAsync(userEntity, registerModel.Password);

            if (result.Succeeded)
            {
                var userRole = Roles.User;
                await _userManager.AddToRoleAsync(userEntity, userRole);
                var user = _usersMapper.ToDomain(userEntity, userRole);
                var accessToken = _authService.Authenticate(user);

                return _usersMapper.ToModel(userEntity, userRole, accessToken);
            }

            return null;
        }        
    }
}
