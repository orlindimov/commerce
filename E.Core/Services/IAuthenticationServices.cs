using E.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.Core.Services
{
    public interface IAuthenticationServices
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);

        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(string refreshToken);

        CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
