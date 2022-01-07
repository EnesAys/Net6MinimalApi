using Net6MinimalApi.Models;

namespace Net6MinimalApi.Services
{
    public class JerseyService : IJerseyService
    {
        public List<Jersey> Jerseys { get; set; }
        public JerseyService()
        {
            Jerseys = new List<Jersey> {
                new Jersey{
                    Id = 12,
                    Player = new Player{
                        Id = 30,
                        Name = "Ersin Destanoglu"
                    }
                },
                new Jersey{
                    Id = 3,
                    Player = new Player{
                        Id = 2,
                        Name = "Ridvan Yilmaz"
                    }
                },
                new Jersey{
                    Id = 15,
                    Player = new Player{
                        Id = 3,
                        Name = "Miralem Pjanic"
                    }
                }
            };
        }

        public List<Jersey> GetAll() => Jerseys;
        public Jersey Get(int id) => Jerseys.Find(x => x.Id == id);

        public int Insert(Jersey jersey)
        {
            Jerseys.Add(jersey);
            var insertedJersey = Get(jersey.Id);

            return insertedJersey != null ? insertedJersey.Id : 0;
        }
        public Jersey Update(int id, string playerName)
        {
            var jersey = Get(id);
            if (jersey == null)
                return null;

            jersey.Player.Name = playerName;
            return jersey;
        }
        public bool Delete(int id)
        {
            var jersey = Get(id);
            if (jersey == null)
                return false;

            return Jerseys.Remove(jersey);
        }
    }
}
