using Casino.Domain.Models.Bets;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Casino.Domain.Models.CasinoGames
{
    public class CheckBetResultViewModel
    {
        [Required]
        public int UserKey { get; set; }

        // Color
        private List<BetColor> _colorBets = new();
        public List<BetColor> ColorBets
        {
            get { return _colorBets; }
            set
            {
                _colorBets = ConsolidateBets(value, b => b.Color, "Color");
            }
        }

        // Full Number
        private List<BetFullNumber> _fullNumber = new();
        public List<BetFullNumber> FullNumber
        {
            get { return _fullNumber; }
            set
            {
                _fullNumber = ConsolidateBets(value, b => b.Number, "Number");
            }
        }

        // Dozen
        private List<BetDozen> _dozenBets = new();
        public List<BetDozen> DozenBets
        {
            get { return _dozenBets; }
            set
            {
                _dozenBets = ConsolidateBets(value, b => b.Dozen, "Dozen");
            }
        }

        private static List<T> ConsolidateBets<T, TKey>(List<T> bets, Func<T, TKey> keySelector, string keyPropertyName)
        {
            return bets.GroupBy(keySelector)
                       .Select(group =>
                       {
                           var instance = Activator.CreateInstance<T>();
                           var quantityProperty = typeof(T).GetProperty("Quantity");
                           var keyProperty = typeof(T).GetProperty(keyPropertyName);

                           if (quantityProperty != null && keyProperty != null)
                           {
                               keyProperty.SetValue(instance, group.Key);
                               decimal totalQuantity = group.Sum(b => (decimal)quantityProperty.GetValue(b));
                               quantityProperty.SetValue(instance, totalQuantity);
                           }

                           return instance;
                       })
                       .ToList();
        }
    }
}
