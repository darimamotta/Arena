using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Team
    {
        private List<Fighter> fighters;
       
        public string Name { get; private set; }
        public string Color { get;set; }

        public Team(string name,string color)
        {
            Name = name;
            Color = color;
            this.fighters = new List<Fighter>();
        } 
        
        public void AddFighter(Fighter fighter)
        {
            fighters.Add(fighter);
            fighter.Team = this;
        }
        public void RemoveFighter(Fighter fighter)
        {
            fighters.Remove(fighter);
            fighter.Team = null;
        }
            
    }
}
