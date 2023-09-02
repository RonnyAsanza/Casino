namespace Casino.Domain.Models.Bets
{
    public class BetFullNumber
    {
        private int _number;
        private decimal _quantity;

        public int Number
        {
            get { return _number; }
            set
            {
                if (value < 0 || value > 36)
                {
                    throw new ArgumentOutOfRangeException(nameof(Number), "El número debe estar entre 0 y 36 inclusive.");
                }
                _number = value;
            }
        }

        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Quantity), "The quantity cannot be less than or equal to 0.");
                }
                _quantity = value;
            }
        }
    }
}

