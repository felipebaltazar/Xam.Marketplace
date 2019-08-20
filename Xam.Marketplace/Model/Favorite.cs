using SQLite;

namespace Xam.Marketplace.Model
{
    [Table(nameof(Favorite))]
    public sealed class Favorite
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
