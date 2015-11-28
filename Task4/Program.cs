using System;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new UserRepository();

            for (var i = 0; i < 1000; i++)
            {
                var index = i;
                Task.Run(async () => await repo.CreateAsync(new User { Name = "Vova" + index }));
            }

            for (var i = 0; i < 1000; i += 2)
            {
                var index = i;
                Task.Run(async () => await repo.DeleteAsync(index));
            }

            var task = Task.Run(async () => await repo.GetAll());

            foreach (var user in task.Result)
            {
                Console.WriteLine("UserId: {0}, Name:{1}", user.UserId, user.Name);
            }

            var taskToUpdateUser =
                Task.Run(async () => await repo.UpdateAsync(new User { UserId = 333, Name = "Valodze4ka333" }));

            if (taskToUpdateUser.Result)
            {
                var taskToGetOneById = Task.Run(async () => await repo.GetByIdAsync(333));

                Console.WriteLine("UserId: {0}, Name:{1}", taskToGetOneById.Result.UserId, taskToGetOneById.Result.Name);
            }

            Console.ReadLine();
        }
    }
}
