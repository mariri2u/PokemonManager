using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PokemonManager
{
	public class Pokemon : ICloneable
	{
		//		private const int stLen = Enum.GetNames(typeof(Status)).Length;
		private const int stLen = 6;

		public Pokemon()
		{
			no = 0;
			name = "";
			egg = false;
			final = false;
			NickName = "";
			Lv = 1;
			type = new Type[2];
			Gender = "";
			for (int i = 0; i < type.Length; i++)
			{
				type[i] = Type.None;
			}
			familySt = new int[stLen];
			minIndSt = new int[stLen];
			maxIndSt = new int[stLen];
			basePt = new int[stLen];
			rankScale = new int[stLen];
			for (int i = 0; i < stLen; i++)
			{
				familySt[i] = 10;
				minIndSt[i] = 0;
				maxIndSt[i] = 0;
				basePt[i] = 0;
				rankScale[i] = 0;
			}
			skill = new string[4];
			for (int i = 0; i < skill.Length; i++)
			{
				skill[i] = "";
			}
			baseCharacteristic = new string[3];
			for (int i = 0; i < baseCharacteristic.Length; i++)
			{
				baseCharacteristic[i] = "";
			}
			Characteristic = 0;
			Personality = Personality.none;
			Item = "";
			Memo = "";
			Tag = "";
			Format = 0;
			State = State.None;
		}
		
		//
		private int no;
		public int No { get { return no; } }
		private bool final;
		public bool Final { get { return final; } }
		private bool egg;
		public bool Egg { get { return egg; } }
		// 補正
		private int[] rankScale;
		public int[] RankScale
		{
			get { return rankScale; }
		}
		[System.Xml.Serialization.XmlIgnore]
		public State State { get; set; }

		private string name;
		public string Name
		{
			get { return name; }
			set { 
				name = value;
				int charaNum = 0;
				for (int i = 0; i < baseCharacteristic.Length; i++)
				{
                    if (Characteristic != -1 &&
                        Characteristic == i)
                    {
                        charaNum = i;
                    }
				}
				setPrivateField(value);
                Characteristic =
                    (charaNum != -1 && charaNum < baseCharacteristic.Length) ?
                    charaNum : 0;
			}
		}

        public string NickName { get; set; }
		public int Lv { get; set; }

		private Type[] type;
		public Type Type1 { get { return type[0]; } }
		public Type Type2 { get { return type[1]; } }

		public string Gender { get; set; }

		// 種族値
		private int[] familySt;
        public int[] FamilySt
        {
            get
            {
                int[] st = new int[6];
                for (int i = 0; i < 6; i++) { st[i] = familySt[i]; }
                return st;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public string FamilyStatus
        {
            get
            {
                string s = "";
                for (int i = 0; i < 6; i++)
                {
                    s += familySt[i];
                    if (i < 5) { s += ","; }
                }
                return s;
            }
            set
            {
                string[] s = ((string)value).Split(',');
                for (int i = 0; i < 6; i++)
                {
                    familySt[i] = int.Parse(s[i]);
                }
            }
        }

		#region 個体値
		private int[] minIndSt;
		public int[] MinIndSt { get { return minIndSt; } }
        public string MinIndividualStatus
        {
            get
            {
                string s = "";
                for (int i = 0; i < 6; i++)
                {
                    s += minIndSt[i];
                    if (i < 5) { s += ","; }
                }
                return s;
            }
            set
            {
                string[] s = ((string)value).Split(',');
                for (int i = 0; i < 6; i++)
                {
                    minIndSt[i] = int.Parse(s[i]);
                }
            }
        }

		private int[] maxIndSt;
		public int[] MaxIndSt { get { return maxIndSt; } }
        public string MaxIndividualStatus
        {
            get
            {
                string s = "";
                for (int i = 0; i < 6; i++)
                {
                    s += maxIndSt[i];
                    if (i < 5) { s += ","; }
                }
                return s;
            }
            set
            {
                string[] s = ((string)value).Split(',');
                for (int i = 0; i < 6; i++)
                {
                    maxIndSt[i] = int.Parse(s[i]);
                }
            }
        }

		public string[] IndSt
		{
			get
			{
				string[] st = new string[stLen];
				for (int i = 0; i < st.Length; i++)
				{
					if (maxIndSt[i] == minIndSt[i])
					{
						st[i] = maxIndSt[i].ToString();
					}
					else
					{
						st[i] = minIndSt[i] + " - " + maxIndSt[i];
					}
				}
				return st;
			}
		}

		public void setIndStFromRealSt(Status st, int realSt)
		{
			int maxTmp = maxIndSt[(int)st];
			int latestValue = 0;
			for (int i = 0; i < 32; i++)
			{
				maxIndSt[(int)st] = i;
				int value = calcRealSt(st, true, false);
				if (i == 0)
				{
					if (value == realSt) { minIndSt[(int)st] = i; }
				}
				else if (i == 31)
				{
					if (value == realSt)
					{
						if (realSt > latestValue) { minIndSt[(int)st] = i; }
						maxTmp = i;
					}
				}
				else
				{
					if (latestValue < value && value == realSt)
					{
						minIndSt[(int)st] = i;
					}
					if (latestValue < value && latestValue == realSt)
					{
						maxTmp = i - 1;
					}
				}
				latestValue = value;
			}
			maxIndSt[(int)st] = maxTmp;
		}
		#endregion

		#region 努力値（基礎ポイント）
		private int[] basePt;
		public int[] BasePt { get { return basePt; } }
        public string BasePoint
        {
            get
            {
                string s = "";
                for (int i = 0; i < 6; i++)
                {
                    s += basePt[i];
                    if (i < 5) { s += ","; }
                }
                return s;
            }
            set
            {
                string[] s = ((string)value).Split(',');
                for (int i = 0; i < 6; i++)
                {
                    basePt[i] = int.Parse(s[i]);
                }
            }
        }

		#endregion

		// 技
		private string[] skill;
		public string[] Skill { get { return skill; } }
        [System.Xml.Serialization.XmlElement("Skill")]
        public string SkillSer
        {
            get
            {
                string s = "";
                for (int i = 0; i < 4; i++)
                {
                    s += skill[i];
                    if (i < 3) { s += ","; }
                }
                return s;
            }
            set
            {
                string[] s = ((string)value).Split(',');
                for (int i = 0; i < 4; i++)
                {
                    skill[i] = s[i];
                }
            }
        }

		// 特性
		private string[] baseCharacteristic;
		public string[] BaseCharacteristic { get { return baseCharacteristic; } }
        public int Characteristic { get; set; }

		// 性格
		[System.Xml.Serialization.XmlIgnore]
		public Personality Personality { get; set; }
		[System.Xml.Serialization.XmlElement("Personality")]
		public string PersonalityStr
		{
			get { return Personality.Decode(); }
			set { Personality = PersonalityEnum.Encode(value); }
		}


		// 持ち物
		public string Item { get; set; }

		// ゲーム外データ
		public string Memo { get; set; }
        public string Tag { get; set; }
        public string[] Tags
        {
            get
            {
				string[] s = Tag.Split(',');
				for (int i = 0; i < s.Length; i++)
				{
					s[i] = s[i].Replace(" ", "");
				}
                return s;
            }
        }
		public void AddTag(string s)
		{
			if (Tag == "")
			{
				Tag = s + ", ";
			}
			else
			{
				Tag += ", " + s + ", ";
			}
		}
		public bool TagContain(string s)
		{
			bool res = false;
			res |= (s == "");
			foreach (string t in Tags)
			{
				res |= (s == t);
			}
			return res;
		}
		//@

		#region 実測値のプロパティ
		public int[] MaxRealSt
		{
			get
			{
				int[] st = new int[stLen];
				for (int i = 0; i < stLen; i++)
				{
					st[i] =
						calcRealSt((Status)Enum.ToObject(typeof(Status), i), true, false);
				}
				return st;
			}
		}

		public int[] MinRealSt
		{
			get
			{
				int[] st = new int[stLen];
				for (int i = 0; i < stLen; i++)
				{
					st[i] =
						calcRealSt((Status)Enum.ToObject(typeof(Status), i), false, false);
				}
				return st;
			}
		}

		public int[] MaxRealOffsetSt
		{
			get {
				int[] st = new int[6];
				for (int i = 0; i < 6; i++)
				{
					st[i] =
						calcRealSt((Status)Enum.ToObject(typeof(Status), i), true, true);
				}
				return st; 
			}
		}

		public int[] MinRealOffsetSt
		{
			get {
				int[] st = new int[6];
				for (int i = 0; i < 6; i++)
				{
					st[i] =
						calcRealSt((Status)Enum.ToObject(typeof(Status), i), false, true);
				}
				return st; 		
			}
		}

		public string[] RealSt
		{
			get
			{
				string[] st = new string[stLen];
				int[] minRealSt = MinRealSt;
				int[] maxRealSt = MaxRealSt;
				for (int i = 0; i < st.Length; i++)
				{
					if (maxRealSt[i] == minRealSt[i])
					{
						st[i] = maxRealSt[i].ToString();
					}
					else
					{
						st[i] = minRealSt[i] + " - " + maxRealSt[i];
					}
				}
				return st;
			}
		}

		public string[] RealOffsetSt
		{
			get
			{
				string[] st = new string[stLen];
				int[] minRealSt = MinRealOffsetSt;
				int[] maxRealSt = MaxRealOffsetSt;
				for (int i = 0; i < st.Length; i++)
				{
					if (maxRealSt[i] == minRealSt[i])
					{
						st[i] = maxRealSt[i].ToString();
					}
					else
					{
						st[i] = minRealSt[i] + " - " + maxRealSt[i];
					}
				}
				return st;
			}
		}
		#endregion

		// 関数

		private int calcRealSt(Status st, bool max, bool rank)
		{
			return CalcRealSt(st,
				rank ? State : State.None,
				Lv, FamilySt[(int)st],
				max ? MaxIndSt[(int)st] : MinIndSt[(int)st],
				BasePt[(int)st], Personality.Scale(st), 
				rank ? RankScale[(int)st] : 0);
		}

		// 自己生成用
		public static Pokemon create(string name)
		{
			Pokemon pokemon = new Pokemon();
			pokemon.setPrivateField(name);
			return pokemon;
		}

        #region private関数群
        private void setPrivateField(string name)
		{
			// データを読み込む
			using (StreamReader reader = new StreamReader(@"data/pokemon.csv",
				System.Text.Encoding.GetEncoding("shift_jis")))
			{
				string line;
				// 1行目（CSVのヘッダ）を捨てる
				reader.ReadLine();
				// 引数に該当するデータまでファイルを読み込む
				while ((line = reader.ReadLine()) != null)
				{
					string[] s = line.Split(',');
					if (s[1] == name)
					{
						// 文字列から値を入力
						this.no = (int)double.Parse(s[0]);
                        this.name = s[1];
                        this.type[0] = TypeEnum.Encode(s[2]);
                        this.type[1] = TypeEnum.Encode(s[3]);
						for (int i = 0; i < stLen; i++)
						{
							this.familySt[i] = int.Parse(s[i + 4]);
						}
                        int charCnt = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            charCnt += (s[i + 10] != "") ? 1 : 0;
                        }
						this.baseCharacteristic = new string[charCnt];
                        int j = 0;
						for (int i = 0; i < 3; i++)
						{
                            if (s[i + 10] != "")
                            {
                                this.baseCharacteristic[j++] = s[i + 10];
                            }
						}
						this.final = (s[13] == "○");
						this.egg = (s[14] == "○");
						break;
					}
				}
			}
		}
        #endregion

		#region static関数（計算）
		public static int CalcRealSt(
			Status st, State se, int lv, int fam, int ind, int bp, Personality p){
			return CalcRealSt(st, se, lv, fam, ind, bp, p.Scale(st), 0);
		}

		public static int CalcRealSt(
			Status st, State se, 
			int lv, int fam, int ind, int bp, double pScale, int rScale)
		{
			int value = 0;
			if (st == Status.H)
			{
				value =
					(fam * 2 + ind + bp / 4)
					* lv / 100 + lv + 10;
			}
			else
			{
				double rs = rScale > 0 ?
					(3.0 + rScale) / 3.0 :
					3.0 / (3.0 - rScale);
				double ss =
					(se == State.Stop && st == Status.S) ? 0.25 :
					(se == State.Fire && st == Status.A) ? 0.5 : 1;
				value =
					(int)(((fam * 2 + ind + bp / 4)
					* lv / 100 + 5) * pScale * rs * ss);
				// アイテム補正
				// 特性補正
			}
			return value;
		}
		#endregion

		#region オーバライドした関数群
		public Object Clone()
		{
			Pokemon clone = Pokemon.create(this.name);
			clone.NickName = this.NickName;
			clone.Lv = this.Lv;
			clone.Gender = this.Gender;
			clone.Item = this.Item;
			clone.minIndSt = new int[6];
			clone.maxIndSt = new int[6];
			clone.basePt = new int[6];
			clone.Personality = this.Personality;
			clone.skill = new string[4];
			clone.Format = 0;
			clone.Tag = this.Tag;
			clone.Memo = this.Memo;
			for (int i = 0; i < 6; i++)
			{
				clone.minIndSt[i] = this.minIndSt[i];
				clone.maxIndSt[i] = this.maxIndSt[i];
				clone.basePt[i] = this.basePt[i];
			}
			for (int i = 0; i < 4; i++) { clone.skill[i] = this.skill[i]; }
			return clone;
		}
		[System.Xml.Serialization.XmlIgnore]
		public int Format { get; set; }
		public override string ToString()
		{
			string s = name;
			if (Format == 0)
			{
				s += " (";
				for (int i = 0; i < 6; i++)
				{
					if (i == 5) { s += minIndSt[i] + ")"; }
					else { s += minIndSt[i] + "-"; }
				}
			}
			return s;
		}
		#endregion
	}
}
