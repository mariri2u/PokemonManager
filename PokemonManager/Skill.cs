using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonManager
{
	public class Skill
	{
		public int Name { get; set; }
		public int Power { get; set; }
		public Type Type { get; set; }
		public bool IsPhysic { get; set; }

		public static Skill Create(string n)
		{
			Skill s = new Skill();
			return s;
		}
	}
}
