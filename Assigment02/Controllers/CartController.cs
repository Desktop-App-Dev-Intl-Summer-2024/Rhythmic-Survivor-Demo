using Microsoft.AspNetCore.Mvc;
using Assigment02.Models;
using System.Data.SqlClient;

namespace Assigment02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public CartController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpPost]
        [Route("AddItem")]
        public ServerResponse addItem(ItemInfo item)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("FarmerStorage"));
            DatabaseModel database = new DatabaseModel();
            response = database.addItem(connection, item);

            return response;
        }

        [HttpGet]
        [Route("GetAllItems")]
        public ServerResponse getAllItems()
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("FarmerStorage"));
            DatabaseModel database = new DatabaseModel();
            response = database.getAllItems(connection);

            return response;
        }

        [HttpGet]
        [Route("SearchItem/{id}")]
        public ServerResponse searchItem(int id)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("FarmerStorage"));
            DatabaseModel database = new DatabaseModel();
            response = database.searchItem(connection, id);

            return response;
        }

        [HttpPut]
        [Route("UpdateItem/{id}")]
        public ServerResponse updateItem(int id, ItemInfo item)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("FarmerStorage"));
            DatabaseModel database = new DatabaseModel();
            response = database.updateItem(connection, id, item);

            return response;
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public ServerResponse deleteItem(int id)
        {
            ServerResponse response = new ServerResponse();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("FarmerStorage"));
            DatabaseModel database = new DatabaseModel();
            response = database.deleteItem(connection, id);

            return response;
        }
    }
}
