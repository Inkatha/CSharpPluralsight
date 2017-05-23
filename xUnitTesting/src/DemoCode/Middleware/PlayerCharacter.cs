using System;
using System.Collections.Generic;

namespace DemoCode.Middleware
{
    public class PlayerCharacter
    {
        public int Health { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public bool IsNoob { get; set; }
        public List<string> Weapons { get; set; }

        public PlayerCharacter()
        {
            Name = GenerateName();

            IsNoob = true;

            CreateStartingWeapons();
        }

        private string GenerateName()
        {
            var names = new[]
            {
                "John",
                "Peter",
                "Chris",
                "Shalnorr",
                "G'Toth'Lop",
                "Boldrakteethtop"
            };

            return names[new Random().Next(0, names.Length)];
        }

        private void CreateStartingWeapons()
        {
            
        }
    }
}