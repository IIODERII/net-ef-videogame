using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_ef_videogame
{
    public static class VideogameManager
    {
        public static void AddGame(string gameName, string gameDescription, DateTime gameDate, long gameSoftId)
        {
            using VideogameContext db = new VideogameContext();
            Videogame nuovoGioco = new Videogame
            {
                Name = gameName,
                Description = gameDescription,
                Release = gameDate,
                SoftwareHouseID = gameSoftId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            db.Add(nuovoGioco);
            db.SaveChanges();
            Console.WriteLine("Gioco aggiunto con successo");
        }

        public static void GetGameById(int id)
        {
            using VideogameContext db = new VideogameContext();
            Videogame gioco = db.Videogames.Where(v => v.Id == id).Include("Softwarehouse").First();

            Console.WriteLine($"\nNome: {gioco.Name}");
            Console.WriteLine($"Descrizione: {gioco.Description}");
            Console.WriteLine($"Data di rilascio: {gioco.Release.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Software House: {gioco.Softwarehouse.Name}\n");
        }

        public static List<Videogame> GetGamesByName(string name)
        {
            using VideogameContext db = new VideogameContext();
            

            List<Videogame> giochi = db.Videogames.Where(v => v.Name.Contains(name)).Include("Softwarehouse").ToList();

            return giochi;
        }

        public static void DeleteGameById(int id)
        {
            using VideogameContext db = new VideogameContext();

            Videogame gioco = db.Videogames.Where(v => v.Id == id).First();
            db.Remove(gioco);
            db.SaveChanges();
        }

        public static void AddHouse(string houseName, string houseCode, string houseCity, string houseCountry)
        {
            using VideogameContext db = new VideogameContext();
            SoftwareHouse nuovaCasa = new SoftwareHouse
            {
                Name = houseName,
                Code = houseCode,
                City = houseCity,
                Country = houseCountry,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            db.Add(nuovaCasa);
            db.SaveChanges();
        }

        public static List<Videogame> GetGamesByHouseId(int id)
        {
            using VideogameContext db = new VideogameContext();
            return db.Videogames.Where(s => s.SoftwareHouseID == id).ToList();

        }
    }
}
