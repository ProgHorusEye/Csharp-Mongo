using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Mongoessai
{
    class Person
    {
        public ObjectId id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<string> Interests { get; set; }


        public Person()
        {
        }
    }
}
