using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace Mongoessai
{
    class Program
    {
        /// <summary>
        /// créer une person en static
        /// </summary>
        static Person staticPerson = new Person()
        {
            FirstName = "Ivan",
            LastName = "Kleiber",
            Age = 37,
            Interests = new List<string>() { "Photo","Natation" }
        };

        /// <summary>
        /// Crée une liste de personne
        /// </summary>
        static List<Person> staticPeople = new List<Person>()
        {
        new Person() {FirstName = "Kleiber", LastName="Georges", Age = 45, Interests = new List<string>(){"Natation","Photo" } },
        new Person() { FirstName = "Jean", LastName = "Tasse", Age = 32, Interests = new List<string>() { "Foot", "Brocante" } }
    };

        static void Main(string[] args)
        {
            

            MongoClient client = new MongoClient("mongodb://localhost:27017");

            IMongoDatabase db = client.GetDatabase("people");

            IMongoCollection<Person> collection = db.GetCollection<Person>("persons");

            db.DropCollection("persons"); //vide la base

            collection.InsertOne(staticPerson);

            collection.InsertMany(staticPeople);

           
           List<Person> result = collection.Find(person => person.FirstName == "Ivan").ToList();
           Console.WriteLine(result.Count().ToString() + " personnes trouvées pour le filtre");

            List<Person> resultall = collection.Find(x => true).ToList();

            Console.WriteLine(resultall.Count().ToString() + " Personnes trouvées");

            Person personne;

            foreach (Person p in resultall)
            {
                personne = p;
                Console.WriteLine(p.id + " : " + p.FirstName + " " + p.LastName + "(" + p.Age + ")" );
            }

            Console.ReadLine();

            personne = collection.Find(x => x.LastName == "Tasse").ToList().FirstOrDefault();


            var filter = Builders<Person>.Filter.Eq(x => x.LastName, "Tasse");

            var update = Builders<Person>.Update.Set(x => x.LastName, "Bonneau");

            collection.UpdateOne(filter, update);



            resultall = collection.Find(x => true).ToList();

            foreach (Person p in resultall)
            {
                personne = p;
                Console.WriteLine(p.id + " : " + p.FirstName + " " + p.LastName + "(" + p.Age + ")");
            }


            Console.ReadLine();
        }
    }
}
