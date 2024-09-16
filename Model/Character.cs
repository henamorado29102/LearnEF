using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEF.Model
{
    public class Character
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public Backpack Backpack { get; set; }


        public List<Weapon> Weapons { get; set; }

        public List<Faction> factions { get; set; }
    }

  }