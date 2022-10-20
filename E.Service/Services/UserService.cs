using AutoMapper;
using E.Core.DTOs;
using E.Core.Models;
using E.Core.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };
            var userapp=_mapper.Map<UserAppDto>(user);
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<UserAppDto>.Fail(400, (errors));
            }
            
            return CustomResponseDto<UserAppDto>.Success(200,userapp);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404,"UserName not found");
            }
            var userapp = _mapper.Map<UserAppDto>(user);

            return CustomResponseDto<UserAppDto>.Success( 200, userapp);
        }
    }
}

//var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
//var productsDto = _mapper.Map<ProductDto>(product);
//return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));