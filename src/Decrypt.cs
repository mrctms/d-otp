/* Copyright (C) mrctms <mrctms@protonmail.com>

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
            var keyValue = dotpDictionary.FirstOrDefault(x => x.Value == i);
            if (!keyValue.Equals(default(KeyValuePair<char, int>))) decryptedPhrase.Add(keyValue.Key);
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
