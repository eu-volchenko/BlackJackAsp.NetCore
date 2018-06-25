using System;
using System.Collections.Generic;
using System.Text;
using BlackJack.BLL.DTO;
using BlackJack.DAL.Entities;

namespace BlackJack.BLL.Mapper
{
    class DTOToEntities
    {
        public Game GetGame(GameDTO gameDTO)
        {
            var game = new Game();
            game.NumberOfPlayers = gameDTO.NumberOfPlayers;
            return game;
        }

        public User GetBot(UserDTO userDto)
        {
            var user = new User();
            user.Name = userDto.Name;
            user.TypeId = userDto.TypeId;
            user.GameId = userDto.GameId;
            return user;
        }

        public User GetPlayer(UserDTO userDto)
        {
            var user = new User();

            user.Name = userDto.Name;
            user.TypeId = userDto.TypeId;
            user.GameId = userDto.GameId;
            return user;
        }

        public User GetDealer(UserDTO userDto)
        {
            var user = new User();
            user.Name = userDto.Name;
            user.TypeId = userDto.TypeId;
            user.GameId = userDto.GameId;
            return user;
        }
    }
}
