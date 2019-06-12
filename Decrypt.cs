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

public class Decrypt : IDictionary
{
    Dictionary<char, int> DotpDictionary
    {
        get;
        set;
    }

    public Decrypt(string phrase, string key)
    {
        DecryptPhrase(phrase, key);
    }

    public Dictionary<char, int> DeserializeDictionaryFromFile()
    {
        Directory.GetCurrentDirectory();
        try
        {
            var dictionaryFile = File.ReadAllText("dict_otp.json");
            var dictionaryFileDeserialized = JsonConvert.DeserializeObject<Dictionary<char, int>>(dictionaryFile);
            return dictionaryFileDeserialized;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("\n\ndict_otp.json not found\n");
        }

    }

    private List<string> GetTranslatedPhrase(string phrase, string key)
    {
        var translatedPhrase = new List<string>();
        var secretKey = key.Select(x => int.Parse(x.ToString())).ToList();
        try
        {
            var encryptedPhrase = phrase.Select(x => int.Parse(x.ToString())).ToList();

            foreach (var i in Enumerable.Range(0, key.Length))
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
            throw new KeyNotFoundException("\n\nOnly numbers are permitted\n");
        }
    }

    private void DecryptPhrase(string phrase, string key)
    {
        var decryptedPhrase = new List<char>();
        DotpDictionary = DeserializeDictionaryFromFile();
        var translatedPhrase = GetTranslatedPhrase(phrase, key);

        //maybe a regex is better
        var splitTranslatedPhrase = Enumerable.Range(0, string.Join("", translatedPhrase).Length)
            .GroupBy(x => x / 2)
            .Select(x => new string(x.Select(y => string.Join("", translatedPhrase)[y]).ToArray()))
            .ToList().Select(z => int.Parse(z)).ToList();

        //ugly part, PR welcome.
        //(https://github.com/MarckTomack/d-otp)
        #region Ugly part
        foreach (var i in splitTranslatedPhrase)
        {
            if (DotpDictionary['a'] == i) decryptedPhrase.Add('a');
            if (DotpDictionary['b'] == i) decryptedPhrase.Add('b');
            if (DotpDictionary['c'] == i) decryptedPhrase.Add('c');
            if (DotpDictionary['d'] == i) decryptedPhrase.Add('d');
            if (DotpDictionary['e'] == i) decryptedPhrase.Add('e');
            if (DotpDictionary['f'] == i) decryptedPhrase.Add('f');
            if (DotpDictionary['g'] == i) decryptedPhrase.Add('g');
            if (DotpDictionary['h'] == i) decryptedPhrase.Add('h');
            if (DotpDictionary['i'] == i) decryptedPhrase.Add('i');
            if (DotpDictionary['j'] == i) decryptedPhrase.Add('j');
            if (DotpDictionary['k'] == i) decryptedPhrase.Add('k');
            if (DotpDictionary['l'] == i) decryptedPhrase.Add('l');
            if (DotpDictionary['m'] == i) decryptedPhrase.Add('m');
            if (DotpDictionary['n'] == i) decryptedPhrase.Add('n');
            if (DotpDictionary['o'] == i) decryptedPhrase.Add('o');
            if (DotpDictionary['p'] == i) decryptedPhrase.Add('p');
            if (DotpDictionary['q'] == i) decryptedPhrase.Add('q');
            if (DotpDictionary['r'] == i) decryptedPhrase.Add('r');
            if (DotpDictionary['s'] == i) decryptedPhrase.Add('s');
            if (DotpDictionary['t'] == i) decryptedPhrase.Add('t');
            if (DotpDictionary['u'] == i) decryptedPhrase.Add('u');
            if (DotpDictionary['v'] == i) decryptedPhrase.Add('v');
            if (DotpDictionary['w'] == i) decryptedPhrase.Add('w');
            if (DotpDictionary['x'] == i) decryptedPhrase.Add('x');
            if (DotpDictionary['y'] == i) decryptedPhrase.Add('y');
            if (DotpDictionary['z'] == i) decryptedPhrase.Add('z');
            if (DotpDictionary[' '] == i) decryptedPhrase.Add(' ');
            #endregion
        }
        Console.WriteLine($"\nDecrypted phrase: {string.Join("", decryptedPhrase)}\n");
    }

}