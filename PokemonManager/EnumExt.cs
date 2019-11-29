using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonManager
{
    #region Type
	public enum Type : int
	{
		None, Normal, Fire, Water, Electric, Leef,
		Ice, Fight, Poison, Earth, Fly, Esper, Insect,
		Stone, Ghost, Dragon, Evil, Steel
	}



    public static class TypeEnum
    {
		private static double[,] scaleTable = {
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,0.5,0,1,1,0.5},
			{1,1,0.5,0.5,1,2,2,1,1,1,1,1,2,0.5,1,0.5,1,2},
			{1,1,2,0.5,1,0.5,1,1,1,2,1,1,1,2,1,0.5,1,1},
			{1,1,1,2,0.5,0.5,1,1,1,0,2,1,1,1,1,0.5,1,1},
			{1,1,0.5,2,1,0.5,1,1,0.5,2,0.5,1,0.5,2,1,0.5,1,0.5},
			{1,1,0.5,0.5,1,2,0.5,1,1,2,2,1,1,1,1,2,1,0.5},
			{1,2,1,1,1,1,2,1,0.5,1,0.5,0.5,0.5,2,0,1,2,2},
			{1,1,1,1,1,2,1,1,0.5,0.5,1,1,1,0.5,0.5,1,1,0},
			{1,1,2,1,2,0.5,1,1,2,1,0,1,0.5,2,1,1,1,2},
			{1,1,1,1,0.5,2,1,2,1,1,1,1,2,0.5,1,1,1,0.5},
			{1,1,1,1,1,1,1,2,2,1,1,0.5,1,1,1,1,0,0.5},
			{1,1,0.5,1,1,2,1,0.5,0.5,1,0.5,2,1,1,0.5,1,2,0.5},
			{1,1,2,1,1,1,2,0.5,1,0.5,2,1,2,1,1,1,1,0.5},
			{1,0,1,1,1,1,1,1,1,1,1,2,1,1,2,1,0.5,0.5},
			{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,1,0.5},
			{1,1,1,1,1,1,1,0.5,1,1,1,2,1,1,2,1,0.5,0.5},
			{1,1,0.5,0.5,0.5,1,2,1,1,1,1,1,1,2,1,1,1,0.5}
		};


        public static Type Encode(string s)
        {
            Type type = Type.None;
            switch (s)
            {
                case "ドラゴン":
                    type = Type.Dragon;
                    break;
                case "じめん":
                    type = Type.Earth;
                    break;
                case "でんき":
                    type = Type.Electric;
                    break;
                case "エスパー":
                    type = Type.Esper;
                    break;
                case "あく":
                    type = Type.Evil;
                    break;
                case "かくとう":
                    type = Type.Fight;
                    break;
                case "ほのお":
                    type = Type.Fire;
                    break;
                case "ひこう":
                    type = Type.Fly;
                    break;
                case "ゴースト":
                    type = Type.Ghost;
                    break;
                case "こおり":
                    type = Type.Ice;
                    break;
                case "むし":
                    type = Type.Insect;
                    break;
                case "くさ":
                    type = Type.Leef;
                    break;
                case "ノーマル":
                    type = Type.Normal;
                    break;
                case "どく":
                    type = Type.Poison;
                    break;
                case "はがね":
                    type = Type.Steel;
                    break;
                case "いわ":
                    type = Type.Stone;
                    break;
                case "みず":
                    type = Type.Water;
                    break;
            }
            return type;
        }

        public static string Decode(this Type type)
        {
            string s = "";
            switch (type)
            {
                case Type.None:
                    s = "－";
                    break;
                case Type.Normal:
                    s = "ノーマル";
                    break;
                case Type.Fire:
                    s = "ほのお";
                    break;
                case Type.Water:
                    s = "みず";
                    break;
                case Type.Leef:
                    s = "くさ";
                    break;
                case Type.Electric:
                    s = "でんき";
                    break;
                case Type.Ice:
                    s = "こおり";
                    break;
                case Type.Fight:
                    s = "かくとう";
                    break;
                case Type.Poison:
                    s = "どく";
                    break;
                case Type.Earth:
                    s = "じめん";
                    break;
                case Type.Fly:
                    s = "ひこう";
                    break;
                case Type.Esper:
                    s = "エスパー";
                    break;
                case Type.Insect:
                    s = "むし";
                    break;
                case Type.Stone:
                    s = "いわ";
                    break;
                case Type.Ghost:
                    s = "ゴースト";
                    break;
                case Type.Dragon:
                    s = "ドラゴン";
                    break;
                case Type.Evil:
                    s = "あく";
                    break;
                case Type.Steel:
                    s = "はがね";
                    break;
            }
            return s;
        }

		public static string Decode1(this Type type)
		{
			string s = "";
			switch (type)
			{
				case Type.None:
					s = "－";
					break;
				case Type.Normal:
					s = "ノ";
					break;
				case Type.Fire:
					s = "炎";
					break;
				case Type.Water:
					s = "水";
					break;
				case Type.Leef:
					s = "草";
					break;
				case Type.Electric:
					s = "電";
					break;
				case Type.Ice:
					s = "氷";
					break;
				case Type.Fight:
					s = "闘";
					break;
				case Type.Poison:
					s = "毒";
					break;
				case Type.Earth:
					s = "地";
					break;
				case Type.Fly:
					s = "飛";
					break;
				case Type.Esper:
					s = "エ";
					break;
				case Type.Insect:
					s = "虫";
					break;
				case Type.Stone:
					s = "岩";
					break;
				case Type.Ghost:
					s = "ゴ";
					break;
				case Type.Dragon:
					s = "ド";
					break;
				case Type.Evil:
					s = "悪";
					break;
				case Type.Steel:
					s = "鋼";
					break;
			}
			return s;
		}

		public static double Scale(this Type type, Type type1, Type type2)
		{
			double scale = 1.0;
			scale *= scaleTable[(int)type, (int)type1];
			scale *= scaleTable[(int)type, (int)type2];
			return scale;
		}

		public static double Scale(this Type aType, Type dType)
		{
			return scaleTable[(int)aType, (int)dType];
		}


    }
    #endregion

    #region Status
    public enum Status : int
    {
        H, A, B, C, D, S
    }
    #endregion

    #region Persoanlity
    public enum Personality : int
    {
		none,
        sami, iji, yan, yuu, zubu, wan,
        nou, non, hika, otto, ukka, rei,
        oda, oto, shin, nama, oku, sekka,
        you, muja, gan, kima, suna, tere, maji
    }

    public static class PersonalityEnum
    {
        public static Personality Encode(string s)
        {
            Personality per = Personality.none;
            switch (s)
            {
                case "がんばりや":
                    per = Personality.gan;
                    break;
                case "ひかえめ":
                    per = Personality.hika;
                    break;
                case "いじっぱり":
                    per = Personality.iji;
                    break;
                case "きまぐれ":
                    per = Personality.kima;
                    break;
                case "まじめ":
                    per = Personality.maji;
                    break;
                case "むじゃき":
                    per = Personality.muja;
                    break;
                case "なまいき":
                    per = Personality.nama;
                    break;
                case "のんき":
                    per = Personality.non;
                    break;
                case "のうてんき":
                    per = Personality.nou;
                    break;
                case "おだやか":
                    per = Personality.oda;
                    break;
                case "おくびょう":
                    per = Personality.oku;
                    break;
                case "おとなしい":
                    per = Personality.oto;
                    break;
                case "おっとり":
                    per = Personality.otto;
                    break;
                case "れいせい":
                    per = Personality.rei;
                    break;
                case "さみしがり":
                    per = Personality.sami;
                    break;
                case "せっかち":
                    per = Personality.sekka;
                    break;
                case "しんちょう":
                    per = Personality.shin;
                    break;
                case "すなお":
                    per = Personality.suna;
                    break;
                case "てれや":
                    per = Personality.tere;
                    break;
                case "うっかりや":
                    per = Personality.ukka;
                    break;
                case "わんぱく":
                    per = Personality.wan;
                    break;
                case "やんちゃ":
                    per = Personality.yan;
                    break;
                case "ようき":
                    per = Personality.you;
                    break;
                case "ゆうかん":
                    per = Personality.yuu;
                    break;
                case "ずぶとい":
                    per = Personality.zubu;
                    break;
            }
            return per;
        }
        public static string Decode(this Personality per)
        {
            string s = "－";
            switch (per)
            {
                case Personality.gan:
                    s = "がんばりや";
                    break;
                case Personality.hika:
                    s = "ひかえめ";
                    break;
                case Personality.iji:
                    s = "いじっぱり";
                    break;
                case Personality.kima:
                    s = "きまぐれ";
                    break;
                case Personality.maji:
                    s = "まじめ";
                    break;
                case Personality.muja:
                    s = "むじゃき";
                    break;
                case Personality.nama:
                    s = "なまいき";
                    break;
                case Personality.non:
                    s = "のんき";
                    break;
                case Personality.nou:
                    s = "のうてんき";
                    break;
                case Personality.oda:
                    s = "おだやか";
                    break;
                case Personality.oku:
                    s = "おくびょう";
                    break;
                case Personality.oto:
                    s = "おとなしい";
                    break;
                case Personality.otto:
                    s = "おっとり";
                    break;
                case Personality.rei:
                    s = "れいせい";
                    break;
                case Personality.sami:
                    s = "さみしがり";
                    break;
                case Personality.sekka:
                    s = "せっかち";
                    break;
                case Personality.shin:
                    s = "しんちょう";
                    break;
                case Personality.suna:
                    s = "すなお";
                    break;
                case Personality.tere:
                    s = "てれや";
                    break;
                case Personality.ukka:
                    s = "うっかりや";
                    break;
                case Personality.wan:
                    s = "わんぱく";
                    break;
                case Personality.yan:
                    s = "やんちゃ";
                    break;
                case Personality.you:
                    s = "ようき";
                    break;
                case Personality.yuu:
                    s = "ゆうかん";
                    break;
                case Personality.zubu:
                    s = "ずぶとい";
                    break;
            }
            return s;
        }
        public static double Scale(this Personality per, Status st)
        {
			double scale = 1.0;
            double[,] scaleArray = {
                {1,1,1,1,1},
                {1.1,0.9,1,1,1},
                {1.1,1,0.9,1,1},
                {1.1,1,1,0.9,1},
                {1.1,1,1,1,0.9},
                {0.9,1.1,1,1,1},
                {1,1.1,0.9,1,1},
                {1,1.1,1,0.9,1},
                {1,1.1,1,1,0.9},
                {0.9,1,1.1,1,1},
                {1,0.9,1.1,1,1},
                {1,1,1.1,0.9,1},
                {1,1,1.1,1,0.9},
                {0.9,1,1,1.1,1},
                {1,0.9,1,1.1,1},
                {1,1,0.9,1.1,1},
                {1,1,1,1.1,0.9},
                {0.9,1,1,1,1.1},
                {1,0.9,1,1,1.1},
                {1,1,0.9,1,1.1},
                {1,1,1,0.9,1.1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1},
                {1,1,1,1,1}
            };
			scale = (st == Status.H) ? 1.0 : scaleArray[(int)per, (int)st - 1];
			return scale;
        }
		public static System.Drawing.Color Color(this Personality per, Status st){
			System.Drawing.Color color =
					(per.Scale(st) == 1.1) ? System.Drawing.Color.Red :
					(per.Scale(st) == 0.9) ? System.Drawing.Color.Blue :
					System.Drawing.Color.Black;
			return color;
		}
		public static System.Drawing.Color Color(this Personality per, int st)
		{
			return per.Color((Status)Enum.Parse(typeof(Status), st.ToString()));
		}
    }
    #endregion

	#region State
	public enum State : int
	{
		None, Poison, Fire, Freeze, Stop
	}
	public static class StateExt
	{
        public static State Encode(string s){
			State st = State.None;
			switch(s){
				case "－":
					st = State.None;
					break;
				case "どく":
					st =State.Poison;
					break;
				case "やけど":
					st =State.Fire;
					break;
				case "こおり":
					st =State.Freeze;
					break;
				case "まひ":
					st =State.Stop;
					break;
			}
			return st;
		}
		public static string Decode(this State st)
		{
			string s = "";
			switch (st)
			{
				case State.None:
					s = "－";
					break;
				case State.Fire:
					s = "やけど";
					break;
				case State.Freeze:
					s = "こおり";
					break;
				case State.Poison:
					s = "どく";
					break;
				case State.Stop:
					s = "まひ";
					break;
			}
			return s;
		}
	}
	#endregion
}
