using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LearnEF.Data;
using LearnEF.DTOs;
using LearnEF.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LearnEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "WeatherData";



        private readonly IConfiguration _configuration;
        public GameController(DataContext dataContext, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _cache = memoryCache;
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(CharacterCreateDto request)
        {
            var newCharacter = new Character
            {
                Name = request.Name,
            };
            var backpack = new Backpack { Description = request.Backpack.Description, Character = newCharacter };
            var weapons = request.Weapons.Select(w => new Weapon { Name = w.Name, Character = newCharacter }).ToList();
            var factions = request.Factions.Select(f => new Faction { Name = f.Name, characters = new List<Character> { newCharacter } }).ToList();
            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.factions = factions;
            _dataContext.Characters.Add(newCharacter);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Characters.Include(c => c.Backpack).Include(c => c.Weapons).ToListAsync());
        }

        [HttpGet()]
        public async Task<ActionResult<Character>> GetCharacter()
        {

            _dataContext.ChangeTracker.LazyLoadingEnabled = true;
            var weapons = CompileQuery(_dataContext).ToList();
            return Ok(await _dataContext.Characters.AsNoTracking().Include(f => f.factions).ToListAsync());
            // return Ok(await _dataContext.Database
            //     .SqlQuery<Employee>($@"select * from Employees where Id = 1")
            //     .FirstOrDefaultAsync());
        }

        [HttpGet("compileQuery")]
        public ActionResult<WeaponCreateDto> GetWeapons()
        {

            _dataContext.ChangeTracker.LazyLoadingEnabled = true;
            var weapons = CompileQuery(_dataContext).ToList();
            // return Ok(await _dataContext.Characters.AsNoTracking().Include(f => f.factions).FirstOrDefaultAsync());
            return Ok(weapons);
        }

        [HttpGet("cache")]
        public IActionResult GetWeather()
        {
            // Try to get data from the cache
            if (!_cache.TryGetValue(CacheKey, out string cachedData))
            {
                // Simulate fetching data (e.g., from a database or API)
                cachedData = $"Weather data generated at {DateTime.Now}";

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(4)) // Cache will expire if not accessed in 30 seconds
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10)); // Cache will expire after 1 minute

                // Save data in cache
                _cache.Set(CacheKey, cachedData, cacheEntryOptions);
            }

            // Return cached data
            return Ok(cachedData);
        }


        [HttpGet("test")]
        public ActionResult<Character> GetTest()
        {

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("DatabaseConnectionString");

                connection.Open();
                // Create a SQL command object.
                string sql =
                "SELECT * FROM Employees where Id in (1,2,3,4,5,6,7,8,9);";
                SqlCommand myCommand = new(sql, connection);
                // Obtain a data reader a la ExecuteReader().
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    // Loop over the results.
                    while (myDataReader.Read())
                    {
                        Console.WriteLine($"-> Name: {myDataReader["Name"]}, Salary: {myDataReader
                       ["Salary"]}, Id: {myDataReader["Id"]}.");
                    }
                }
            }
            Console.ReadLine();
            return Ok(new Character { Name = "", });
        }
        //         {
        //           "email": "henamorado29102@gmail.com",
        //           "password": "PerroSucio.29102"
        //         }

        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Ok(new { message = "You have been logged out successfully." });
        }


        [HttpGet("me")]
        //  [Authorize]
        public async Task<ActionResult<User>> GetMe()
        {
            var user = User;
            if (!user.Identity.IsAuthenticated)
            {
                return Unauthorized("You must be logged in to access this resource.");
            }

            // Extract specific claims, for example:
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get user ID from claims
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
            return Ok(await _dataContext.Users.FindAsync(userId));
        }

        private static readonly Func<DataContext, IEnumerable<WeaponCreateDto>> CompileQuery =
        EF.CompileQuery(
                 (DataContext context) =>
                    context.Weapons
                    .Where(weapon => weapon.CharacterId == 1)
                    .Select(w => new WeaponCreateDto { Name = w.Name }));
    }
}