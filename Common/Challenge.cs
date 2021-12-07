using System;
using System.ComponentModel;
using System.IO;

namespace AdventOfCode2021.Common {
    internal abstract class Challenge {
        public readonly string[] input;

        public Challenge(bool testInput) {
            var filename = Path.Combine(this.GetType().Name, testInput?Constants.DefaultExampleFileName:Constants.DefaultFileName);

            if (input is null) {
                input = File.ReadAllLines(filename);
            }
        }
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
