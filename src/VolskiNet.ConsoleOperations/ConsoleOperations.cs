namespace VolskSoft.Bibliotheca.ConsoleOperations
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

                actionKey = System.Console.ReadKey().Key;
                System.Console.Clear();
                if (actionKey.Equals(settings.BasicControls.QuitKey))
                {
                    continue;
                }

                var key = actionKey;
                var operation = GetOperation(o => o.ActivationKey.Equals(key));

                if (operation != null)
                {
                    operation.Action();
                }

            } while (!actionKey.Equals(settings.BasicControls.QuitKey));
        }

        public void DisplayOperations()
        {
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine(string.Format("--{0}--", settings.AppName));
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine("Available actions: ");

            foreach (var operation in operations)
            {
                System.Console.WriteLine(string.Format("{0} - {1}", operation.ActivationKey, operation.Name));
            }

            System.Console.WriteLine(string.Format("Press {0} to exit.", settings.BasicControls.QuitKey));
        }

        public void SetOperations(Operations operationList) =>
            operations.AddRange(operationList);

        public Operation GetOperation(Func<Operation, bool> predicate)
        {
            return operations.FirstOrDefault(predicate);
        }
    }
}