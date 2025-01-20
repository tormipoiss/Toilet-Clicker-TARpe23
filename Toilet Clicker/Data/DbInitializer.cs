using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Data
{
    public enum LocationType
    {
        House, Hotel, Public, Forest, BlackHole, NeutronStar, Underwater, Office, SkibidiLand
    }

    public class DbInitializer
    {
        public static void Initialize(ToiletClickerContext _context)
        {
            if (_context.Locations.Any())
            {
                return;
            }
            var locations = new Location[]
            {
                new Location { LocationName = "TTHK", LocationDescription = "Suur kutsekool mis on varustatud tualetiga Markus.", LocationType = (Core.Domain.LocationType)LocationType.Public, LocationWasMade = new DateTime(2000, 01, 30, 22, 30, 00), CreatedAt = new DateTime(2000, 01, 30, 22, 30, 00) },
                new Location { LocationName = "Haigla", LocationDescription = "Väike haigla, kus saab terveks ühe päevaga.", LocationType = (Core.Domain.LocationType)LocationType.Public, LocationWasMade = new DateTime(1980, 06, 30, 22, 30, 00), CreatedAt = new DateTime(1980, 06, 30, 22, 30, 00) },
                new Location { LocationName = "Kontor", LocationDescription = "Töökoht, kus töötavad töötajad ja on varustatud arvutitega.", LocationType = (Core.Domain.LocationType)LocationType.Office, LocationWasMade = new DateTime(1995, 12, 30, 22, 30, 00), CreatedAt = new DateTime(1995, 12, 30, 22, 30, 00) },
                new Location { LocationName = "Mets", LocationDescription = "Vana mets, kus on seisnud kaua aega käimla nimega Hanku.", LocationType = (Core.Domain.LocationType)LocationType.Forest, LocationWasMade = new DateTime(0761, 08, 30, 22, 30, 00), CreatedAt = new DateTime(0761, 08, 30, 22, 30, 00) }
            };
            _context.Locations.AddRange(locations);
            _context.SaveChanges();


            if (_context.Toilets.Any())
            {
                return;
            }

            var toilets = new Toilet[] 
            {
                new Toilet { ToiletName = "Markus", ToiletWasBorn = new DateTime(2000, 01, 30, 22, 30, 00), CreatedAt = new DateTime(2000, 01, 30, 22, 30, 00), LocationID = _context.Locations.ElementAt(0).ID },
                new Toilet { ToiletName = "Liisu", ToiletWasBorn = new DateTime(1980, 06, 30, 22, 30, 00), CreatedAt = new DateTime(1980, 06, 30, 22, 30, 00), LocationID = _context.Locations.ElementAt(1).ID },
                new Toilet { ToiletName = "Miisu", ToiletWasBorn = new DateTime(1995, 12, 30, 22, 30, 00), CreatedAt = new DateTime(1995, 12, 30, 22, 30, 00), LocationID = _context.Locations.ElementAt(2).ID },
                new Toilet { ToiletName = "Hanku", ToiletWasBorn = new DateTime(1950, 08, 30, 22, 30, 00), CreatedAt = new DateTime(1950, 08, 30, 22, 30, 00), LocationID = _context.Locations.ElementAt(3).ID }
            };
            _context.Toilets.AddRange(toilets);
            _context.SaveChanges();
        }
    }
}
