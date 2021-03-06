﻿using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace PacMan
{
    /**
     * Grab the pellets as fast as you can!
     **/
    public class Game
    {
        static void Main(string[] args)
        {
            var gameGrid = new Grid();

            // game loop
            while (true)
            {
                var inputs = Console.ReadLine().Split(' ');
                int myScore = int.Parse(inputs[0]);
                int opponentScore = int.Parse(inputs[1]);

                var pacs = GetPacs();
                var myPac = pacs.Find(p => p.IsMine);

                var pellets = GetPellets();
                var superPelletes = pellets.Where(p => p.NumberOfEarnedPoints > 1);

                var theBestCoordinate = FindTheBestCoordinate(myPac.Coordinates, superPelletes.Select(sp => sp.Coordinates));
                if (theBestCoordinate == null)
                {
                    theBestCoordinate = FindTheBestCoordinate(myPac.Coordinates, pellets.Select(sp => sp.Coordinates));
                }

                Console.WriteLine($"MOVE {myPac.ID} {theBestCoordinate.Abscissa} {theBestCoordinate.Ordinate}"); // MOVE <pacId> <x> <y>

            }
        }

        public static Coordinates FindTheBestCoordinate(Coordinates currentCoordinates, IEnumerable<Coordinates> listOfPossibleCoordinates)
        {
            var wishedCoordinate = listOfPossibleCoordinates.FirstOrDefault();
            int theBestDistance;
            try
            {
                theBestDistance = currentCoordinates.GetDistance(wishedCoordinate);
            }
            catch (Exception)
            {
                return null;
            }
            
            foreach (var theBestCoordinate in listOfPossibleCoordinates)
            {
                var distance = currentCoordinates.GetDistance(theBestCoordinate);
                if(distance < theBestDistance)
                {
                    theBestDistance = distance;
                    wishedCoordinate = theBestCoordinate;
                }
            }

            return wishedCoordinate;
        }

        private static List<Pellet> GetPellets()
        {
            var pellets = new List<Pellet>();
            int visiblePelletCount = int.Parse(Console.ReadLine()); // all pellets in sight
            for (int i = 0; i < visiblePelletCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                var pellet = new Pellet
                {
                    Coordinates = new Coordinates
                    {
                        Abscissa = int.Parse(inputs[0]),
                        Ordinate = int.Parse(inputs[1])
                    },
                    NumberOfEarnedPoints = int.Parse(inputs[2]) // amount of points this pellet is worth
                };
                pellets.Add(pellet);
            }
            return pellets;
        }

        private static List<Pac> GetPacs()
        {
            int visiblePacCount = int.Parse(Console.ReadLine()); // all your pacs and enemy pacs in sight
            var pacs = new List<Pac>();
            for (int i = 0; i < visiblePacCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                var pac = new Pac
                {
                    ID = int.Parse(inputs[0]), // pac number (unique within a team)
                    IsMine = inputs[1] != "0", // true if this pac is yours
                    Coordinates = new Coordinates
                    {
                        Abscissa = int.Parse(inputs[2]), // position in the grid
                        Ordinate = int.Parse(inputs[3]) // position in the grid
                    },
                    TypeID = inputs[4], // unused in wood leagues
                    SpeedTurnsLeft = int.Parse(inputs[5]), // unused in wood leagues
                    AbilityCooldown = int.Parse(inputs[6]) // unused in wood leagues
                };
                pacs.Add(pac);
            }
            return pacs;
        }
    }
    class Grid
    {
        public Grid()
        {
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            Width = int.Parse(inputs[0]); // size of the grid
            Height = int.Parse(inputs[1]); // top left corner is (x=0, y=0)
            for (int i = 0; i < Height; i++)
            {
                string row = Console.ReadLine(); // one line of the grid: space " " is floor, pound "#" is wall
                Rows.Add(row);
            }
        }
        public int Width { get; set; }

        public int Height { get; set; }

        public List<string> Rows { get; set; } = new List<string>();
    }
    class Pac
    {
        public int ID { get; set; }

        public bool IsMine { get; set; }

        public Coordinates Coordinates { get; set; } = new Coordinates();

        public string TypeID { get; set; }

        public int SpeedTurnsLeft { get; set; }

        public int AbilityCooldown { get; set; }

        public override string ToString()
        {
            return $"ID: {ID} IsMine: {IsMine} " +
                $"\r\t {Coordinates}" +
                $"\r TypeID: {TypeID} " +
                $"\r SpeedTurnsLeft: {SpeedTurnsLeft} " +
                $"\r AbilityCooldown: {AbilityCooldown}";
        }
    }
    class Pellet
    {
        public Coordinates Coordinates { get; set; } = new Coordinates();

        public int NumberOfEarnedPoints { get; set; }

        public override string ToString()
        {
            return $"{Coordinates} NumberOfEarnedPoint: {NumberOfEarnedPoints}";
        }
    }
    public class Coordinates
    {
        public int Ordinate { get; set; }

        public int Abscissa { get; set; }

        public override string ToString()
        {
            return $"Coordinates : {Abscissa};{Ordinate}";
        }

        public int GetDistance(Coordinates otherCoordinates)
        {
            var abscisseDiff = otherCoordinates.Abscissa - Abscissa;
            var unsignAbscisseDiff = (abscisseDiff < 0) ? abscisseDiff * -1 : abscisseDiff;
            var ordinateDiff = otherCoordinates.Ordinate - Ordinate;
            var unsignAOrdinateDiff = (ordinateDiff < 0) ? ordinateDiff * -1 : ordinateDiff;
            return unsignAbscisseDiff + unsignAOrdinateDiff;
        }
    }
}