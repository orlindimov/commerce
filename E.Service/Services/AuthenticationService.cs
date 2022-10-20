using E.Core.DTOs;
using E.Core.Models;
using E.Core.Repositories;
using E.Core.Services;
using E.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E.Service.Services
{
    public class AuthenticationService : IAuthenticationServices
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(List<Client> clients, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = clients;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user=await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return CustomResponseDto<TokenDto>.Fail(400, "Email or password is wrong");

            if(!await _userManager.CheckPasswordAsync(user,loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(400,"Email or password is wrong");
            }
            var token=_tokenService.CreateToken(user);

            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if(userRefreshToken==null)
            {
                await _userRefreshTokenService.AddAsync(new UserRefreshToken { UserId=user.Id,Code=token.RefreshToken,Expiration=token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code=token.RefreshToken;
                userRefreshToken.Expiration=token.RefreshTokenExpiration;

            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200,token);
        }

        public CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if(client==null)
            {
                return CustomResponseDto<ClientTokenDto>.Fail(404, "ClientId or ClientSecret not found");
            }
            var token = _tokenService.CreateTokenByClient(client);

            return CustomResponseDto<ClientTokenDto>.Success(200,token);
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return CustomResponseDto<TokenDto>.Fail(400,"Refresh token not found");
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(400,"User Id not found");
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<TokenDto>.Success(200,tokenDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404,"Refresh token not found");
            }

            _userRefreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
