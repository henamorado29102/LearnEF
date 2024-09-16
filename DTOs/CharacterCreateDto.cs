using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEF.DTOs
{
    public record struct CharacterCreateDto(
        string Name, 
        BackpackCreateDto Backpack, 
        List<WeaponCreateDto> Weapons,
        List<FactionCreateDto> Factions);
}