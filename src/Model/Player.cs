using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.src.Model
{
    public struct Player
    {
        public int ID { get; }
        public int Score { get; set; }
        public Player(int ID, int Score)
        {
            this.ID = ID;
            this.Score = Score;
        }
    }
}
