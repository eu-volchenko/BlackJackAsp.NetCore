using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackJack.ViewModels.CreateGameViewModels;

namespace BlackJack.BLL.Interfaces
{
    public interface ICreateGameService
    {
        Task AddBots(InnerGameViewModel gameData, int id);

        Task AddPlayer(InnerGameViewModel gameData, int id);

        int AddGame(InnerGameViewModel gamedata);

        Task AddDealer(InnerGameViewModel gameData, int id);
    }
}
