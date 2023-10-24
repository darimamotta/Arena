using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using ArenaProject;

namespace ArenaProject
{
    public class FileSimulationBuilder : ISimulationBuilder
    {
        public FileSimulationBuilder()
        {
        }

        private List<Team> CreateTeams()
        {
            List<Team> result = new List<Team>();
  
            using (StreamReader reader = new StreamReader("./files/teams.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(' ');
                    string title = parts[0];
                    string teamCol = parts[1];
                    Team team = new Team(title,teamCol);                    
                    result.Add(team);
                }
            }
            return result;
        }
      
        public Simulation Build()
        {     
            List<Team> teams = CreateTeams();           
      
            List<Fighter> fighters = CreateFighters(teams);
            Arena arena = CreateMap(fighters);
            BFSPathFinder pathFinder = new BFSPathFinder();
            Simulation simulation = new Simulation();
            ArenaVisualizer visualizer  = new ArenaVisualizer();           
            simulation.Arena = arena;
            simulation.Fighters = fighters;
            simulation.Teams = teams;
            simulation.PathFinder = pathFinder;
            simulation.Visualizer = visualizer;
            
            return simulation;
            
        }
        private List<Fighter> CreateFighters(List<Team> teams)
            
        {
            List<Fighter> result = new List<Fighter>();
            using (StreamReader reader = new StreamReader("./files/fighters.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] arr1 = line.Split(' ');
                    char label = arr1[0][0];
                    int attack = int.Parse(arr1[1]);
                    int protection = int.Parse(arr1[2]);
                    int armor = int.Parse(arr1[3]);
                    int damage = int.Parse(arr1[4]);
                    int health = int.Parse(arr1[5]);                    
                    string team = arr1[6];
                    string color =arr1[7];                    
                    Fighter figther = new Fighter(label, attack, protection, armor, damage, health,team,color);
                    foreach(Team t in teams)
                    {
                        if (t.Name == team)
                        {
                            t.AddFighter(figther);
                            break;
                        }
                    } 
                    result.Add(figther);                        

                }
                return result;

            }            
        }

        private Arena CreateMap(List<Fighter> fighters)

        {
            using (StreamReader reader = new StreamReader("./files/map.txt"))
            {
                int width = int.Parse( reader.ReadLine());
                int height = int.Parse( reader.ReadLine());
                Arena arena = new Arena(width, height);
                for (int i = 0;i<height;i++)
                { 
                    string mapline = reader.ReadLine();
                    for (int j = 0; j < mapline.Length; j++)
                    {
                        if (mapline[j] == '#')
                            arena.GetCell(i, j).Obstacle = new Obstacle();

                        if (mapline[j] != ' ')
                            for (int k = 0; k < fighters.Count; k++)
                                if (fighters[k].Label == mapline[j])
                                {
                                    arena.GetCell(i, j).Fighter = fighters[k];
                                    fighters[k].Position = new Position(i, j);
                                }
                            
                    }

                }
                return arena;
                
            }

                     
        }


    }
}
