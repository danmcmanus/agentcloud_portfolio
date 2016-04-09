using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureHealth
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Name= "Dan";
            person.Age= 33;
            person.SelectedPolicy= new Policy();

            Console.WriteLine(person.Name + person.Age);
        }
    }
}
