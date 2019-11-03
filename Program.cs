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

class Program
{
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

    static void Main(string[] args)
    {
        var randomCreator = new RandomCreator();
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                if (o.CreateDict)
                {
                    Directory.GetCurrentDirectory();
                    File.WriteAllText("dict_otp.json", JsonConvert.SerializeObject(randomCreator.CreateDictionary()));
                }
                else if (o.Encrypt)
                {
                    var encrypt = new Encrypt(args[1], args[3]);
                    encrypt.EncryptWithSecretKey();
                }
                else if (o.Decrypt)
                {
                    var decrypt = new Decrypt(args[1], args[3]);
                    decrypt.DecryptPhrase();
                }
                else if (o.CreateKey)
                {
                    randomCreator.CreateKey(args[1]);
                }
            });

    }
}