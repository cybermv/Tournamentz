namespace Tournamentz.TestConsole
{
    using DAL;
    using DAL.Core;
    using DAL.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Testing DAL...");

            using (IUnitOfWork uow = new BasicUnitOfWork(new TournamentzModelContext()))
            {
                IRepository<Player> playersRepo = uow.Repository<Player>();

                List<Player> players = playersRepo.Query
                    .OrderBy(p => p.Nickname)
                    .Include(p => p.ApplicationUser.Roles)
                    .ToList();

                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.Nickname} - {player.Name} {player.Surname}");
                    if (player.ApplicationUser != null)
                    {
                        Console.WriteLine($"---> has account with e-mail {player.ApplicationUser.Email}");
                    }

                    Console.WriteLine();
                }

                uow.Commit();
            }

            Console.WriteLine("DAL test success.");
            Console.ReadKey();
        }
    }
}