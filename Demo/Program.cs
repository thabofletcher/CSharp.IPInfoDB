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
            var yourKey = "e48b1398b3e13eae50dc1b7963aaf53abde69dc8d3ca746f9b28433485aee963";

            var consumer = new CSharp.IPInfoDB.ApiConsumer(yourKey, true);

            var model = consumer.GetCity("63.75.190.1");
            //var model = consumer.GetCountry("::1");

            if (model.statusCode == "ERROR")
                Console.WriteLine(model.statusMessage);
            else
                Console.WriteLine(model.cityName);

            Console.Read();
        }
    }
}
