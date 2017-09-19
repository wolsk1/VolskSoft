namespace VolskiNet.ConsoleOperations
{
    using System;
    using System.Linq;

    public class ConsoleOperations
    {
        private readonly Operations operations;
        private readonly ApplicationSettings settings;

        public ConsoleOperations(ApplicationSettings settings)
        {
            this.settings = settings;
            operations = new Operations();
        }

        public void Run()
        {
            ConsoleKey actionKey;
            do
            {
                DisplayOperations();

                actionKey = Console.ReadKey().Key;
                Console.Clear();
                if (actionKey.Equals(settings.BasicControls.QuitKey))
                {
                    continue;
                }

                var key = actionKey;
                var operation = GetOperation(o => o.ActivationKey.Equals(key));
                operation?.Action();

            } while (!actionKey.Equals(settings.BasicControls.QuitKey));
        }

        public void DisplayOperations()
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine($"--{settings.AppName}--");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Available actions: ");

            foreach (var operation in operations)
            {
                Console.WriteLine($"{operation.ActivationKey} - {operation.Name}");
            }

            Console.WriteLine($"Press {settings.BasicControls.QuitKey} to exit.");
        }

        public void SetOperations(Operations operationList) =>
            operations.AddRange(operationList);

        public Operation GetOperation(Func<Operation, bool> predicate)
        {
            return operations.FirstOrDefault(predicate);
        }
    }
}