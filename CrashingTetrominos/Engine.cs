using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashingTetrominos
{
    internal class Engine
    {
        public Boolean IsGameover { get; set; }
        public int RandomNumberInteger { get; set; }

        public int RandomNumber()
        {
            Random random = new Random();
            RandomNumberInteger = random.Next(0, 100);
            return RandomNumberInteger;
        }

        public Engine()
        {
        }

        public void Initialize()
        {
            IsGameover = false;
        }
    }    
}
