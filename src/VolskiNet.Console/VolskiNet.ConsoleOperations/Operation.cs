namespace VolskiNet.ConsoleOperations
{
    using System;

    public class Operation
    {
        public ConsoleKey ActivationKey { get; set; }
        public string Name { get; set; }
        public Action Action { get; set; }
    }
}