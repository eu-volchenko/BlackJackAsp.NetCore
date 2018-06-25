using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlackJack.BLL.Interfaces;
using BlackJack.DAL.Enums;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.RoundViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackJack.Controllers
{
    [Route("api/Game/")]
    public class GameApiController : Controller
    {
        private readonly IRoundService _roundService;

        public GameApiController(IRoundService roundService)
        {
            _roundService = roundService;
        }

        // GET: api/<controller>


        // GET api/<controller>/5
        //[HttpGet("GetGame/{id}")]
        //public string GetGame(int id)
        //{
        //    try
        //    {
        //        var gameModel = _roundService.GetGameInfo(id);
        //        var result = JsonConvert.SerializeObject(gameModel);
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.WriteLog(e.Message, "GameApiController");
        //        throw;
        //    }
        //}

        [HttpGet("CreateRound/{gameId}")]
        public async Task<string> CreateRound(int gameId)
        {
            try
            {
                var idRound = await _roundService.CreateRound(gameId);
                var result = JsonConvert.SerializeObject(idRound);
                return result;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
        }

        [HttpGet("GetPlayerCards/{game}/{roundId}")]
        public async Task<string> GetPlayerCards(int game, int roundId)
        {
            var userCard = await _roundService.GetPlayerDealerCards((int)PlayerTypeEnum.Player, game, roundId);
            var result = JsonConvert.SerializeObject(userCard);
            return result;
        }
        [HttpGet("GetDealerCards/{game}/{roundId}")]
        public async Task<string> GetDealerCards(int game, int roundId)
        {
            var userCard = await _roundService.GetPlayerDealerCards((int)PlayerTypeEnum.Dealer, game, roundId);
            var result = JsonConvert.SerializeObject(userCard);
            return result;
        }

        [HttpGet("GetBotsCards/{game}/{roundId}")]
        public async Task<string> GetBotsCards(int game, int roundId)
        {
            var usersCards = await _roundService.GetBotsCards(game, roundId);
            var result = JsonConvert.SerializeObject(usersCards);
            return result;
        }

        // PUT api/<controller>/5
        [HttpGet("GetCards")]
        public async Task<string> GetCardsForPlayers(int idGame, string nameUser, int roundId)
        {
            try
            {
                var userCards = await _roundService.GetCardsForStartGame(idGame, nameUser, roundId);
                return JsonConvert.SerializeObject(userCards);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
        }

        [HttpGet("OneMore/{roundId}/{userId}")]
        public async Task<string> OneMoreCard(int roundId, int userId)
        {
            try
            {
                var player = await _roundService.GetUser(userId);

                int idCard = await _roundService.GetCard(player, roundId);
                return JsonConvert.SerializeObject(idCard);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }

        }

        //[HttpPost("FinishRound")]
        //public async Task<string> FinishRound(InnerRoundViewModel model)
        //{
        //    try
        //    {
        //        var winner = await _roundService.LearnTheWinner(model);
        //        return JsonConvert.SerializeObject(winner);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.WriteLog(e.Message, "GameApiController");
        //        throw;
        //    }
        //}

        //[HttpPut("OneMoreBots")]
        //public async Task<HttpResponseMessage> OneMoreCardBots(int gameId, string userName, int roundId)
        //{
        //    try
        //    {
        //        var user = _roundService.GetUser(gameId, userName);
        //        await _roundService.GetCardsForBots(user, roundId);
        //        return new HttpResponseMessage(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.WriteLog(e.Message, "GameApiController");
        //        throw;
        //    }

        //}
    }
}
