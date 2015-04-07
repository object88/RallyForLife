using System;
using LifeLogic;
using LifeLogic.Exceptions;
using LifeLogic.Factories;
using LifeLogic.IO;
using StructureMap;

namespace Life
{
    class Program
    {
        static void Main(string[] args)
        {
            if (null == args || 1 != args.Length)
            {
                Console.Out.WriteLine("Life expects one parameter, a path to a file containing a regular grid of");
                Console.Out.WriteLine("ones and zeros.  Life is finicky like that.");
                Console.Out.WriteLine();
                Console.Out.WriteLine("Life was brought to you by with the musical assistance of Front 242.");
                return;
            }

            var container = new Container(_ => _.For<IFactory>().Use<Factory>().Singleton());

            IState s;
            try
            {
                using (FileLoader fl = new FileLoader(args[0]))
                {
                    IStateReader sr = container.GetInstance<IFactory>().CreateReader(fl.Open());
                    s = sr.Read();
                }
            }
            catch (FileAccessException fae)
            {
                Console.Out.WriteLine("Life could not access the file located at:");
                Console.Out.WriteLine(args[0]);
                return;
            }

            IStateWriter sw = container.GetInstance<IFactory>().CreateWriter(Console.Out);
            IStateEngine se = container.GetInstance<IFactory>().CreateEngine();

            sw.Write(s);
            Console.Out.WriteLine();

            se.Iterate(s);
            
            sw.Write(s);
            Console.Out.WriteLine();
        }
    }
}
