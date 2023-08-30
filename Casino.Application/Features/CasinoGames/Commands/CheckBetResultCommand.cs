using AutoMapper;
using Casino.Application.Wrappers;
using Casino.Domain.Interfaces.Repositories;
using Casino.Domain.Models.CasinoGames;
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

            var user = await _userRepository.GetByIdAsync(request.checkBetResultViewModel.UserKey) ?? throw new Exception("User not exist");

            decimal totalBet = 0;
            decimal totalProfit = 0;

            BetResultViewModel betResultViewModel = new();

            Random _random = new();
            betResultViewModel.RandomNumber = _random.Next(0, 37);

            List<int> redNumbers = new List<int> { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            RouletteColor resultColor = redNumbers.Contains(betResultViewModel.RandomNumber) ? RouletteColor.Red : RouletteColor.Black;

            List<int> blackNumbers = new() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };


            foreach (var bet in request.checkBetResultViewModel.ColorBets)
            {
                if (bet.Quantity > 0 && bet.Color == resultColor && bet.Color == RouletteColor.Red)
                {
                    betResultViewModel.ResultColor += bet.Quantity * 2;
                }

                if (bet.Quantity > 0 && bet.Color == resultColor && bet.Color == RouletteColor.Black)
                {
                    betResultViewModel.ResultColor += bet.Quantity * 2;
                }

                totalBet += bet.Quantity;
            }
            totalProfit = betResultViewModel.ResultColor;

            user.Money += totalProfit - totalBet;

            return new Response<BetResultViewModel>(betResultViewModel);
        }
    }
}
