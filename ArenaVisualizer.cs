using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class ArenaVisualizer
    {
        List<string> messages = new List<string>();       
       
        public void Draw(Simulation simulation)
        {
            for (int i = 0; i < simulation.Arena.Height; i++)
            {
                for (int j = 0; j < simulation.Arena.Width; j++)
                {
               

                    if (simulation.Arena.GetCell(i, j).Obstacle!=null)
                    {
                        Console.Write("#");
                    }
                    else if (simulation.Arena.GetCell(i, j).Fighter != null)                       
                    {
                        string col = simulation.Arena.GetCell(i, j).Fighter!.Color;
                        if (!simulation.Arena.GetCell(i, j).Fighter!.IsAlive)
                        {                                                                                
                            Console.BackgroundColor = ConsoleColor.Red;
                            //Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col, true);
                            //Console.Write(simulation.Arena.GetCell(i, j).Fighter.Label);
                        } 
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col, true);
                        Console.Write(simulation.Arena.GetCell(i, j).Fighter!.Label);                        
                        Console.ResetColor();                       
                    }
                    else 
                    {
                         Console.Write(" ");
                    }    

                }
                Console.WriteLine();          
                
                
            }
            foreach (var item in messages)
                {
                    Console.WriteLine(item);

                }
            foreach(var fighter in simulation.Fighters)
            {
                string col = fighter.Color;
                if (fighter.IsAlive)
                { 
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col, true); 
                    Console.WriteLine($"Fighter {fighter.Label} has health {fighter.Health}");
                }
                else
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col, true); ;
                    Console.BackgroundColor = ConsoleColor.Red;
                    //Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), col, true);
                    Console.WriteLine($"Fighter {fighter.Label} has health 0");
                }
                
                Console.ResetColor();
            }    
        }
        public void AddMessage(string msg)
        {
           messages.Add(msg);
        }
        public void Clear()
        {
           messages.Clear();
        }
    }
}
