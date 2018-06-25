using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Mapper;
using BlackJack.DAL.Entities;
using BlackJack.DAL.Enums;
using BlackJack.DAL.Interfaces;
using BlackJack.DAL.Repositories;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.CreateGameViewModels;


namespace BlackJack.BLL.Services
{
    public class CreateGameService:ICreateGameService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly DTOToEntities _dtoToEntities;
        private readonly ModelViewToDTO _modelViewToDto;

        public CreateGameService(IGenericRepository<History> historyRepository, IGenericRepository<User> userRepository, IGenericRepository<Game> gameRepository)
        {
            _modelViewToDto = new ModelViewToDTO();
            _dtoToEntities = new DTOToEntities();
            _gameRepository = gameRepository;
            _historyRepository = historyRepository;
            _userRepository = userRepository;
        }

        public async Task AddBots(InnerGameViewModel gameData, int id)
        {
            try
            {
                for (int i = 0; i < gameData.nameOfBots.Count; i++)
                {
                    var bot = new UserDTO();
                    bot.TypeId = (int)PlayerTypeEnum.Bot;
                    bot.GameId = id;
                    bot = _modelViewToDto.GetBotDto(gameData.nameOfBots[i], bot);
                    var botEntity = _dtoToEntities.GetBot(bot);
                    var task = _userRepository.CreateAsync(botEntity);
                    await task;
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "CreateGameService");
            }

        }

        public async Task AddPlayer(InnerGameViewModel gameData, int id)
        {
            try
            {
                var playerDto = new UserDTO();
                playerDto.GameId = id;
                _modelViewToDto.GetPlayerDto(gameData, playerDto);
                await _userRepository.CreateAsync(_dtoToEntities.GetPlayer(playerDto));
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "CreateGameService");
            }



        }

        public int AddGame(InnerGameViewModel gamedata)
        {
            try
            {
                var gameDto = new GameDTO();
                _modelViewToDto.GetGameDto(gamedata, gameDto);
                var game = _dtoToEntities.GetGame(gameDto);
                _gameRepository.Create(game);
                var historyGame = new History();
                historyGame.LogDateTime = DateTime.Now;
                historyGame.GameId = game.Id;
                _historyRepository.Create(historyGame);
                return game.Id;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "CreateGameService");
                return 0;
            }
        }

        public async Task AddDealer(InnerGameViewModel gameData, int id)
        {
            try
            {
                var dealerDto = new UserDTO();
                dealerDto.GameId = id;
                dealerDto = _modelViewToDto.GetDealerDto(gameData, dealerDto);
                await _userRepository.CreateAsync(_dtoToEntities.GetDealer(dealerDto));
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "CreateGameService");
            }

        }
    }
}
