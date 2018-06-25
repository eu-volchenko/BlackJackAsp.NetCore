using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackJack.ViewModels.HistoryViewModels;
using BlackJack.ViewModels.RoundViewModels;

namespace BlackJack.BLL.Interfaces
{
    public interface IHistoryService
    {
        Task<List<GameHistoriesViewModel>> GetGames();
        List<RoundViewModel> GetRounds(int gameId);
        RoundPlayersViewModel GetPlayers(int gameId);
        Task<PlayerCardHistoryViewModel> GetPlayersCards(int roundId, int userId);
    }
}
