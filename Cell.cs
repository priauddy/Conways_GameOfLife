using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Conways_Game_of_Life
{
    internal class Cell
    {
        //IsAlive Backing field
        private bool isAlive;

        //Constructor
        public Cell(bool NewIsAlive)
        {
            this.isAlive = NewIsAlive;
        }

        //Properties
        public bool IsAlive
        {
            get
            {
                return this.isAlive;
            }
            set
            {
                this.isAlive = value;
            }

        }
    }
}
