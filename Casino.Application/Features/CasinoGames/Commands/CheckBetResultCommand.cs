using AutoMapper;
using Casino.Application.Validations;
using Casino.Application.Wrappers;
using Casino.Domain.Interfaces.Repositories;
using Casino.Domain.Models.Bets;
using Casino.Domain.Models.CasinoGames;
using Casino.Domain.Utils;
using MediatR;
using static Casino.Domain.Utils.Enum;

namespace Casino.Application.Features.CasinoGames.Commands
{
    public class CheckBetResultCommand : IRequest<Response<BetResultViewModel>>
    {
        public CheckBetResultViewModel checkBetResultViewModel { get; set; } = new CheckBetResultViewModel();
    }

    public class CheckBetResultCommandHandler : IRequestHandler<CheckBetResultCommand, Response<BetResultViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public CheckBetResultCommandHandler(IMapper mapper, IMediator mediator, IUserRepository userRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<Response<BetResultViewModel>> Handle(CheckBetResultCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.checkBetResultViewModel.UserKey);
            var validation = await new CheckBetResultCommandValidator(user).ValidateAsync(request.checkBetResultViewModel, cancellationToken);
            if (!validation.IsValid)
                return new Response<BetResultViewModel>("Error Validations", validation.Errors.Select(x => x.ErrorMessage));

            BetResultViewModel betResultViewModel = new();
            Random _random = new();
            betResultViewModel.RandomNumber = _random.Next(0, 37);

            decimal totalBet = 0;
            totalBet += BetColors(betResultViewModel.RandomNumber, request.checkBetResultViewModel.ColorBets, betResultViewModel);
            totalBet += BetFullNumbers(betResultViewModel.RandomNumber, request.checkBetResultViewModel.FullNumber, betResultViewModel);
            totalBet += BetDozen(betResultViewModel.RandomNumber, request.checkBetResultViewModel.DozenBets, betResultViewModel);


            user.Money += 
                (betResultViewModel.ProfitsColor.ProfitColorRed + betResultViewModel.ProfitsColor.ProfitColorBlack)
                + betResultViewModel.ProfitFullNumber 
                + betResultViewModel.ProfitsDozen.ProfitFirst + betResultViewModel.ProfitsDozen.ProfitSecond + betResultViewModel.ProfitsDozen.ProfitThrid
                - totalBet;

            //
            betResultViewModel.MoneyWallet = user.Money;
            user.DateModifiedUtc = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            return new Response<BetResultViewModel>(betResultViewModel);
        }

        private static decimal BetColors(int number, List<BetColor> colorBets, BetResultViewModel betResultViewModel)
        {
            RouletteColor resultColor = Constants.GetRedNumbers().Contains(number) ? RouletteColor.Red : RouletteColor.Black;
            decimal totalBet = colorBets.Sum(n => n.Quantity);

            var specificColorBet = colorBets.FirstOrDefault(c => c.Color == resultColor);

            if (specificColorBet != null)
            {
                if (resultColor == RouletteColor.Red)
                {
                    betResultViewModel.ProfitsColor.ProfitColorRed = specificColorBet.Quantity * 2;
                }
                else //black
                {
                    betResultViewModel.ProfitsColor.ProfitColorBlack = specificColorBet.Quantity * 2;
                }
            }

            return totalBet;
        }

        private static decimal BetFullNumbers(int number, List<BetFullNumber> numbers, BetResultViewModel betResultViewModel)
        {

            var specificNumber = numbers.FirstOrDefault(n => n.Number == number);

            if (specificNumber != null)
            {
                betResultViewModel.ProfitFullNumber = specificNumber.Quantity * 36;
            }

            return numbers.Sum(n => n.Quantity);
        }

        private static decimal BetDozen(int number, List<BetDozen> betDozens, BetResultViewModel betResultViewModel)
        {
            RouletteDozen resultDozen =
                Constants.GetFirstDozen().Contains(number) ? RouletteDozen.First :
                Constants.GetSecondDozen().Contains(number) ? RouletteDozen.Second : RouletteDozen.Third;
            decimal totalBet = betDozens.Sum(n => n.Quantity);

            var specificDozen = betDozens.FirstOrDefault(d => d.Dozen == resultDozen);

            if (specificDozen != null)
            {
                if (resultDozen == RouletteDozen.First)
                {
                    betResultViewModel.ProfitsDozen.ProfitFirst = specificDozen.Quantity * 3;
                }
                else if (resultDozen == RouletteDozen.Second)
                {
                    betResultViewModel.ProfitsDozen.ProfitSecond = specificDozen.Quantity * 3;
                }
                else //Third
                {
                    betResultViewModel.ProfitsDozen.ProfitThrid = specificDozen.Quantity * 3;
                }
            }

            return betDozens.Sum(n => n.Quantity);
        }
    }
}
