using System.Text.Json.Serialization;


namespace LearnEF.Model
{
    public class Backpack
    {
        public int Id { get; set; }

        public required string Description { get; set; }

        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}