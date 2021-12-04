using System;
using System.ComponentModel;

namespace AdventOfCode2021.Common {
    internal abstract class Challenge {
        public abstract string FirstResult();
        public abstract string SecondResult();
        public void Run() {
            Console.WriteLine(this.GetType().Name);
            Console.WriteLine($"1* = {FirstResult()}");
            Console.WriteLine($"2* = {SecondResult()}");
            Console.WriteLine();
        }
    }
}
