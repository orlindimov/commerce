using E.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E.Service;
using E.Core.Services;



namespace EE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthController(IAuthenticationServices authenticationService)
        {
            _authenticationServices = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationServices.CreateTokenAsync(loginDto);

            return CreateActionResult(result);
        }
        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationServices.CreateTokenByClient(clientLoginDto);

            return CreateActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationServices.RevokeRefreshToken(refreshTokenDto.Token);

            return CreateActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)

        {
            var result = await _authenticationServices.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return CreateActionResult(result);
        }
    }
}
