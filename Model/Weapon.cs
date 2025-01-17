using System.Text.Json.Serialization;

namespace LearnEF.Model
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set;} = string.Empty;
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}