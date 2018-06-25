using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackJack.ViewModels.CreateGameViewModels;
using BlackJack.ViewModels.RoundViewModels;

namespace BlackJack.BLL.Interfaces
{
    public interface IRoundService
    {
        InnerGameViewModel GetGameInfo(int id);

        Task<UserCardsViewModel> GetCardsForStartGame(int gameId, string userName, int idRound);

        Task<int> CreateRound(int gameId);

        Task<UserViewModel> GetUser(int userId);

        Task<int> GetCard(UserViewModel userModelView, int roundId);

        Task<UserCardsViewModel> GetPlayerDealerCards(int role, int gameId, int roundId);

        Task<List<UserCardsViewModel>> GetBotsCards(int gameId, int roundId);

   //     Task<WinnerViewModel> LearnTheWinner(InnerRoundViewModel model);

        Task GetCardsForBots(UserViewModel userViewModel, int roundId);
    }
}
