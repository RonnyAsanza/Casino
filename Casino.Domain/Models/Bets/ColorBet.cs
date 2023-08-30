using static Casino.Domain.Utils.Enum;

namespace Casino.Domain.Models.Bets
{
    public class ColorBet
    {
        public RouletteColor Color { get; set; }
        public decimal Quantity { get; set; }
    }
}
