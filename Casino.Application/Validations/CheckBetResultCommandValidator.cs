using Casino.Domain.Entities;
using Casino.Domain.Models.CasinoGames;
using FluentValidation;

namespace Casino.Application.Validations
{
    public class CheckBetResultCommandValidator : AbstractValidator<CheckBetResultViewModel>
    {
        public decimal totalBet = 0;
        public CheckBetResultCommandValidator(User? user)
        {
            RuleFor(x => x)
                .Custom((viewModel, context) =>
                {
                    if (user == null)
                    {
                        context.AddFailure("UserKey", "User not exist");
                    }
                    else
                    {
                        decimal totalQuantityColors = viewModel.ColorBets.Sum(bet => bet.Quantity);
                        decimal totalQuantityFullNumber = viewModel.FullNumber.Sum(bet => bet.Quantity);
                        decimal totalQuantityDozen = viewModel.DozenBets.Sum(bet => bet.Quantity);

                        totalBet = totalQuantityColors + totalQuantityFullNumber + totalQuantityDozen;
                        if (totalBet > user.Money)
                        {
                            context.AddFailure("Bet", "Total bet amount exceeds maximum allowed.");
                        }
                    }
                });
        }
    }
}
