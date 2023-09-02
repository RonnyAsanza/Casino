namespace Casino.Domain.Models.CasinoGames
{
    public class BetResultViewModel
    {
        public int RandomNumber { get; set; }
        public decimal MoneyWallet { get; set; }
        public decimal ProfitFullNumber { get; set; }
        public BetResultProfitColor ProfitsColor { get; set; } = new ();
        public BetBetDozenProfitColor ProfitsDozen { get; set; } = new();
    }

    public class BetResultProfitColor
    {
        public decimal ProfitColorRed { get; set; }
        public decimal ProfitColorBlack { get; set; }

    }

    public class BetBetDozenProfitColor
    {
        public decimal ProfitFirst { get; set; }
        public decimal ProfitSecond { get; set; }
        public decimal ProfitThrid { get; set; }
    }
}
