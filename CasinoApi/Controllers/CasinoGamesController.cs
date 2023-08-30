using Casino.Application.Features.CasinoGames.Commands;
using Casino.Domain.Models.CasinoGames;
using CasinoApi.Controllers.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CasinoApi.Controllers
{
    [ApiVersion("1.0")]
    public class CasinoGamesController : BaseApiController
    {
        [HttpPost("checkBetResult")]
        public async Task<IActionResult> CheckBetResult([FromBody] CheckBetResultViewModel checkBetResult)
        {
            return Ok(await Mediator.Send(new CheckBetResultCommand { checkBetResultViewModel = checkBetResult }));
        }
    }
}
