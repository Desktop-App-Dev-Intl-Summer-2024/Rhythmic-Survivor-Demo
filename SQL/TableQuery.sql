CREATE TABLE dbo.PlayerData(
player_id INT NOT NULL,
player_nickName VARCHAR(25) NOT NULL,
player_email VARCHAR(50) NOT NULL,
player_password VARCHAR(50) NOT NULL,
player_saveSlot INT NOT NULL DEFAULT(0),
player_character INT NOT NULL DEFAULT(0),
player_damageLevel INT NOT NULL DEFAULT(0),
player_healthLevel INT NOT NULL DEFAULT(0)
);