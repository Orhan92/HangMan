using System;
using System.Collections.Generic;
using System.Text;

namespace OwnProjects.Utilities
{
    class Player
    {
        public string Name { get; set; }
        public int Points { get; private set; }

        public Player (string name, int points)
        {
            Name = name;
            Points = points;
        }
    }
}
