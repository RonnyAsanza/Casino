using static Casino.Domain.Utils.Enum;

namespace Casino.Domain.Models.Bets
{
    public class BetColor
    {
        private RouletteColor _color;

        public RouletteColor Color
        {
            get { return _color; }
            set
            {
                if (!Enum.IsDefined(typeof(RouletteColor), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(RouletteColor), "The color must be Red (0) or Black (1).");
                }
                _color = value;
            }
        }

        public decimal Quantity { get; set; }
    }
}
