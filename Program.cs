using System;
using System.Linq;
using Cassandra;

namespace nosql_cassandra
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connect to the demo keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("teste");

            // Insert Bob
            session.Execute("insert into users (id,lastname, age, city, email, firstname) values (1,'Jones', 35, 'Austin', 'bob@example.com', 'Silvia')");
            session.Execute("insert into users (id,lastname, age, city, email, firstname) values (2,'Camila', 35, 'Austin', 'bob@example.com', 'Marcao')");
            session.Execute("insert into users (id,lastname, age, city, email, firstname) values (3,'Vinicius', 35, 'Austin', 'bob@example.com', 'Marley')");
            session.Execute("insert into users (id,lastname, age, city, email, firstname) values (4,'Jose', 35, 'Austin', 'bob@example.com', 'Pingo')");

            //Get Bob
            Row result = session.Execute("select * from users where lastname='Jones' ALLOW FILTERING").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            //update Bob
            session.Execute("update users set age = 36 where lastname = 'Jones'");
            result = session.Execute("select * from users where lastname='Jones'").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            //delete Bob
            session.Execute("delete from users where lastname = 'Jones'");

            //Select All
            RowSet rows = session.Execute("select * from users");
            foreach (Row row in rows)
                Console.WriteLine("{0} {1}", row["firstname"], row["age"]);


            Console.Read();

        }
    }
}
