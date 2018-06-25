using System;
using System.Threading.Tasks;
using BlackJack.BLL.Interfaces;
using BlackJack.Utitlity.Utilities;
using BlackJack.ViewModels.CreateGameViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlackJack.Controllers
{
    [Route("api/CreateGame")]
    public class CreateGameApiController : Controller
    {
        private readonly ICreateGameService _createGameService;
        public CreateGameApiController(ICreateGameService createGameService)
        {
            _createGameService = createGameService;
        }

        // POST api/<controller>
        [HttpPost]          
        public async Task<string> CreateGame([FromBody] InnerGameViewModel gameModel)
        {
            try
            {
                int id = _createGameService.AddGame(gameModel);
                await _createGameService.AddBots(gameModel, id);
                await _createGameService.AddDealer(gameModel, id);
                await _createGameService.AddPlayer(gameModel, id);
                return JsonConvert.SerializeObject(id);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "CreateGameApiController");
                throw;
            }
        }
    }
}
