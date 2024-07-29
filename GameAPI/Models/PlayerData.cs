namespace GameAPI.Models
{
    public class PlayerData
    {
        public int id { get; set; } = 0;
        public string nickName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int saveSlot { get; set; } = 0;
        public int character { get; set; } = 0;
        public int damageLevel { get; set; } = 0;
        public int healthLevel { get; set; } = 0;
    }
}
