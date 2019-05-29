using MRS.Data;
using System;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MRSContext context = new MRSContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Console.WriteLine("Ready");
            }
        }
    }
}
