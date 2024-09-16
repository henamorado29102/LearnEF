using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LearnEF.Model
{
    public class Faction
    {
        public int Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        [JsonIgnore]
        public List<Character> characters { get; set; } = new List<Character>();

        
    }
}