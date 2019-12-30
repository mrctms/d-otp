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


using System.IO;
using CommandLine;
using Newtonsoft.Json;
using System;

class Program
{
    const string DictionaryFileName = "dict_otp.json";

    public class Options
    {
        [Option('e', "encrypt", Required = false, HelpText = "Phrase to encrypt")]
        public bool Encrypt { get; set; }

        [Option('k', "key", Required = false, HelpText = "The secret key")]
        public bool Key { get; set; }

        [Option('d', "decrypt", Required = false, HelpText = "The encrypted phrase")]
        public bool Decrypt { get; set; }

        [Option("ck", Required = false, HelpText = "Create a secret key")]
        public bool CreateKey { get; set; }

        [Option("cd", Required = false, HelpText = "Create a random dictionary")]
        public bool CreateDict { get; set; }
    }

    static string GetDictionaryFilePath()
    {
        string currentDir = Directory.GetCurrentDirectory();
        string dictionaryFilePath = Path.Combine(currentDir, DictionaryFileName);
        return dictionaryFilePath;
    }

    static void Main(string[] args)
    {
        var randomCreator = new RandomCreator();
        string dictionaryFilePath = GetDictionaryFilePath();
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                if (o.CreateDict)
                {
                    File.WriteAllText(dictionaryFilePath, JsonConvert.SerializeObject(randomCreator.CreateDictionary()));
                    Console.WriteLine($"Dictionary file created: {dictionaryFilePath}");
                }
                else if (o.Encrypt)
                {
                    try
                    {
                        var encrypt = new Encrypt()
                        {
                            Phrase = args[1],
                            Key = args[3],
                            DictionaryFilePath = dictionaryFilePath
                        };
                        string encryptedPhrase = encrypt.EncryptWithSecretKey();
                        Console.WriteLine($"\nEncrypted phrase: {encryptedPhrase}\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (o.Decrypt)
                {
                    try
                    {
                        var decrypt = new Decrypt()
                        {
                            Phrase = args[1],
                            Key = args[3],
                            DictionaryFilePath = dictionaryFilePath
                        };
                        string decryptedPhrase = decrypt.DecryptPhrase();
                        Console.WriteLine($"\nDecrypted phrase: {decryptedPhrase}\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                else if (o.CreateKey)
                {
                    string key = randomCreator.CreateKey(args[1]);
                    Console.WriteLine($"\nSecret Key: {key}\n");
                }
            });

    }
}