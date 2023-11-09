using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ArenaProject
{
    public abstract class Fighter
    {
       
        private char label;

        private int attack;
        private int protection;
        private int armor;
        private int damage;
        private int health;
        private const int BasicChance = 40;
        private const int MinDamage = 30;
        
        private int maxHealth;
        private static Random random = new Random();
        
        private string color;


        public Fighter(
            
            char label,
            int attack,
            int protection,
            int armor,
            int damage,
            int health,            
            string color

        )
        {
           
            this.label = label;
            this.attack = attack;
            this.protection = protection;
            this.armor = armor;
            this.damage = damage;
            this.health = health;
            this.maxHealth = health;            
            this.color = color;
        }

      
        public char Label { get { return label; } }
        public int Attack { get { return attack; } }
        public string Color { get { return color; } }
        public int Protection { get { return protection; } }
        public int Armor { get { return armor; } }
        public int Damage { get { return damage; } }
        public int Health { get { return health; } }
        public int MaxHealth { get { return maxHealth; } }
        public const int MaxChanceValue = 100;
        public const int MinChanceValue = 5;
        public Team? Team { get; set; }
        public Position Position { get; set; } = null!;
        public bool IsAlive => Health > 0;

        public bool AttackOtherFighter(Fighter target)
        {
            if (CheckAttackWithChance(target))
            {
                target.ApplyDamage(CalculateDamage(target));
                return true;   
            }
            return false;            
        }
        

      
        private int CalculateChance(Fighter target)
        {            
            int chance = Attack - target.Protection + BasicChance;
            if (chance < MinChanceValue)
                chance = MinChanceValue;          
            return chance;
        }
        private bool CheckAttackWithChance(Fighter target)
        {        
            return random.Next(MaxChanceValue) < CalculateChance(target);
        }



        private int CalculateDamage(Fighter target)
        {
            int calcDamage = (Damage - target.Armor);
            if (calcDamage < MinDamage)
                calcDamage = MinDamage;
            return calcDamage;        
        }

        private void ApplyDamage(int damage)
        {
            health -= damage;
        }
        public abstract Fighter? ChooseTarget(Simulation simulation);

        public abstract void Move(Simulation simulation);
        
        
    }
}

