using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Interfaces;
using BlackJack.DAL.Entities;
using BlackJack.DAL.Interfaces;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.HistoryViewModels;
using BlackJack.ViewModels.RoundViewModels;

namespace BlackJack.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Round> _roundRepository;
        private readonly IGenericRepository<UserCard> _userCardRepository;
        private int countUsersWithoutBots = 2;


        public HistoryService(IGenericRepository<UserCard> userCardRepository, IGenericRepository<Game> gameRepository, IGenericRepository<User> userRepository, IGenericRepository<History> historyRepository, IGenericRepository<Round> roundRepository)
        {
            _userCardRepository = userCardRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _historyRepository = historyRepository;
            _roundRepository = roundRepository;
        }


        public async Task<List<GameHistoriesViewModel>> GetGames()
        {
            try
            {
                var gameHistorieses = new List<GameHistoriesViewModel>();
                var listOfHistories = await _historyRepository.GetAllAsync();
                foreach (var history in listOfHistories)
                {
                    var game = await _gameRepository.GetAsync(history.GameId);
                    var gameHistory = new GameHistoriesViewModel();
                    gameHistory.DateTimeGame = history.LogDateTime;
                    gameHistory.CountOfBots = game.NumberOfPlayers - countUsersWithoutBots;
                    gameHistory.Id = game.Id;
                    gameHistorieses.Add(gameHistory);
                }
                return gameHistorieses;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryService");
                return null;
            }

        }

        public List<RoundViewModel> GetRounds(int gameId)
        {
            try
            {
                var roundsInGame = _roundRepository.GetAll().Where(round => round.GameId == gameId).ToList();
                var roundsList = new List<RoundViewModel>();
                foreach (var currentRound in roundsInGame)
                {
                    var round = new RoundViewModel();
                    round.id = currentRound.Id;
                    round.gameId = gameId;
                    round.roundInGame = currentRound.RoundInGame;
                    round.winnerName = _userRepository.Get(currentRound.UserId).Name;
                    roundsList.Add(round);
                }
                return roundsList;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryService");
                return null;
            }

        }

        public RoundPlayersViewModel GetPlayers(int gameId)
        {
            try
            {
                var playersInGame = _userRepository.GetAll().Where(player => player.GameId == gameId).ToList();
                var playersId = new List<int>();
                foreach (var user in playersInGame)
                {
                    playersId.Add(user.Id);
                }
                var roundPlayers = new RoundPlayersViewModel();
                roundPlayers.PlayersId = playersId;
                return roundPlayers;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryService");
                return null;
            }

        }

        public async Task<PlayerCardHistoryViewModel> GetPlayersCards(int roundId, int userId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var cards = _userCardRepository.GetAll().Where(player => player.UserId == userId && player.RoundId == roundId).ToList();
                var cardsList = new List<int>();
                foreach (var userCard in cards)
                {
                    cardsList.Add(userCard.CardId);
                }

                var playerCardHistoryModelView = new PlayerCardHistoryViewModel();
                playerCardHistoryModelView.PlayerName = user.Name;
                playerCardHistoryModelView.CardsId = cardsList;
                return playerCardHistoryModelView;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryService");
                return null;
            }

        }
    }
}
