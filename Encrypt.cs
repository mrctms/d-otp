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

public class Encrypt
{
    Dictionary<char, int> DotpDictionary
    {
        get;
        set;
    }

    public Encrypt(string phrase, string key)
    {
        EncryptWithSecretKey(key, GetPhraseEncrypted(phrase));
    }

    private Dictionary<char, int> DeserializeDictionaryFromFile()
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
            Console.WriteLine("\nERROR: dict_otp.json not found\n");
            Environment.Exit(1);
            return null;
        }


    }

    private List<int> GetPhraseEncrypted(string phrase)
    {
        var translatedPhrase = new List<int>();
        var reversedPhrase = new List<int>();
        DotpDictionary = DeserializeDictionaryFromFile();
        try
        {
            phrase.ToList().ForEach(x => reversedPhrase.Add(DotpDictionary[x]));
            reversedPhrase.ForEach(e => translatedPhrase.Add(e));
            var splitTranslatedPhrase = string.Join("", translatedPhrase).ToList().ConvertAll(x => int.Parse(x.ToString()));
            return splitTranslatedPhrase;
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("\nERROR: Only letters in lowercase and space tab are permitted\n");
            return null;
        }
    }

    private void EncryptWithSecretKey(string key, List<int> translatedPhrase)
    {
        var secretKey = key.Select(x => int.Parse(x.ToString())).ToList();
        var encryptedPhraseWithKey = new List<int>();

        foreach (var i in Enumerable.Range(0, key.Length))
        {
            int result;
            var encryptPhrase = translatedPhrase[i] + secretKey[i];

            if (encryptPhrase <= 9)
            {
                result = encryptPhrase;
            }
            else
            {
                result = encryptPhrase - 10;
            }

            encryptedPhraseWithKey.Add(result);
        }

        Console.WriteLine($"\nEncrypted phrase: {string.Join("", encryptedPhraseWithKey)}\n");
    }

}