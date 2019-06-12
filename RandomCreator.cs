/* Copyright (C) MarckTomack <marcktomack@tutanota.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>. */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class RandomCreator
{
    private readonly Random random = new Random();

    public void CreateKey(string length)
    {

        var key = new HashSet<int>();
        for (var i = 0; i < length.Length; i++)
        {
            while (key.Count != int.Parse(length))
            {
                key.Add(random.Next(10, 99));
            }

        }
        Console.WriteLine($"\nSecret Key: {string.Join("", key)}\n");
    }

    private List<int> CreateRandomNumberForDictionary()
    {
        var randomList = new List<int>();
        var tempHashSet = new HashSet<int>();
        for (var i = 0; i < 27; i++)
        {
            while (tempHashSet.Count != 27)
            {
                tempHashSet.Add(random.Next(10, 99));
            }

        }
        foreach (var i in tempHashSet)
        {
            randomList.Add(i);
        }
        return randomList;
    }

    public Dictionary<string, int> CreateDictionary()
    {
        var randomNumbGenerated = CreateRandomNumberForDictionary();

        var randomDictionary = new Dictionary<string, int>()
            { { "a", randomNumbGenerated[0] }, { "b", randomNumbGenerated[1] },
            { "c", randomNumbGenerated[2] }, { "d", randomNumbGenerated[3] },
            { "e", randomNumbGenerated[4] }, { "f", randomNumbGenerated[5] },
            { "g", randomNumbGenerated[6] }, { "h", randomNumbGenerated[7] },
            { "i", randomNumbGenerated[8] }, { "j", randomNumbGenerated[9] },
            { "k", randomNumbGenerated[10] }, { "l", randomNumbGenerated[11] },
            { "m", randomNumbGenerated[12] }, { "n", randomNumbGenerated[13] },
            { "o", randomNumbGenerated[14] }, { "p", randomNumbGenerated[15] },
            { "q", randomNumbGenerated[16] }, { "r", randomNumbGenerated[17] },
            { "s", randomNumbGenerated[18] }, { "t", randomNumbGenerated[19] },
            { "u", randomNumbGenerated[20] }, { "v", randomNumbGenerated[21] },
            { "w", randomNumbGenerated[22] }, { "x", randomNumbGenerated[23] },
            { "y", randomNumbGenerated[24] }, { "z", randomNumbGenerated[25] },
            { " ", randomNumbGenerated[26] }
            };

        return randomDictionary;
    }
}