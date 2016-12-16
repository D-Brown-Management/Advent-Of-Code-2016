using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1CS
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentHeading = Direction.North;
            var input = File.ReadAllText("input.txt");            
            var instrArray = input.Split(',');
            var currentPoint = new Location
            {
                x = 0,
                y = 0
            };

            var visitHistory = new List<Location>();
            visitHistory.Add(currentPoint);
            foreach (string instruction in instrArray)
            {
                var oldLocation = currentPoint;
                var trim = instruction.Trim();
                var direction = trim.Substring(0, 1);
                var len = int.Parse(trim.Substring(1));
                //Console.Write($"{trim}:");
                var newHeading = GetNewHeading(currentHeading, direction);
                //Console.WriteLine($"New Direction: {newHeading} / Len: {len}");
                currentHeading = newHeading;
                var newLocation = GetNewLocation(currentPoint, currentHeading, len);
                //Console.WriteLine($"New Position: X:{newLocation.x} Y:{newLocation.y}");
                currentPoint = newLocation;
                LogLocations(oldLocation, newLocation, visitHistory);
                //visitHistory.Add(currentPoint);
            }

            var mag = Math.Abs(currentPoint.x) + Math.Abs(currentPoint.y);
            Console.WriteLine($"Total Length: {mag}");
           
        }

        static Direction GetNewHeading(Direction currentHeading, string turnDirection)
        {
            Direction newHeading;
            if (turnDirection == "R")
            {
                if (currentHeading == Direction.West)
                {
                    newHeading = Direction.North;
                }
                else
                {
                    newHeading = currentHeading + 1;
                }
                
            }
            else
            {
                if (currentHeading == Direction.North)
                {
                    newHeading = Direction.West;
                }
                else
                {
                    newHeading = currentHeading - 1;
                }
            }
            return newHeading;
        }

        static Location GetNewLocation(Location currentLocation, Direction currentHeading, int length)
        {
            Location newLocation = currentLocation;
            switch (currentHeading)
            {
                case Direction.North:
                    newLocation.y += length;
                    break;
                case Direction.East:
                    newLocation.x += length;
                    break;
                case Direction.South:
                    newLocation.y -= length;
                    break;
                case Direction.West:
                    newLocation.x -= length;
                    break;
            }

            return newLocation;
        }

        static void LogLocations(Location oldLocation, Location newLocation, List<Location> locationLog)
        {            
            if (oldLocation.x != newLocation.x)
            {
                int numX;
                if (oldLocation.x > newLocation.x)
                {                    
                    for (int i = oldLocation.x-1; i > newLocation.x-1; i--)
                    {
                        var loc = new Location() {x = i, y = oldLocation.y};
                        DupeCheck(loc, locationLog);
                        locationLog.Add(loc);
                    }
                }
                else
                {
                    for (int i = oldLocation.x + 1; i < newLocation.x + 1; i++)
                    {
                        var loc = new Location() {x = i, y = oldLocation.y};
                        DupeCheck(loc, locationLog);
                        locationLog.Add(loc);
                    }
                }                
            }
            else
            {
                int numY;
                if (oldLocation.y > newLocation.y)
                {
                    for (int i = oldLocation.y - 1; i > newLocation.y - 1; i--)
                    {
                        var loc = new Location() {x = oldLocation.x, y = i};
                        DupeCheck(loc, locationLog);
                        locationLog.Add(loc);
                    }
                }
                else
                {
                    for (int i = oldLocation.y + 1; i < newLocation.y + 1; i++)
                    {
                        var loc = new Location() {x = oldLocation.x, y = i};
                        DupeCheck(loc, locationLog);
                        locationLog.Add(loc);
                    }
                }
            }
            
            locationLog.Add(newLocation);
        }

        static void DupeCheck(Location loc, List<Location> locLog)
        {
            if (locLog.Contains(loc) && !DupeFound)
            {
                //Console.WriteLine("First Hit!");
                var mag = Math.Abs(loc.x) + Math.Abs(loc.y);
                
                Console.WriteLine($"First Dupe Length: {mag}");
                DupeFound = true;
                //Console.ReadLine();
            }
        }
        enum Direction
        {
            North,
            East,
            South,
            West
        }

        public static bool DupeFound = false;        
        struct Location
        {
            public int x;
            public int y;
        }
    }
}
