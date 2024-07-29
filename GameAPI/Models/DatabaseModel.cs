using System.Data;
using System.Data.SqlClient;

namespace GameAPI.Models
{
    public class DatabaseModel
    {
        public ServerResponse signUpPlayer(SqlConnection connection, PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "insert into dbo.PlayerData (player_id, player_nickName, player_email, player_password, player_saveSlot) " +
                               "values (@id, @name, @email, @password, 1), (@id, @name, @email, @password, 2), (@id, @name, @email, @password, 3)";
                SqlCommand command = new SqlCommand(query, connection);
                char[] nickNameChars = playerData.nickName.ToCharArray();
                DateTime dateTime = DateTime.Now;
                string idString = nickNameChars[0] + nickNameChars[1] + nickNameChars[2] + dateTime.ToString("yyyy");
                Console.WriteLine(idString);
                int id =  int.Parse(idString);
                Console.WriteLine(id);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", playerData.nickName);
                command.Parameters.AddWithValue("@email", playerData.email);
                command.Parameters.AddWithValue("@password", playerData.password);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    List<PlayerData> gameData = new List<PlayerData>();
                    for (int i = 1; i < 4; i++)
                    {
                        PlayerData newData = new PlayerData();
                        newData.id = id;
                        newData.nickName = playerData.nickName;
                        newData.email = playerData.email;
                        newData.password = playerData.password;
                        newData.saveSlot = i;
                        gameData.Add(newData);
                    }

                    response.statusCode = 200;
                    response.statusMessage = "Account Created!";
                    response.dataChanged = null;
                    response.gamesData = gameData;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Error Creating Account!";
                    response.dataChanged = null;
                    response.gamesData = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.dataChanged = null;
                response.gamesData = null;
            }

            return response;
        }

        public ServerResponse getAllPlayerData(SqlConnection connection, string email)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "select * from dbo.PlayerData where player_email = '" + email +"'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                List<PlayerData> gameData = new List<PlayerData>();
                if(table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        PlayerData playerData = new PlayerData();
                        playerData.id = (int)row["player_id"];
                        playerData.nickName = row["player_nickName"].ToString();
                        playerData.email = row["player_email"].ToString();
                        playerData.password = row["player_password"].ToString();
                        playerData.saveSlot = (int)row["player_saveSlot"];
                        playerData.character = (int)row["player_character"];
                        playerData.damageLevel = (int)row["player_damageLevel"];
                        playerData.healthLevel = (int)row["player_healthLevel"];
                        gameData.Add(playerData);
                    }
                }

                if(gameData.Count > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Player's Data Retrieved!";
                    response.dataChanged = null;
                    response.gamesData = gameData;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Player not found!";
                    response.dataChanged = null;
                    response.gamesData = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.dataChanged = null;
                response.gamesData = null;
            }

            return response;
        }

        public ServerResponse saveGameData(SqlConnection connection, PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "update dbo.PlayerData set player_character = @character, player_damageLevel = @damageLevel, player_healthLevel = @healthLevel" +
                               " where player_nickName = @nickName and player_saveSlot = @saveSlot";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@character", playerData.character);
                command.Parameters.AddWithValue("@damageLevel", playerData.damageLevel);
                command.Parameters.AddWithValue("@healthLevel", playerData.healthLevel);
                command.Parameters.AddWithValue("@nickName", playerData.nickName);
                command.Parameters.AddWithValue("@saveSlot", playerData.saveSlot);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    response.statusCode = 200;
                    response.statusMessage = "Game Data Saved!";
                    response.dataChanged = playerData;
                    response.gamesData = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail Saving Game Data!";
                    response.dataChanged = null;
                    response.gamesData = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.dataChanged = null;
                response.gamesData = null;
            }

            return response;
        }

        public ServerResponse deleteGameData(SqlConnection connection, PlayerData playerData)
        {
            ServerResponse response = new ServerResponse();

            try
            {
                connection.Open();

                string query = "update dbo.PlayerData set player_character = 0, player_damageLevel = 0, player_healthLevel = 0" +
                               " where player_nickName = @nickName and player_saveSlot = @saveSlot";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nickName", playerData.nickName);
                command.Parameters.AddWithValue("@saveSlot", playerData.saveSlot);

                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    playerData.character = 0;
                    playerData.damageLevel = 0;
                    playerData.healthLevel = 0;

                    response.statusCode = 200;
                    response.statusMessage = "Game Data Deleted!";
                    response.dataChanged = playerData;
                    response.gamesData = null;
                }
                else
                {
                    response.statusCode = 100;
                    response.statusMessage = "Fail Deleting Game Data!";
                    response.dataChanged = null;
                    response.gamesData = null;
                }

                connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                response.statusCode = ex.ErrorCode;
                response.statusMessage = ex.Message;
                response.dataChanged = null;
                response.gamesData = null;
            }

            return response;
        }
    }
}