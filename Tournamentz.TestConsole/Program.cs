namespace Tournamentz.TestConsole
{
    using DAL;
    using DAL.Core;
    using DAL.Entity;
    using DAL.Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Testing DAL...");

            ApplicationUser user;

            using (IUnitOfWork uow = new UnitOfWork(new TournamentzModelContext()))
            {
                ApplicationUserManager manager = new ApplicationUserManager(uow);

                user = manager.Find("admin", "adminadmin");
                uow.Commit();
            }

            using (IUnitOfWork uow = new UnitOfWork(new TournamentzModelContext()))
            {
                TodoEntry entry1 = new TodoEntry { Name = "Prvi TODO", Difficulty = 4 };
                TodoEntry entry2 = new TodoEntry { Name = "Drugi TODO", Difficulty = 3 };
                TodoEntry entry3 = new TodoEntry { Name = "Treći TODO", Difficulty = 1 };

                IRepository<TodoEntry> todoRepo = new TrackedEntityRepository<TodoEntry, ApplicationUser>(uow, user);

                todoRepo.Insert(entry1);
                todoRepo.Insert(entry2);
                todoRepo.Insert(entry3);

                entry3.Name = "Treći TODO (ispravak)";
                todoRepo.Update(entry3);

                List<TodoEntry> todoEntries = todoRepo.Query.ToList();

                foreach (TodoEntry entry in todoEntries)
                {
                    Console.WriteLine($"{entry.Id} - {entry.Name}, difficulty = {entry.Difficulty}");
                }
                uow.Commit();
            }

            using (IUnitOfWork uow = new UnitOfWork(new TournamentzModelContext()))
            {
                IRepository<TodoEntry> todoRepo = uow.Repository<TodoEntry>();

                List<Guid> entries = todoRepo.Query
                    .Select(t => t.Id)
                    .ToList();

                foreach (Guid entry in entries)
                {
                    todoRepo.Delete(entry);
                }
                uow.Commit();
            }

            Console.WriteLine("DAL test success.");
            Console.ReadKey();
        }
    }
}