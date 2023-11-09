using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;
using ArenaProject;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Drawing;
using System.Reflection.Emit;

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
                    string line = reader.ReadLine()!;
                    string[] parts = line.Split(' ');
                    string title = parts[0];
                    string teamCol = parts[1];
                    Team team = new Team(title, teamCol);
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
            ArenaVisualizer visualizer = new ArenaVisualizer();
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
                    string line = reader.ReadLine()!;
                    string[] arrt = line.Split(' ');
                    string type = arrt[0];
                    Fighter fighter =null!;
                    if (type == "a")
                    {
                        fighter = CreateArcher(teams, arrt);
                        
                    }
                    else if (type == "m")
                    {
                        fighter = CreateMillefighter(teams, arrt);                       

                    }
                    result.Add(fighter);
                }

            }return result;
            

        }

        private static MilleFighter CreateMillefighter(List<Team> teams, string[] arrt)
        {
            char label = arrt[1][0];
            int attack = int.Parse(arrt[2]);
            int protection = int.Parse(arrt[3]);
            int armor = int.Parse(arrt[4]);
            int damage = int.Parse(arrt[5]);
            int health = int.Parse(arrt[6]);
            string team = arrt[7];
            string color = arrt[8];
            MilleFighter millefighter = new MilleFighter(label, attack, protection, armor, damage, health, color);
            AssignFighterToTeam(teams, team, millefighter);
            return millefighter;
        }

        private static Archer CreateArcher(List<Team> teams, string[] arrt)
        {
            char label = arrt[1][0];
            int attack = int.Parse(arrt[2]);
            int protection = int.Parse(arrt[3]);
            int armor = int.Parse(arrt[4]);
            int damage = int.Parse(arrt[5]);
            int health = int.Parse(arrt[6]);
            string team = arrt[7];
            string color = arrt[8];
            double minAttackDistance = double.Parse(arrt[9]);
            double maxAttackDistance = double.Parse(arrt[10]);
            int retreatDistance = int.Parse(arrt[11]);
            int offensiveDistance = int.Parse(arrt[12]);
            Archer archer = new Archer(minAttackDistance, maxAttackDistance, retreatDistance, offensiveDistance, label, attack, protection, armor, damage, health, color);
            AssignFighterToTeam(teams, team, archer);
            return archer;
        }

        private static void AssignFighterToTeam(List<Team> teams, string team, Fighter fighter)
        {
            foreach (Team t in teams)
            {
                if (t.Name == team)
                {
                    t.AddFighter(fighter);
                    break;
                }

            }
           
        }

        private Arena CreateMap(List<Fighter> fighters)

        {
            using (StreamReader reader = new StreamReader("./files/map.txt"))
            {
                int width = int.Parse( reader.ReadLine()!);
                int height = int.Parse( reader.ReadLine()!);
                Arena arena = new Arena(width, height);
                for (int i = 0;i<height;i++)
                { 
                    string mapline = reader.ReadLine()!;
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
