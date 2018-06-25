using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.BLL.Common;
using BlackJack.BLL.DTO;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Mapper;
using BlackJack.DAL.Dapper;
using BlackJack.DAL.Entities;
using BlackJack.DAL.Enums;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.CreateGameViewModels;
using BlackJack.ViewModels.RoundViewModels;

namespace BlackJack.BLL.Services
{
    public class RoundService:IRoundService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Card> _cardRepository;
        private readonly IGenericRepository<Round> _roundRepository;
        private readonly IGenericRepository<UserCard> _userCardRepository;
        private readonly int maxCountOfPoints = 21;
        private readonly int _userDefaultWin = 4;
        private readonly int _countCardsForStartGame = 2;
        public RoundService(IGenericRepository<Card> cardRepository, IGenericRepository<UserCard> userCardRepository, IGenericRepository<Round> roundRepository, IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _userCardRepository = userCardRepository;
            _roundRepository = roundRepository;
        }


        public  InnerGameViewModel GetGameInfo(int id)
        {
            try
            {
                var gameModel = new InnerGameViewModel();
                EntitiesToDTO entitiesToDto = new EntitiesToDTO();
                var listOfUserDto = new List<UserDTO>();
                var userList = _userRepository.GetAll().Where(x=>x.GameId == id);
                foreach (var item in userList)
                {
                    if (item.TypeId == (int)PlayerTypeEnum.Dealer) gameModel.dealerName = item.Name;
                    if (item.TypeId == (int)PlayerTypeEnum.Bot) gameModel.nameOfBots.Add(item.Name);
                    if (item.TypeId == (int)PlayerTypeEnum.Player) gameModel.playerName = item.Name;
                }
                gameModel.numberOfBots = gameModel.nameOfBots.Count;
                gameModel.id = id;
                return gameModel;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return null;
            }

        }

        public async Task<UserCardsViewModel> GetCardsForStartGame(int gameId, string userName, int idRound)
        {
            try
            {
                int countsCardsInStartGame = 2;
                var user = _userRepository.GetAll().SingleOrDefault(x => x.GameId==gameId && x.Name == userName);
                var userCards = new UserCardsViewModel();
                userCards.id = user.Id;
                for (int i = 0; i < countsCardsInStartGame; i++)
                {
                    var random = Randomizer.RandomId();
                    var cardForUser = _cardRepository.Get(random);
                    userCards.cards.Add(cardForUser.Id);
                    var userCard = new UserCard();
                    userCard.CardId = cardForUser.Id;
                    userCard.UserId = user.Id;
                    userCard.RoundId = idRound;
                    await _userCardRepository.CreateAsync(userCard);
                    Thread.Sleep(100);
                }
                return userCards;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return null;
            }

        }

        public async Task<int> CreateRound(int gameId)
        {
            try
            {
                var rounds = _roundRepository.GetAll().Where(x => x.GameId == gameId).ToList();
                int roundsCount = rounds.Count();
                var createRound = new Round();
                createRound.GameId = gameId;
                createRound.RoundInGame = ++roundsCount;
                createRound.UserId = _userDefaultWin;
                await _roundRepository.CreateAsync(createRound);
                return createRound.Id;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return 0;
            }

        }



        private string[] ReturnBotsNames(IEnumerable<dynamic> users)
        {
            int i = 0;
            string[] names = new string[users.Count()];
            foreach (var user in users)
            {
                names[i] = user.Name;
                i++;
            }

            return names;
        }

        public async Task<UserViewModel> GetUser(int userId)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                var userViewModel = new UserViewModel();
                userViewModel.Id = user.Id;
                userViewModel.Name = user.Name;
                userViewModel.Type = user.TypeId;
                return userViewModel;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return null;
            }

        }

        public async Task<int> GetCard(UserViewModel userModelView, int roundId)
        {
            try
            {
                var cardId = Randomizer.RandomId();
                var userCard = new UserCard();
                userCard.CardId = cardId;
                userCard.UserId = userModelView.Id;
                userCard.RoundId = roundId;
                await _userCardRepository.CreateAsync(userCard);
                return cardId;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return 0;
            }

        }

        public async Task<UserCardsViewModel> GetPlayerDealerCards(int role, int gameId, int roundId)
        {

            var userCards = new UserCardsViewModel();
            var user = _userRepository.GetAll().Single(player => player.GameId == gameId && player.TypeId == role);
            userCards.id = user.Id;
            userCards.name = user.Name;
            for (int i = 0; i < _countCardsForStartGame; i++)
            {
                var cardId = Randomizer.RandomId();
                var cardForUser = _cardRepository.Get(cardId);
                userCards.cards.Add(cardForUser.Id);
                var userCard = new UserCard();
                userCard.CardId = cardForUser.Id;
                userCard.UserId = user.Id;
                userCard.RoundId = roundId;
                await _userCardRepository.CreateAsync(userCard);
                Thread.Sleep(100);
            }

            return userCards;
        }

        public async Task<List<UserCardsViewModel>> GetBotsCards(int gameId, int roundId)
        {
            var bots = _userRepository.GetAll()
                .Where(bot => bot.TypeId == (int) PlayerTypeEnum.Bot && bot.GameId == gameId);
            var listOfBotCards = new List<UserCardsViewModel>();
            foreach (var bot in bots)
            {
                var userCardViewModel = new UserCardsViewModel();
                userCardViewModel.name = bot.Name;
                userCardViewModel.id = bot.Id;
                for (int i = 0; i < _countCardsForStartGame; i++)
                {
                    var cardId = Randomizer.RandomId();
                    var cardForUser = _cardRepository.Get(cardId);
                    userCardViewModel.cards.Add(cardForUser.Id);
                    var userCard = new UserCard();
                    userCard.CardId = cardForUser.Id;
                    userCard.UserId = bot.Id;
                    userCard.RoundId = roundId;
                    await _userCardRepository.CreateAsync(userCard);
                    Thread.Sleep(100);
                }
                listOfBotCards.Add(userCardViewModel);
            }

            return listOfBotCards;
        }

        //public async Task<WinnerViewModel> LearnTheWinner(InnerRoundViewModel model)
        //{
        //    try
        //    {
        //        int maxCountOfPointAmongPlayers = 0;
        //        var botsScore = new List<int>();
        //        var botsCards = new List<IEnumerable<UserCard>>();
        //        var player = _userRepository.GetAll().SingleOrDefault(x => x.GameId == model.Id && x.name == model.PlayerName);
        //        var playerCards = _userCardRepository.GetAll()
        //            .Where(x => x.RoundId == model.RoundId && x.id == player.Id).ToList();
        //        var dealer = _userRepository.GetAll()
        //            .SingleOrDefault(x => x.GameId == model.Id && x.name == model.DealerName);
        //        var dealerCards = _userCardRepository.GetAll()
        //            .Where(x => x.RoundId == model.RoundId && x.id == dealer.Id).ToList();
        //        for (int i = 0; i < model.NumberOfBots; i++)
        //        {
        //            var bot = _userRepository.GetAll()
        //                .SingleOrDefault(x => x.GameId == model.Id && x.name == model.NameOfBots[i]);
        //            var botCards = _userCardRepository.GetAll()
        //                .Where(x => x.RoundId == model.RoundId && x.id == bot.Id).ToList();
        //            botsCards.Add(botCards);
        //        }

        //        string winnerName = "";
        //        IEnumerable<UserCard> winnerCards = null;
        //        var playerScore = PointCount(playerCards);
        //        var dealerScore = PointCount(dealerCards);
        //        for (int i = 0; i < model.NumberOfBots; i++)
        //        {
        //            botsScore.Add(PointCount(botsCards[i]));
        //        }

        //        for (int i = 0; i < model.NumberOfBots; i++)
        //        {
        //            if (botsScore[i] > maxCountOfPointAmongPlayers && botsScore[i] <= maxCountOfPoints)
        //            {
        //                maxCountOfPointAmongPlayers = botsScore[i];
        //                winnerName = model.NameOfBots[i];
        //                winnerCards = botsCards[i];
        //            }
        //        }

        //        if (playerScore > maxCountOfPointAmongPlayers && playerScore <= maxCountOfPoints)
        //        {
        //            maxCountOfPointAmongPlayers = playerScore;
        //            winnerName = model.PlayerName;
        //            winnerCards = playerCards;
        //        }

        //        if (dealerScore > maxCountOfPointAmongPlayers && dealerScore <= maxCountOfPoints)
        //        {
        //            maxCountOfPointAmongPlayers = dealerScore;
        //            winnerName = model.DealerName;
        //            winnerCards = dealerCards;
        //        }

        //        var winner = new WinnerViewModel();
        //        winner.name = winnerName;
        //        winner.Score = maxCountOfPointAmongPlayers;
        //        var winnerUser = _userRepository.GetAll()
        //            .SingleOrDefault(x => x.GameId == model.Id && x.name == winnerName);
        //        winner.id = winnerUser.Id;
        //        winner.Cards = getWinnerCards(winnerCards);
        //        var round = await _roundRepository.GetAsync(model.RoundId);
        //        round.id = winner.id;
        //        await _roundRepository.UpdateAsync(round);
        //        return winner;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogWriter.WriteLog(ex.Message, "RoundService");
        //        return null;
        //    }
        //}

        public async Task GetCardsForBots(UserViewModel userViewModel, int roundId)
        {
            try
            {
                var userCards = _userCardRepository.GetAll()
                    .Where(x => x.RoundId == roundId && x.UserId == userViewModel.Id).ToList();
                var userPoints = PointCount(userCards);
                while (userPoints < 17)
                {
                    var randomCard = Randomizer.RandomId();
                    var userCard = new UserCard();
                    userCard.CardId = randomCard;
                    userCard.UserId = userViewModel.Id;
                    userCard.RoundId = roundId;
                    await _userCardRepository.CreateAsync(userCard);
                    userPoints += _cardRepository.Get(randomCard).Cost;
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
            }

        }

        private List<int> getWinnerCards(IEnumerable<UserCard> userCards)
        {
            try
            {
                var cards = new List<int>();
                foreach (var userCard in userCards)
                {
                    cards.Add(userCard.CardId);
                }
                return cards;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "RoundService");
                return null;
            }

        }

        private int PointCount(IEnumerable<UserCard> userCards)
        {
            int sumOfPointsCard = 0;
            foreach (var userCard in userCards)
            {
                var card = _cardRepository.Get(userCard.CardId);
                sumOfPointsCard += card.Cost;
            }
            return sumOfPointsCard;
        }

    }
}
