using static Casino.Domain.Utils.Enum;

namespace Casino.Domain.Models.Bets
{
    public class BetDozen
    {
        private RouletteDozen _dozen { get; set; }
        public RouletteDozen Dozen
        {
            get { return _dozen; }
            set
            {
                if (!Enum.IsDefined(typeof(RouletteDozen), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(RouletteDozen), "The dozen must be 1 (1-12), 2 (2-24) or 3 (25-36).");
                }
                _dozen = value;
            }
        }
        public decimal Quantity { get; set; }
    }
}
