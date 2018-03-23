namespace VolskSoft.Bibliotheca.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                    Console.WriteLine(string.Format("{0} - {1}", desiredKey.Key, desiredKey.Description));
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