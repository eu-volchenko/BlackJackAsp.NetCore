using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.BLL.Interfaces;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.HistoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackJack.Controllers
{
    [Route("api/[controller]")]
    public class HistoryApiController : Controller
    {
        private readonly IHistoryService _historyService;

        public HistoryApiController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("GetGames")]
        public async Task<string> GetAllGames()
        {
            try
            {
                List<GameHistoriesViewModel> innerGameModels = await _historyService.GetGames();
                return JsonConvert.SerializeObject(innerGameModels);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }

        }


        [HttpGet("GetRounds")]
        public string GetRounds(int gameId)
        {
            try
            {
                var rounds = _historyService.GetRounds(gameId);
                return JsonConvert.SerializeObject(rounds);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }

        }


        [HttpGet("GetPlayers")]
        public string GetPlayers(int gameId)
        {
            try
            {
                var playersId = _historyService.GetPlayers(gameId);
                return JsonConvert.SerializeObject(playersId);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }

        }

        [HttpGet("GetPlayers")]
        public async Task<string> GetPlayersCards(int roundId, int userId)
        {
            try
            {
                var playersCards = await _historyService.GetPlayersCards(roundId, userId);
                return JsonConvert.SerializeObject(playersCards);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }

        }
    }
}
