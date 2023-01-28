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

public class Encrypt
{
    public string Phrase { get; set; }
    public string Key { get; set; }

    public string DictionaryFilePath { get; set; }



    public string EncryptWithSecretKey()
    {
        var secretKey = Key.Select(x => int.Parse(x.ToString())).ToList();
        var encryptedPhraseWithKey = new List<int>();

        List<int> translatedPhrase = GetPhraseEncrypted();

        foreach (var i in Enumerable.Range(0, Key.Length))
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
        return string.Join("", encryptedPhraseWithKey);
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

    private List<int> GetPhraseEncrypted()
    {
        var translatedPhrase = new List<int>();
        var reversedPhrase = new List<int>();
        try
        {
            var DotpDictionary = DeserializeDictionaryFromFile();
            Phrase.ToList().ForEach(x => reversedPhrase.Add(DotpDictionary[x]));
            reversedPhrase.ForEach(e => translatedPhrase.Add(e));
            var splitTranslatedPhrase = string.Join("", translatedPhrase).ToList().ConvertAll(x => int.Parse(x.ToString()));
            return splitTranslatedPhrase;
        }
        catch (KeyNotFoundException)
        {
            throw new Exception("\nERROR: Only letters in lowercase and space tab are permitted\n");
        }
    }


}