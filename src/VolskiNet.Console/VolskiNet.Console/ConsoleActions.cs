namespace VolskiNet.Console
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class ConsoleActions
    {
        public static ConsoleKey SelectKey(
            IList<KeyDescription> desiredKeys,
            string promtText,
            string errorText)
        {
            ConsoleKey selectedKey;
            do
            {
                Console.WriteLine(promtText);

                foreach (var desiredKey in desiredKeys)
                {
                    Console.WriteLine($"{desiredKey.Key} - {desiredKey.Description}");
                }

                selectedKey = Console.ReadKey().Key;
                Console.Clear();
                if (desiredKeys.Any(k => k.Key.Equals(selectedKey)))
                {
                    break;
                }

                Console.WriteLine();
            } while (!desiredKeys.Any(k => k.Key.Equals(selectedKey)));

            return selectedKey;
        }
    }
}