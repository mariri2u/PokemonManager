using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonManager
{
	public struct Damage
	{
		public int MinDmg;
		public int MaxDmg;
	}

	public enum Weather
	{
		Sunny, Rainy, Snowy, Stormy
	}

	public class Field
	{
		public Pokemon AtkPokemon { get; set; }
		public Pokemon DefPokemon { get; set; }
		public Weather Weather { get; set; }
		public bool Critical { get; set; }

		public Damage Attack(Skill s)
		{
			return CalcDmg(s);
		}

		public Damage Deffence(Skill s)
		{
			return CalcDmg(s);
		}

		private Damage CalcDmg(Skill s)
		{
			Damage damage = new Damage();
			int power;
			double sameType, typeScale1, typeScale2, criScale;
			double itemTypeScale, itemSkillScale;
			sameType = 
				(AtkPokemon.Type1 == s.Type || AtkPokemon.Type2 == s.Type) ?
				1.5 : 1.0;
			// てきおうりょく
			typeScale1 = s.Type.Scale(DefPokemon.Type1);
			typeScale2 = s.Type.Scale(DefPokemon.Type2);
			criScale = Critical ? 1.5 : 1.0;
			itemSkillScale = 1.0;
			power = (int)(s.Power * itemSkillScale);
			itemTypeScale = 1.0;
			for (int i = 0; i < 2; i++)
			{
				int atk, def;
				int dmg;
				double randomScale;
				if (i == 0)
				{
					// 最大ダメージ
					atk = s.IsPhysic ?
						AtkPokemon.MaxRealOffsetSt[(int)Status.A] :
						AtkPokemon.MaxRealOffsetSt[(int)Status.C];
					def = s.IsPhysic ?
						DefPokemon.MinRealOffsetSt[(int)Status.B] :
						DefPokemon.MinRealOffsetSt[(int)Status.D];
					randomScale = 1.0;
				}
				else
				{
					// 最小ダメージ
					atk = s.IsPhysic ?
						AtkPokemon.MinRealOffsetSt[(int)Status.A] :
						AtkPokemon.MinRealOffsetSt[(int)Status.C];
					def = s.IsPhysic ?
						DefPokemon.MaxRealOffsetSt[(int)Status.B] :
						DefPokemon.MaxRealOffsetSt[(int)Status.D];
					randomScale = 0.85;
				}
				dmg = (AtkPokemon.Lv * 2 / 5 + 2) * power * atk / def / 50 + 2;
				dmg = (int)(dmg * criScale);
				dmg = (int)(dmg * randomScale);
				dmg = (int)(dmg * sameType);
				dmg = (int)(dmg * typeScale1);
				dmg = (int)(dmg * typeScale2);
				dmg = (int)(dmg * itemTypeScale);
				if (i == 0) { damage.MaxDmg = dmg; }
				else { damage.MinDmg = dmg; }
			}
			return damage;
		}
	}
}
