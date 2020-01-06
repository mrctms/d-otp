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
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Decrypt
{

    public string DictionaryFilePath { get; set; }
    public string Phrase { get; set; }
    public string Key { get; set; }

    public string DecryptPhrase()
    {
        var decryptedPhrase = new List<char>();
        var dotpDictionary = DeserializeDictionaryFromFile();
        var translatedPhrase = GetTranslatedPhrase();

        //maybe a regex is better
        var splitTranslatedPhrase = Enumerable.Range(0, string.Join("", translatedPhrase).Length)
            .GroupBy(x => x / 2)
            .Select(x => new string(x.Select(y => string.Join("", translatedPhrase)[y]).ToArray()))
            .ToList().Select(z => int.Parse(z)).ToList();

        foreach (var i in splitTranslatedPhrase)
        {
            if (dotpDictionary['a'] == i) decryptedPhrase.Add('a');
            if (dotpDictionary['b'] == i) decryptedPhrase.Add('b');
            if (dotpDictionary['c'] == i) decryptedPhrase.Add('c');
            if (dotpDictionary['d'] == i) decryptedPhrase.Add('d');
            if (dotpDictionary['e'] == i) decryptedPhrase.Add('e');
            if (dotpDictionary['f'] == i) decryptedPhrase.Add('f');
            if (dotpDictionary['g'] == i) decryptedPhrase.Add('g');
            if (dotpDictionary['h'] == i) decryptedPhrase.Add('h');
            if (dotpDictionary['i'] == i) decryptedPhrase.Add('i');
            if (dotpDictionary['j'] == i) decryptedPhrase.Add('j');
            if (dotpDictionary['k'] == i) decryptedPhrase.Add('k');
            if (dotpDictionary['l'] == i) decryptedPhrase.Add('l');
            if (dotpDictionary['m'] == i) decryptedPhrase.Add('m');
            if (dotpDictionary['n'] == i) decryptedPhrase.Add('n');
            if (dotpDictionary['o'] == i) decryptedPhrase.Add('o');
            if (dotpDictionary['p'] == i) decryptedPhrase.Add('p');
            if (dotpDictionary['q'] == i) decryptedPhrase.Add('q');
            if (dotpDictionary['r'] == i) decryptedPhrase.Add('r');
            if (dotpDictionary['s'] == i) decryptedPhrase.Add('s');
            if (dotpDictionary['t'] == i) decryptedPhrase.Add('t');
            if (dotpDictionary['u'] == i) decryptedPhrase.Add('u');
            if (dotpDictionary['v'] == i) decryptedPhrase.Add('v');
            if (dotpDictionary['w'] == i) decryptedPhrase.Add('w');
            if (dotpDictionary['x'] == i) decryptedPhrase.Add('x');
            if (dotpDictionary['y'] == i) decryptedPhrase.Add('y');
            if (dotpDictionary['z'] == i) decryptedPhrase.Add('z');
            if (dotpDictionary[' '] == i) decryptedPhrase.Add(' ');
        }
        return string.Join("", decryptedPhrase);
    }



    private Dictionary<char, int> DeserializeDictionaryFromFile()
    {        
        try
        {
            var dictionaryFile = File.ReadAllText(DictionaryFilePath);
            var dictionaryFileDeserialized = JsonConvert.DeserializeObject<Dictionary<char, int>>(dictionaryFile);
            return dictionaryFileDeserialized;
        }
        catch (FileNotFoundException)
        {
            throw new Exception($"\nERROR: {DictionaryFilePath} not found\n");
        }

    }

    private List<string> GetTranslatedPhrase()
    {
        var translatedPhrase = new List<string>();
        var secretKey = Key.Select(x => int.Parse(x.ToString())).ToList();
        try
        {
            var encryptedPhrase = Phrase.Select(x => int.Parse(x.ToString())).ToList();

            foreach (var i in Enumerable.Range(0, Key.Length))
            {
                int result;
                var decryptPhrase = encryptedPhrase[i] - secretKey[i];

                if (decryptPhrase >= 0)
                {
                    result = decryptPhrase;
                }
                else
                {
                    result = decryptPhrase + 10;
                }
                translatedPhrase.Add(result.ToString());
            }
            return translatedPhrase;
        }
        catch (KeyNotFoundException)
        {
            throw new Exception("\nERROR: Only numbers are permitted\n");
        }
    }

}
