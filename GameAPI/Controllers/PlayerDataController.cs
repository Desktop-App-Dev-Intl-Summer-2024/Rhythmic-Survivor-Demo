using GameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerDataController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public PlayerDataController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("SignUpPlayer")]
        public ServerResponse signUpPlayer(PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("gameDatabase"));
            DatabaseModel databaseModel = new DatabaseModel();
            response = databaseModel.signUpPlayer(connection, playerData);

            return response;
        }

        [HttpGet]
        [Route("GetAllGamesPlayerData/{email}")]
        public ServerResponse getAllPlayerData(string email)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("gameDatabase"));
            DatabaseModel databaseModel = new DatabaseModel();
            response = databaseModel.getAllPlayerData(connection, email);

            return response;
        }

        [HttpPut]
        [Route("SaveGameData")]
        public ServerResponse saveGameData(PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("gameDatabase"));
            DatabaseModel databaseModel = new DatabaseModel();
            response = databaseModel.saveGameData(connection, playerData);

            return response;
        }

        [HttpDelete]
        [Route("DeleteGameData")]
        public ServerResponse deleteGameData(PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("gameDatabase"));
            DatabaseModel databaseModel = new DatabaseModel();
            response = databaseModel.deleteGameData(connection, playerData);

            return response;
        }
    }
}
