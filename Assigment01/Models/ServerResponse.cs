namespace Assigment01.Models
{
    public class ServerResponse
    {
        public int statusCode { get; set; }
        public string statusMessage { get; set; }
        public ItemInfo item { get; set; }
        public List<ItemInfo> itemsCart { get; set; }
    }
}