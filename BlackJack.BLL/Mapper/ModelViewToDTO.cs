using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.BLL.DTO;
using BlackJack.DAL.Enums;
using BlackJack.ViewModels.CreateGameViewModels;

namespace BlackJack.BLL.Mapper
{
    class ModelViewToDTO
    {
        public GameDTO GetGameDto(InnerGameViewModel innerGameModel, GameDTO gameDto)
        {
            gameDto.NumberOfPlayers = innerGameModel.numberOfBots + 2;
            return gameDto;
        }

        public UserDTO GetBotDto(string name, UserDTO userDto)
        {
            userDto.Name = name;
            return userDto;
        }

        public UserDTO GetPlayerDto(InnerGameViewModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = (int)PlayerTypeEnum.Player;
            userDto.Name = innerGameModel.playerName;
            return userDto;
        }

        public UserDTO GetDealerDto(InnerGameViewModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = (int)PlayerTypeEnum.Dealer;
            userDto.Name = innerGameModel.dealerName;
            return userDto;
        }
    }
}
