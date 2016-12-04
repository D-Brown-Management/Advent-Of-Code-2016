using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day4CS
{
    public class PartOne
    {
        private Regex roomRegex = new Regex("(?<room>[a-z\\-]+)(?:-(?<sector>\\d+))\\[(?<checksum>[a-z]+)\\]");
        public PartOne()
        {
            
        }

        public bool ValidateRoom(string roomNumber)
        {
            var things = new Dictionary<char, int>();
            for (int i = 97; i < 123; i++)
            {
                things.Add((char)i, 0);
            }
            
            var matchInfo = roomRegex.Match(roomNumber);
            var parsedRoom = matchInfo.Groups["room"].Value.Replace("-","");

            for (int i = 0; i < parsedRoom.Length; i++)
            {
                things[parsedRoom[i]]++;
            }

            var obd = things.OrderByDescending(t => t.Value).ThenBy(t=>t.Key).Select(s=>s.Key).Take(5);
            var checksum = string.Join(null, obd);

            return string.Compare(checksum, matchInfo.Groups["checksum"].Value) == 0;
        }

        public int GetSector(string roomNumber)
        {            
            var matchInfo = roomRegex.Match(roomNumber);
            var parsedRoom = matchInfo.Groups["room"].Value.Replace("-", "");

            return int.Parse(matchInfo.Groups["sector"].Value);
        }

        public string DecodeSector(string input)
        {
            var matchInfo = roomRegex.Match(input);
            var parsedRoom = matchInfo.Groups["room"].Value.Replace("-", "").ToCharArray();
            int thing = int.Parse(matchInfo.Groups["sector"].Value);
            for(int i=0; i<parsedRoom.Length; i++)
            {
                int ch = parsedRoom[i];
                for (int j = 0; j < thing; j++)
                {
                    if (ch == 122)
                    {
                        ch = 97;
                    }
                    else
                    {
                        ch++;
                    }
                }

                parsedRoom[i] = (char) ch;

            }

            return string.Join(null, parsedRoom);
        }
    }
}
