using Casino.Domain.Models.Bets;
using System.ComponentModel.DataAnnotations;

namespace Casino.Domain.Models.CasinoGames
{
    public class CheckBetResultViewModel
    {
        [Required]
        public int UserKey { get; set; }
        public List<ColorBet> ColorBets { get; set; } = new List<ColorBet>();
    }
}
