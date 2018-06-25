using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.BLL.DTO;
using BlackJack.DAL.Entities;

namespace BlackJack.BLL.Mapper
{
    class EntitiesToDTO
    {
        public GameDTO GameToGameDto(Game game)
        {
            var gameDto = new GameDTO();
            gameDto.NumberOfPlayers = game.NumberOfPlayers;
            gameDto.Id = game.Id;
            return gameDto;
        }

        public UserDTO UserToUserDto(User user)
        {
            var userDto = new UserDTO();
            userDto.GameId = user.GameId;
            userDto.Id = user.Id;
            userDto.TypeId = user.TypeId;
            userDto.Name = user.Name;
            return userDto;
        }
    }
}
