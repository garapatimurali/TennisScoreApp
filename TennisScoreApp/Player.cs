using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisScoreApp
{
    public class Player
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? GameScore { get; set; } = "0";
        public int SetsWon { get; set; } = 0;
    }
}
