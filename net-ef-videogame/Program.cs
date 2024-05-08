using Microsoft.EntityFrameworkCore;

namespace net_ef_videogame
{    public class TitoloVuotoException : Exception
    {
        public string Message => "Non puoi lasciare il titolo Vuoto";
    }
    public class DescrizioneVuotaException : Exception
    {
        public string Message => "Non puoi lasciare la descrizione vuota";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creiamo quindi una console app che all'avvio mostra un menu con la possibilità di :
            //> 1 inserire un nuovo videogioco
            //> 2 ricercare un videogioco per id
            //> 3 ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input
            //> 4 cancellare un videogioco
            //> 5 chiudere il programma

            int choise = 0;
            while (choise != 6)
            {

                Console.WriteLine("\nBenvenuto nel DataBase dei Videogiochi, scegli cosa vuoi fare: ");
                Console.WriteLine("\t1. Inserire un nuovo VideoGame");
                Console.WriteLine("\t2. Cercare un videogame per ID");
                Console.WriteLine("\t3. Cercare dei videogiochi per nome");
                Console.WriteLine("\t4. Eliminare un videogioco da id");
                Console.WriteLine("\t5. Aggiungere una nuova software house");
                Console.WriteLine("\t6. Cercare tutti i videoGames prodotti da una software house");
                Console.WriteLine("\t6. Chiudi il programma");

                Console.Write("\nInserire il numero relativo all'azione che si vuole utilizzare: ");
                choise = int.Parse(Console.ReadLine());
                if (choise == 1)
                {
                    var going = true;
                    string gameName = "";
                    string gameDescription = "";
                    DateTime gameDate = DateTime.Now;
                    int gameSoftId = 0;
                    while (going)
                    {
                        try
                        {
                            Console.Write("\nInserire Titolo del videogioco: ");
                            gameName = Console.ReadLine();
                            if (gameName == "")
                                throw new TitoloVuotoException();
                            Console.Write("Inserire la descrizione: ");
                            gameDescription = Console.ReadLine();
                            if (gameDescription == "")
                                throw new DescrizioneVuotaException();
                            Console.Write("Inserire la data di rilascio (gg/mm/aaaa): ");
                            gameDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Inserire l'ID della software house: ");
                            gameSoftId = int.Parse(Console.ReadLine());

                            going = false;
                        }
                        catch (TitoloVuotoException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DescrizioneVuotaException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
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
                else if (choise == 2)
                {
                    using VideogameContext db = new VideogameContext();
                    Console.Write("Inserisci l'Id che vuoi cercare: ");
                    int input = int.Parse(Console.ReadLine());

                    Videogame gioco = db.Videogames.Where(v => v.Id == input).Include("Softwarehouse").First();

                    Console.WriteLine($"\nNome: {gioco.Name}");
                    Console.WriteLine($"Descrizione: {gioco.Description}");
                    Console.WriteLine($"Data di rilascio: {gioco.Release.ToString("dd/MM/yyyy")}");
                    Console.WriteLine($"Software House: {gioco.Softwarehouse.Name}\n");
                }
                else if (choise == 3)
                {
                    using VideogameContext db = new VideogameContext();
                    Console.Write("Inserisci il nome che vuoi cercare: ");
                    string input = Console.ReadLine();

                    List<Videogame> giochi = db.Videogames.Where(v => v.Name.Contains(input)).Include("Softwarehouse").ToList();

                    foreach(var gioco in giochi)
                    {
                        Console.WriteLine($"\nNome: {gioco.Name}");
                        Console.WriteLine($"Descrizione: {gioco.Description}");
                        Console.WriteLine($"Data di rilascio: {gioco.Release.ToString("dd/MM/yyyy")}");
                        Console.WriteLine($"Software House: {gioco.Softwarehouse.Name}\n");
                    }
                }
                else if (choise == 4)
                {
                    using VideogameContext db = new VideogameContext();
                    Console.Write("Inserisci l'Id del videogame che si deve eliminare: ");
                    int input = int.Parse(Console.ReadLine());

                    Videogame gioco = db.Videogames.Where(v => v.Id == input).First();
                    db.Remove(gioco);
                    db.SaveChanges();
                }
                else if (choise == 5)
                {
                    var going = true;
                    string houseName = "";
                    string houseCode = "";
                    string houseCity = "";
                    string houseCountry = "";

                    while (going)
                    {
                        try
                        {
                            Console.Write("\nInserire Titolo della software house: ");
                            houseName = Console.ReadLine();
                            if (houseName == "")
                                throw new TitoloVuotoException();
                            Console.Write("Inserire il codice: ");
                            houseCode = Console.ReadLine();
                            if (houseCode == "")
                                throw new DescrizioneVuotaException();
                            Console.Write("Inserire la città della software house: ");
                            houseCity = Console.ReadLine();
                            if (houseCity == "")
                                throw new TitoloVuotoException();
                            Console.Write("Inserire il Paese della software house: ");
                            houseCountry = Console.ReadLine();
                            if (houseCountry == "")
                                throw new DescrizioneVuotaException();

                            going = false;
                        }
                        catch (TitoloVuotoException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DescrizioneVuotaException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
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
                    Console.WriteLine("Software house aggiunta con successo");
                }
                else if(choise == 6)
                {
                    using VideogameContext db = new VideogameContext();
                    Console.Write("Inserisci l'Id della software house: ");
                    int input = int.Parse(Console.ReadLine());

                    List<Videogame> giochi = db.Videogames.Where(s => s.SoftwareHouseID == input).ToList();

                    foreach(var g in giochi)
                    {
                        Console.WriteLine($"\nNome: {g.Name}");
                        Console.WriteLine($"Descrizione: {g.Description}");
                        Console.WriteLine($"Data di rilascio: {g.Release.ToString("dd/MM/yyyy")}");
                    }
                }
                else if (choise == 7)
                {
                    Console.WriteLine("Grazie per aver utilizzato il mio programma :)");
                }
                else
                {
                    Console.WriteLine("Input non valido.\n");
                }
            }

        }
    }


}
