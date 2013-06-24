using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            #warning YES YOU HAVE TO
            var yourKey = "";

            var consumer = new CSharp.IPInfoDB.ApiConsumer(yourKey);

            var model = consumer.GetLocation("8.8.8.8");

            if (model.statusCode == "ERROR")
                Console.WriteLine(model.statusMessage);
            else
                Console.WriteLine(model.cityName + ", " + model.countryName);

            Console.Read();
        }
    }
}
