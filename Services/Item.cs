using SQLite;

namespace Campuscloset.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Email { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
