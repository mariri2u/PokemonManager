using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PokemonManager
{
	public class PokemonList
	{
		private List<Pokemon> baseList;
		private ListIO<List<Pokemon>> listio;

		public PokemonList(string dir, string name)
		{
			baseList = new List<Pokemon>();
			listio = new ListIO<List<Pokemon>>(dir, name);
			ClearInstFilter();
			IsSort = false;
		}

		PokemonList()
		{
			ClearStaticFilter();
		}

		public ListBox.ObjectCollection FilterdList { get; set; }
		public List<string> TagList
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("");
				foreach (Pokemon p in baseList)
				{
					foreach (string t in p.Tags)
					{
						if (!list.Contains(t)) { list.Add(t); }
					}
				}
				return list;
			}
		}

		#region リスト操作
		public void Add(Pokemon p)
		{
			baseList.Add(p);
			ApplyFilter();
		}

		public void AddRange(Pokemon[] ps)
		{
			baseList.AddRange(ps);
			ApplyFilter();
		}

		public void RemoveAt(int index)
		{
			baseList.RemoveAt(index);
			ApplyFilter();
		}

		public void Remove(Pokemon p)
		{
			baseList.Remove(p);
			ApplyFilter();
		}

		public void Insert(int index, Pokemon p)
		{
			baseList.Insert(index, p);
			ApplyFilter();
		}

		public int IndexOf(Pokemon p)
		{
			return baseList.IndexOf(p);
		}

		public void Clear()
		{
			baseList.Clear();
			ApplyFilter();
		}
		#endregion

		#region フィルター設定
		public void ClearInstFilter()
		{
			TagFilter1 = "";
			TagFilter2 = "";
		}

		public static void ClearStaticFilter()
		{
			TypeFilter1 = Type.None;
			TypeFilter2 = Type.None;
			Sort = NoSort;
			ProgressOnly = false;
			EggOnly = false;
		}

		public static Type TypeFilter1 { get; set; }
		public static Type TypeFilter2 { get; set; }
		public string TagFilter1 { get; set; }
		public string TagFilter2 { get; set; }
		public const int NoSort = 0;
		public const int NameSort = 1;
		public static int Sort { get; set; }
		public bool IsSort { get; set; }
		public static bool ProgressOnly { get; set; }
		public static bool EggOnly { get; set; }

		public void ApplyFilter()
		{
			if (FilterdList != null)
			{
				List<Pokemon> list = new List<Pokemon>();
				FilterdList.Clear();
				foreach (Pokemon p in baseList)
				{
					bool f = true;
					f &= TypeFilter1 == Type.None ||
						TypeFilter1 == p.Type1 ||
						TypeFilter1 == p.Type2;
					f &= TypeFilter2 == Type.None ||
						TypeFilter2 == p.Type1 ||
						TypeFilter2 == p.Type2;
					f &= TagFilter1 == "" ||
						p.TagContain(TagFilter1);
					f &= TagFilter2 == "" ||
						p.TagContain(TagFilter2);
					f &= !ProgressOnly || p.Final;
					f &= !EggOnly || p.Egg;
					if (f)
					{
						list.Add(p);
					}
				}
				if (IsSort)
				{
					if (Sort == NameSort)
					{
						list.Sort(delegate(Pokemon a, Pokemon b)
						{
							return string.Compare(a.Name, b.Name);
						});
					}
					else
					{
						list.Sort(delegate(Pokemon a, Pokemon b)
						{
							return a.No - b.No;
						});
					}
				}
				FilterdList.AddRange(list.ToArray());
			}
		}

		public bool IsFilter()
		{
			//bool res = false;
			//if (TypeFilter != Type.None) { res = true; }
			//return res;
			return (baseList.Count == FilterdList.Count);
		}
		#endregion

		#region static関数（一覧取得）
		public static List<string> NameList
		{
			get
			{
				List<string> list = new List<string>();
				// ポケモンデータを読み込む
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
						//bool f = true;
						//f &= TypeFilter1 == Type.None ||
						//    TypeFilter1 == TypeEnum.Encode(s[2]) ||
						//    TypeFilter1 == TypeEnum.Encode(s[3]);
						//f &= TypeFilter2 == Type.None ||
						//    TypeFilter2 == TypeEnum.Encode(s[2]) ||
						//    TypeFilter2 == TypeEnum.Encode(s[3]);
						//if (f)
						//{
						//    list.Add(s[1]);
						//}
						//if (ProgressOnly)
						//{
						//    f &= (s[13] == "○");
						//}
						//if (EggOnly)
						//{
						//    f &= (s[14] == "○");
						//}
						//if (f)
						//{
						//    list.Add(s[1]);
						//}
						list.Add(s[1]);
					}
				}
				if (Sort == NameSort)
				{
					list.Sort(delegate(string a, string b)
					{
						return string.Compare(a, b);
					});
				}
				return list;
			}
		}
		#endregion

		#region ファイルへの入出力
		public void Write()
		{
			listio.Write(baseList);
		}

		public void Read()
		{
			baseList = listio.Read();
		}

		public bool Exist()
		{
			return listio.exist();
		}
		#endregion
	}
}
