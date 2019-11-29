using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PokemonManager
{
	public partial class MainForm : Form
	{
		private Pokemon pokemon;
		private PokemonList[] list;
		private ListBox[] selectorList;

		private TextBox[] indStIn;
		private TextBox[] basePtIn;
		private Label[] familyStIn;
		private TextBox[] skillIn;
		private Label[] stHeader;
		private Label[] realStIn;

		// 戦闘シミュレータ
		private Pokemon oppPokemon;
		private Label[] oppStHeader;
		private Label[] oppFamSt;
		private TextBox[] oppIndSt;
		private TextBox[] oppBasePt;
		private Label[] oppRealSt;

		//
		private Label[,] compSt;
		private PokemonList oppList;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			// 初期化関数の呼び出し
			InitComponentArray();
			InitList();
			InitFilter();
			InitComboBox();
			// ポケモンを初期化しておく
			pokemon = Pokemon.create("");
			oppPokemon = Pokemon.create("");
			// 起動時に選択するポケモンが登録されていないとき
			if (compSelector.Items.Count > 0)
			{
				compSelector.SetSelected(0, true);
			}
			//
			compRankScale.SelectedIndex = 6;
			//
			oppNameIn.SelectedIndex = 0;
			sTypeIn.SelectedIndex = 0;
			sMyState.SelectedIndex = 0;
			sMyRank.SelectedIndex = 6;
			sOppState.SelectedIndex = 0;
			sOppRank.SelectedIndex = 6;
			sWeather.SelectedIndex = 0;
			if (oppListBox.Items.Count > 0)
			{
				oppListBox.SetSelected(0, true);
			}
		}

		#region init関数
		private void InitList()
		{
			//
			list = new PokemonList[3];
			for (int i = 0; i < list.Length; i++)
			{
				list[i] = new PokemonList("save", i.ToString());
				if (list[i].Exist())
				{
					list[i].Read();
				}
				if (i == 2)
				{
					// ポケモンデータを読み込む
					foreach (string s in PokemonList.NameList.ToArray())
					{
						Pokemon p = Pokemon.create(s);
						p.Format = 1;
						list[i].Add(p);
					}
					list[i].IsSort = true;
				}
				list[i].FilterdList = selectorList[i].Items;
				list[i].ApplyFilter();
			}
			oppList = new PokemonList("save", "10");
			if (oppList.Exist())
			{
				oppList.Read();
			}
			oppList.FilterdList = oppListBox.Items;
			oppList.ApplyFilter();
		}

		private void InitFilter()
		{
			// フィルターの初期化
			//typeFilter1.Items.Add("－");
			//typeFilter1.Items.AddRange(PokemonList.TypeList.ToArray());
			typeFilter1.Text = typeFilter1.Items[0].ToString();
			//typeFilter2.Items.Add("－");
			//typeFilter2.Items.AddRange(PokemonList.TypeList.ToArray());
			typeFilter2.Text = typeFilter2.Items[0].ToString();
			tagFilter1.Items.AddRange(list[0].TagList.ToArray());
			tagFilter1.Text = tagFilter1.Items[0].ToString();
			tagFilter2.Items.AddRange(list[0].TagList.ToArray());
			tagFilter2.Text = tagFilter2.Items[0].ToString();
		}

		private void InitComboBox()
		{
			// ポケモンデータを読み込む
			nameIn.Items.Clear();
			nameIn.Items.Add("");
			nameIn.Items.AddRange(PokemonList.NameList.ToArray());
			oppNameIn.Items.Clear();
			oppNameIn.Items.AddRange(PokemonList.NameList.ToArray());
		}

		private void InitComponentArray()
		{
			//
			selectorList = new ListBox[3];
			selectorList[0] = compSelector;
			selectorList[1] = eggSelector;
			selectorList[2] = newSelector;
			//
			Label[] sh = { hhead, ahead, bhead, chead, dhead, shead };
			stHeader = sh;
			indStIn = new TextBox[6];
			indStIn[0] = indH;
			indStIn[1] = indA;
			indStIn[2] = indB;
			indStIn[3] = indC;
			indStIn[4] = indD;
			indStIn[5] = indS;
			basePtIn = new TextBox[6];
			basePtIn[0] = basePtH;
			basePtIn[1] = basePtA;
			basePtIn[2] = basePtB;
			basePtIn[3] = basePtC;
			basePtIn[4] = basePtD;
			basePtIn[5] = basePtS;
			familyStIn = new Label[6];
			familyStIn[0] = familyH;
			familyStIn[1] = familyA;
			familyStIn[2] = familyB;
			familyStIn[3] = familyC;
			familyStIn[4] = familyD;
			familyStIn[5] = familyS;
			skillIn = new TextBox[6];
			skillIn[0] = skillIn1;
			skillIn[1] = skillIn2;
			skillIn[2] = skillIn3;
			skillIn[3] = skillIn4;
			Label[] rss = { realH, realA, realB, realC, realD, realS };
			realStIn = rss;
			//
			Label[] osh = { oppStHeadH, oppStHeadA, oppStHeadB, oppStHeadC, oppStHeadD, oppStHeadS };
			oppStHeader = osh;
			Label[] ofs = { oppFamH, oppFamA, oppFamB, oppFamC, oppFamD, oppFamS };
			oppFamSt = ofs;
			TextBox[] ois = { oppIndH, oppIndA, oppIndB, oppIndC, oppIndD, oppIndS };
			oppIndSt = ois;
			TextBox[] obp = { oppBaseH, oppBaseA, oppBaseB, oppBaseC, oppBaseD, oppBaseS };
			oppBasePt = obp;
			Label[] ors = { oppRealH, oppRealA, oppRealB, oppRealC, oppRealD, oppRealS };
			oppRealSt = ors;
			Label[,] cst = 
				{
					{compSt00,compSt01,compSt02,compSt03,compSt04,compSt05, compSt06},
					{compSt10,compSt11,compSt12,compSt13,compSt14,compSt15, compSt16},
					{compSt20,compSt21,compSt22,compSt23,compSt24,compSt25,compSt26},
					{compSt30,compSt31,compSt32,compSt33,compSt34,compSt35,compSt36}
				};
			compSt = cst;
		}

		#endregion

		#region 別フォームからのポケモン入力
		public void addPokemon(Pokemon p)
		{
			p.Lv = 50;
			list[1].Add(p);
			//pokemon = p;
			eggSelector.SetSelected(eggSelector.Items.Count - 1, true);
			renderMyAll();
			outputList(1);
		}
		#endregion

		#region リストデータ入れ替え
		private int listSelectNo;
		private int listSelectNo2;
		//移動元の項目をマウスを押す
		private void MyListBox_MouseDown(object sender, MouseEventArgs e)
		{
			//移動元のインデックスを取得
			ListBox lb = (ListBox)sender;
			listSelectNo2 = lb.SelectedIndex;
			PokemonList pl = null;
			if (lb == selectorList[selectorTab.SelectedIndex])
			{
				pl = list[selectorTab.SelectedIndex];
			}
			else if (lb == oppListBox)
			{
				pl = oppList;
			}
			listSelectNo = pl.IndexOf((Pokemon)lb.SelectedItem);
		}

		//移動先の項目でマウスを離すと項目移動
		private void MyListBox_MouseUp(object sender, MouseEventArgs e)
		{
			//移動先のインデックスを取得
			ListBox lb = (ListBox)sender;
			PokemonList pl = null;
			if (lb == selectorList[selectorTab.SelectedIndex])
			{
				pl = list[selectorTab.SelectedIndex];
			}
			else if (lb == oppListBox)
			{
				pl = oppList;
			}
			int listChangeNo2 = lb.SelectedIndex;
			int listChangeNo = pl.IndexOf((Pokemon)lb.SelectedItem);
			Pokemon tmpData;

			if (listChangeNo2 >= 0 &&
				listChangeNo2 < lb.Items.Count - 1 &&
				listSelectNo2 != listChangeNo2)
			{
				lb.BeginUpdate();

				//移動元のデータを取得
				tmpData = (Pokemon)lb.Items[listSelectNo2];

				//移動元のデータを削除
				pl.RemoveAt(listSelectNo);

				//移動先にデータを追加
				pl.Insert(listChangeNo, tmpData);

				//選択先のインデックスを指定
				lb.SelectedIndex = listChangeNo2;

				lb.EndUpdate();
			}
			pl.Write();
		}

		private void pokeSelectorUp(object sender, EventArgs e)
		{
			ListBox lb = null;
			PokemonList pl = null;
			if (sender == compUpButton)
			{
				lb = compSelector;
				pl = list[selectorTab.SelectedIndex];
			}
			else if (sender == oppUpButton)
			{
				lb = oppListBox;
				pl = oppList;
			}
			//移動元のインデックスを取得
			int srcIndex = lb.SelectedIndex;
			if (srcIndex > 0)
			{
				lb.BeginUpdate();
				//移動先のインデックスを取得
				Pokemon pokeTmp = (Pokemon)lb.SelectedItem;
				int dstIndex = pl.IndexOf(pokeTmp);
				//移動元のデータを削除
				pl.Remove(pokeTmp);

				//移動先にデータを追加
				pl.Insert(
					dstIndex - 1, pokeTmp);

				// 移動先を選択
				lb.SelectedIndex = srcIndex - 1;
				lb.EndUpdate();
			}
			pl.Write();
		}

		private void pokeSelectorDown(object sender, EventArgs e)
		{
			ListBox lb = null;
			PokemonList pl = null;
			if (sender == compDownButton)
			{
				lb = compSelector;
				pl = list[selectorTab.SelectedIndex];
			}
			else if (sender == oppDownButton)
			{
				lb = oppListBox;
				pl = oppList;
			}
			//移動元のインデックスを取得
			int srcIndex = lb.SelectedIndex;
			if (srcIndex < lb.Items.Count - 1)
			{
				lb.BeginUpdate();
				//移動先のインデックスを取得
				Pokemon pokeTmp = (Pokemon)lb.SelectedItem;
				int dstIndex = pl.IndexOf(pokeTmp);
				//移動元のデータを削除
				pl.Remove(pokeTmp);
				//lb.Items.RemoveAt(srcIndex);

				//移動先にデータを追加
				pl.Insert(
					dstIndex + 1, pokeTmp);
				//lb.Items.Insert(srcIndex + 1, pokeTmp);

				// 移動先を選択
				lb.SelectedIndex = srcIndex + 1;
				lb.EndUpdate();
			}
			pl.Write();
			//if (sender == compDownButton)
			//{
			//    outputList(selectorTab.SelectedIndex);
			//}
			//else if (sender == oppDownButton)
			//{
			//    pl.Write();
			//}
		}
		#endregion

		#region タブ／リストが切り替わったときのイベントハンドラ
		private void pokeSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBox lb = (ListBox)sender;
			if (lb.SelectedIndex >= 0 &&
				lb.SelectedIndex < lb.Items.Count)
			{
				pokemon =
					(Pokemon)((Pokemon)lb.Items[lb.SelectedIndex]).Clone();
				renderMyAll();
			}
			else
			{
				refreshValue();
			}
		}

		//private void newList_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//    pokemon = (Pokemon)selectorList[2].SelectedItem;
		//    pokemon.Lv = 50;
		//    renderMyAll();
		//}

		private void selectorTab_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			TabControl tab = (TabControl)sender;
			try
			{
				selectorList[tab.SelectedIndex].SetSelected(0, true);
			}
			catch (ArgumentOutOfRangeException)
			{
				if (selectorList[tab.SelectedIndex].SelectedIndex <= 0)
				{
					refreshValue();
				}
			}
		}
		#endregion

		#region 編集系

		private void pokemonData_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (sender == nameIn)
			{
				pokemon.Characteristic = charaIn.SelectedIndex;
				pokemon.Name = nameIn.Text;
				renderMyAll();
				// 入力したポケモンに別ポケモンの名前が含まれていた場合
				// 例：サンダース には サンダー が含まれている
				nameIn.Select(nameIn.Text.Length, 0);
			}
			else if (sender == oppNameIn)
			{
				oppPokemon.Characteristic = oppCharIn.SelectedIndex;
				oppPokemon.Name = oppNameIn.Text;
				renderOppSt();
				// 入力したポケモンに別ポケモンの名前が含まれていた場合
				// 例：サンダース には サンダー が含まれている
				oppNameIn.Select(oppNameIn.Text.Length, 0);
			}
			else if (sender == personalityIn)
			{
				pokemon = (Pokemon)pokemon.Clone();
				pokemon.Personality = PersonalityEnum.Encode(personalityIn.Text);
				renderMyAll();
			}
			else if (sender == oppPerIn)
			{
				oppPokemon.Personality = PersonalityEnum.Encode(oppPerIn.Text);
				renderOppSt();
			}
			else if (sender == compRankScale) { renderCompS(); }
			else if (sender == compStop) { renderCompS(); }
		}
		
		private void status_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			for (int i = 0; i < 6; i++)
			{
				if (basePtIn[i] == tb)
				{
					pokemon.BasePt[i] = int.Parse(tb.Text);
					renderMyStatus();
				}
				else if (oppBasePt[i] == tb)
				{
					oppPokemon.BasePt[i] = int.Parse(tb.Text);
					renderOppSt();
				}
				else if (indStIn[i] == tb)
				{
					string[] s = indStIn[i].Text.Split('-');
					if (s.Length < 2)
					{
						pokemon.MinIndSt[i] = int.Parse(s[0]);
						pokemon.MaxIndSt[i] = int.Parse(s[0]);
					}
					else
					{
						pokemon.MinIndSt[i] = int.Parse(s[0]);
						pokemon.MaxIndSt[i] = int.Parse(s[1]);
					}
					renderMyStatus();
				}
				else if (oppIndSt[i] == tb)
				{
					string[] s = oppIndSt[i].Text.Split('-');
					if (s.Length < 2)
					{
						oppPokemon.MinIndSt[i] = int.Parse(s[0]);
						oppPokemon.MaxIndSt[i] = int.Parse(s[0]);
					}
					else
					{
						oppPokemon.MinIndSt[i] = int.Parse(s[0]);
						oppPokemon.MaxIndSt[i] = int.Parse(s[1]);
					}
					renderOppSt();
				}
			}
		}

		private void editBtn_Click(object sender, EventArgs e)
		{
			// 選択されているときは編集
			if (selectorList[selectorTab.SelectedIndex].SelectedIndex >= 0)
			{
				pokemon = (Pokemon)pokemon.Clone();
				// 選択されていないときは追加
			}
			else
			{
				pokemon = Pokemon.create(nameIn.Text);
			}
			// 編集画面の変更を受け取る
			pokemon.Personality = PersonalityEnum.Encode(personalityIn.Text);
			pokemon.Characteristic = charaIn.SelectedIndex;
			pokemon.NickName = nnIn.Text;
			pokemon.Gender = genderIn.Text;
			pokemon.Item = itemIn.Text;
			pokemon.Memo = noteIn.Text;
			pokemon.Tag = tagIn.Text;
			for (int i = 0; i < 6; i++)
			{
				string[] s = indStIn[i].Text.Split('-');
				if (s.Length == 1)
				{
					pokemon.MaxIndSt[i] = int.Parse(s[0]);
					pokemon.MinIndSt[i] = int.Parse(s[0]);
				}
				else
				{
					pokemon.MaxIndSt[i] = int.Parse(s[1]);
					pokemon.MinIndSt[i] = int.Parse(s[0]);
				}
				pokemon.BasePt[i] = int.Parse(basePtIn[i].Text);
			}
			for (int i = 0; i < 4; i++)
			{
				pokemon.Skill[i] = skillIn[i].Text;
			}
			int finIndex = 0;
			if (selectorList[selectorTab.SelectedIndex].SelectedIndex >= 0)
			{
				// 新規ポケモンリストのタブが選択されている
				if (selectorTab.SelectedIndex == 2)
				{
					selectorList[0].BeginUpdate();
					list[0].Add(pokemon);
					//compSelector.Items.Add(pokemon.Clone());
					selectorTab.SelectedIndex = 0;
					//selectorList[0].SetSelected(
					//    selectorList[0].Items.Count - 1, true);
					finIndex = compSelector.Items.Count - 1;
					selectorList[0].EndUpdate();
				}
				else
				{
					selectorList[selectorTab.SelectedIndex].BeginUpdate();
					// フィルター前リストのインデックス
					int index = list[selectorTab.SelectedIndex].IndexOf(
						(Pokemon)selectorList
						[selectorTab.SelectedIndex].SelectedItem);
					// フィルター後リストのインデックス
					int index2 = selectorList
						[selectorTab.SelectedIndex].SelectedIndex;
					// データの操作はフィルター前のに対して行う
					list[selectorTab.SelectedIndex].RemoveAt(index);
					list[selectorTab.SelectedIndex].Insert(index, pokemon);
					// 選択はフィルター後に対して行う
					finIndex = index2;
					selectorList[selectorTab.SelectedIndex].EndUpdate();
				}
			}
			else
			{
				list[0].Add(pokemon);
			}
			// 変更後のポケモンはファイルに書き出す
			outputList(selectorTab.SelectedIndex);
			// タグ一覧を修正する
			selectorList[selectorTab.SelectedIndex].BeginUpdate();
			string tag;
			tag = tagFilter1.Text;
			tagFilter1.Items.Clear();
			tagFilter1.Items.AddRange(list[0].TagList.ToArray());
			tagFilter1.Text = tag;
			tag = tagFilter2.Text;
			tagFilter2.Items.Clear();
			tagFilter2.Items.AddRange(list[0].TagList.ToArray());
			tagFilter2.Text = tag;
			selectorList[selectorTab.SelectedIndex].EndUpdate();
			// 再描画する
			//render();
			selectorList[selectorTab.SelectedIndex].
						SetSelected(finIndex, true);
		}
		#endregion

		#region ボタンが押されたときのイベントハンドラ
		private void selectBtn_Click(object sender, EventArgs e)
		{
			IndStForm indStForm = new IndStForm();
			indStForm.MainForm = this;
			Pokemon p = (Pokemon)pokemon.Clone();
			p.Lv = 100;
			p.Personality = PersonalityEnum.Encode(personalityIn.Text);
			selectorTab.SelectedIndex = 1;
			if (eggSelector.Items.Count > 0)
			{
				eggSelector.SetSelected(eggSelector.Items.Count - 1, true);
			}
			indStForm.Pokemon = p;
			indStForm.Show();
		}

		private void eggConfBtn_Click(object sender, EventArgs e)
		{
			if (eggSelector.SelectedIndex >= 0)
			{
				eggSelector.BeginUpdate();
				Pokemon pokeTmp;
				int index = eggSelector.SelectedIndex;
				pokeTmp = (Pokemon)((Pokemon)eggSelector.SelectedItem).Clone();
				pokeTmp.AddTag("厳選済");
				// タグ一覧を修正する
				string tag;
				tag = tagFilter1.Text;
				tagFilter1.Items.Clear();
				tagFilter1.Items.AddRange(list[0].TagList.ToArray());
				tagFilter1.Text = tag;
				tag = tagFilter2.Text;
				tagFilter2.Items.Clear();
				tagFilter2.Items.AddRange(list[0].TagList.ToArray());
				tagFilter2.Text = tag;
				list[1].Remove((Pokemon)eggSelector.SelectedItem);
				if (eggSelector.Items.Count > 0)
				{
					index += (index <= 0) ? 1 : 0;
					eggSelector.SetSelected(index - 1, true);
					renderMyAll();
				}
				list[0].Add(pokeTmp);
				outputList(0);
				outputList(1);
				eggSelector.EndUpdate();
			}
		}

		private void delBtn_Click(object sender, EventArgs e)
		{
			ListBox lb = selectorList[selectorTab.SelectedIndex];
			lb.BeginUpdate();
			if (selectorTab.SelectedIndex != 2)
			{
				if (lb.SelectedIndex >= 0)
				{
					int index =
						selectorList[selectorTab.SelectedIndex].SelectedIndex;
					list[selectorTab.SelectedIndex].Remove(
						(Pokemon)lb.SelectedItem);
					//lb.Items.RemoveAt(lb.SelectedIndex);
					if (lb.Items.Count > 0)
					{
						index += (index <= 0) ? 1 : 0;
						lb.SetSelected(index - 1, true);
					}
				}
				outputList(selectorTab.SelectedIndex);
			}
			lb.EndUpdate();
		}

		#endregion

		#region フィルター処理
		private void typeFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectorList[selectorTab.SelectedIndex].BeginUpdate();
			// 全部のリストにフィルターを適用
			PokemonList.TypeFilter1 = TypeEnum.Encode(typeFilter1.Text);
			PokemonList.TypeFilter2 = TypeEnum.Encode(typeFilter2.Text);
			foreach (PokemonList pl in list)
			{
				pl.ApplyFilter();
			}
			InitComboBox();
			// フィルター後に残ったものを選択
			if (selectorList[selectorTab.SelectedIndex].Items.Count > 0)
			{
				selectorList[selectorTab.SelectedIndex].SetSelected(0, true);
			}
			else
			{
				refreshValue();
			}
			selectorList[selectorTab.SelectedIndex].EndUpdate();
		}

		private void tagFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectorList[selectorTab.SelectedIndex].BeginUpdate();
			// 全部のリストにフィルターを適用
			//foreach (PokemonList pl in list)
			//{
			//    pl.TagFilter1 = tagFilter1.Text;
			//    pl.TagFilter2 = tagFilter2.Text;
			//    pl.ApplyFilter();
			//}
			// 一覧のみに適用
			list[0].TagFilter1 = tagFilter1.Text;
			list[0].TagFilter2 = tagFilter2.Text;
			list[0].ApplyFilter();
			// フィルター後に残ったものを選択
			if (selectorList[selectorTab.SelectedIndex].Items.Count > 0)
			{
				selectorList[selectorTab.SelectedIndex].SetSelected(0, true);
			}
			else
			{
				refreshValue();
			}
			selectorList[selectorTab.SelectedIndex].EndUpdate();
		}


		private void sortFilter_CheckedChanged(object sender, EventArgs e)
		{
			selectorList[selectorTab.SelectedIndex].BeginUpdate();
			PokemonList.Sort =
				sortName.Checked ? PokemonList.NameSort : PokemonList.NoSort;
			foreach (PokemonList pl in list)
			{
				pl.ApplyFilter();
			}
			InitComboBox();
			// フィルター後に残ったものを選択
			if (selectorList[selectorTab.SelectedIndex].Items.Count > 0)
			{
				selectorList[selectorTab.SelectedIndex].SetSelected(0, true);
			}
			else
			{
				refreshValue();
			}
			selectorList[selectorTab.SelectedIndex].EndUpdate();
		}

		private void progressFilter_CheckedChanged(object sender, EventArgs e)
		{
			selectorList[selectorTab.SelectedIndex].BeginUpdate();
			PokemonList.EggOnly = eggOnly.Checked;
			PokemonList.ProgressOnly = finalOnly.Checked;
			foreach (PokemonList pl in list)
			{
				pl.ApplyFilter();
			}
			InitComboBox();
			// フィルター後に残ったものを選択
			if (selectorList[selectorTab.SelectedIndex].Items.Count > 0)
			{
				selectorList[selectorTab.SelectedIndex].SetSelected(0, true);
			}
			else
			{
				refreshValue();
			}
			selectorList[selectorTab.SelectedIndex].EndUpdate();
		}

		#endregion

		#region private関数群
		private void outputList(int index)
		{
			if (index == 0 || index == 1)
			{
				list[index].Write();
			}
		}
		#endregion

		#region 再描画関数
		private void renderMyAll()
		{
			//
			charaIn.Items.Clear();
			charaIn.Items.AddRange(pokemon.BaseCharacteristic);
			//
			nameIn.Text = pokemon.Name;
			charaIn.SelectedIndex =
				pokemon.Characteristic == -1 ? 0 : pokemon.Characteristic;
			personalityIn.Text =
				pokemon.Personality != Personality.none ?
				pokemon.Personality.Decode() : personalityIn.Items[0].ToString();
			itemIn.Text = pokemon.Item;
			noteIn.Text = pokemon.Memo;
			genderIn.SelectedIndex =
				(pokemon.Gender == "♂") ? 0 :
				(pokemon.Gender == "♀") ? 1 : -1;
			nnIn.Text = pokemon.NickName;
			typeOut.Text = pokemon.Type1.Decode1();
			typeOut.Text += (pokemon.Type2 != Type.None) ? " " + pokemon.Type2.Decode1() : "";
			tagIn.Text = pokemon.Tag;
			typex4.Text = "－";
			typex2.Text = "－";
			typex1_2.Text = "－";
			typex1_4.Text = "－";
			typex0.Text = "－";
			foreach(Type t in Enum.GetValues(typeof(Type))){
				double scale = t.Scale(pokemon.Type1, pokemon.Type2);
				if (scale == 4)
				{
					if (typex4.Text == "－") { typex4.Text = t.Decode1(); }
					else { typex4.Text += " " + t.Decode1(); }
				}
				else if (scale == 2)
				{
					if (typex2.Text == "－") { typex2.Text = t.Decode1(); }
					else { typex2.Text += " " + t.Decode1(); }
				}
				else if (scale == 0.5)
				{
					if (typex1_2.Text == "－") { typex1_2.Text = t.Decode1(); }
					else { typex1_2.Text += " " + t.Decode1(); }
				}
				else if (scale == 0.25)
				{
					if (typex1_4.Text == "－") { typex1_4.Text = t.Decode1(); }
					else { typex1_4.Text += " " + t.Decode1(); }
				}
				else if (scale == 0)
				{
					if (typex0.Text == "－") { typex0.Text = t.Decode1(); }
					else { typex0.Text += " " + t.Decode1(); }
				}
			}
			for (int i = 0; i < 4; i++)
			{
				skillIn[i].Text = pokemon.Skill[i];
			}
			renderMyStatus();
		}

		private void renderMyStatus()
		{
			pokemon.Lv = 50;
			for (int i = 0; i < 6; i++)
			{
				indStIn[i].Text = pokemon.IndSt[i];
				basePtIn[i].Text = pokemon.BasePt[i].ToString();
				familyStIn[i].Text = pokemon.FamilySt[i].ToString();
				stHeader[i].ForeColor = pokemon.Personality.Color(i);
				realStIn[i].Text = pokemon.RealSt[i].ToString();
			}
			renderCompS();
		}

		private void renderCompS()
		{
			//
			Status ss = Status.S;
			pokemon.State = compStop.Checked ? State.Stop : State.None;
			pokemon.RankScale[(int)ss] = compRankScale.SelectedIndex - 6;
			selfStOut.Text = pokemon.MinRealOffsetSt[(int)ss].ToString();
			int mst = int.Parse(selfStOut.Text);
			for (int i = 0; i < compSt.GetLength(0); i++)
			{
				for (int j = 0; j < compSt.GetLength(1); j++)
				{
					//
					int ins = (i <= 2) ? 31 : 0;
					int bp = (i <= 1) ? 252 : 0;
					double ps = (i == 0) ? 1.1 : (i == 3) ? 0.9 : 1.0;
					int rs = (j == 0) ? 0 : j - 3;
					State bs = (j == 0) ? State.Stop : State.None;
					//double bs = (j == 0) ? 0.25 : 1.0;
					bool fin = false;
					for (int k = 180; k >= -1; k--)
					{
						int tst = Pokemon.CalcRealSt(ss, bs,50, k, ins, bp, ps, rs);
						//tst = (int)(tst * bs);
						if (!fin && mst > tst)
						{
							compSt[i, j].Text = k.ToString();
							fin = true;
						}
					}
					if (j == 3)
					{
						compSt[i, j].ForeColor = Color.Magenta;
					}
					if (compSt[i, j].Text == "180")
					{
						compSt[i, j].Text = "All";
						compSt[i, j].ForeColor = Color.Red;
					}
					else if (!fin)
					{
						compSt[i, j].Text = "None";
						compSt[i, j].ForeColor = Color.Blue;
					}
					else if (j != 3)
					{
						compSt[i, j].ForeColor = Color.Black;
					}
				}
			}
		}

		private void renderOppSt()
		{
			if (oppPokemon == null)
			{
				oppPokemon = Pokemon.create(oppNameIn.Text);
			}
			oppPokemon.Lv = 50;
			//
			oppCharIn.Items.Clear();
			oppCharIn.Items.AddRange(oppPokemon.BaseCharacteristic);
			//
			oppNameIn.Text = oppPokemon.Name;
			oppCharIn.Text =
				oppPokemon.Characteristic == -1 ?
				oppPokemon.BaseCharacteristic[0] :
				oppPokemon.BaseCharacteristic[pokemon.Characteristic];
			oppPerIn.Text =
				oppPokemon.Personality != Personality.none ?
				oppPokemon.Personality.Decode() : oppPerIn.Items[0].ToString();
			oppItemIn.Text = pokemon.Item;
			pokemon.Lv = 50;
			for (int i = 0; i < 6; i++)
			{
				oppIndSt[i].Text = oppPokemon.IndSt[i];
				oppBasePt[i].Text = oppPokemon.BasePt[i].ToString();
				oppFamSt[i].Text = oppPokemon.FamilySt[i].ToString();
				oppStHeader[i].ForeColor = oppPokemon.Personality.Color(i);
				oppRealSt[i].Text = oppPokemon.RealSt[i].ToString();
			}
		}

		public void refreshValue()
		{
			pokemon = Pokemon.create("");
			nameIn.Text = "";
			charaIn.Text = "";
			personalityIn.Text = "";
			genderIn.SelectedIndex = -1;
			nnIn.Text = "";

			for (int i = 0; i < 6; i++)
			{
				indStIn[i].Text = 0.ToString();
				basePtIn[i].Text = 0.ToString();
				realStIn[i].Text = 0.ToString();
				familyStIn[i].Text = 0.ToString();
			}
		}
		#endregion

		#region 戦闘シミュレータ
		private void calcBtn_Click(object sender, EventArgs e)
		{
			Field f = new Field();
			// 補正の入力
			Pokemon p = (Pokemon)pokemon.Clone();
			p.State = StateExt.Encode(sMyState.Text);
			oppPokemon.State = StateExt.Encode(sOppState.Text);
			f.Critical = sCri.Checked;
			// 攻守の切り替え
			if (sAtk.Checked)
			{
				p.RankScale[(int)(sPhy.Checked ? Status.A : Status.C)] =
					sMyRank.SelectedIndex - 6;
				oppPokemon.RankScale[(int)(sPhy.Checked ? Status.B : Status.D)] =
					sOppRank.SelectedIndex - 6;
				//
				f.AtkPokemon = p;
				f.DefPokemon = oppPokemon;
			}
			else
			{
				p.RankScale[(int)(sPhy.Checked ? Status.B : Status.D)] =
					sMyRank.SelectedIndex - 6;
				oppPokemon.RankScale[(int)(sPhy.Checked ? Status.A : Status.C)] =
					sOppRank.SelectedIndex - 6;
				//
				f.AtkPokemon = oppPokemon;
				f.DefPokemon = p;
			}
			// スキルの作成
			Skill s = new Skill();
			s.Power = int.Parse(sPowerIn.Text);
			s.Type = TypeEnum.Encode(sTypeIn.Text);
			s.IsPhysic = sPhy.Checked;
			// ダメージ計算
			Damage dmg = f.Attack(s);
			int minPer = dmg.MinDmg * 100 / oppPokemon.MaxRealSt[(int)Status.H];
			int maxPer = dmg.MaxDmg * 100 / oppPokemon.MinRealSt[(int)Status.H];
			// 結果を出力
			sDmgOut.Text = dmg.MinDmg.ToString() + " - " + dmg.MaxDmg.ToString();
			sDmgPerOut.Text = minPer.ToString() + " - " + maxPer.ToString() + " [%]";
			paintRestHp(f.DefPokemon.MaxRealSt[(int)Status.H], dmg.MinDmg, dmg.MaxDmg);
			sASt.Text =  sPhy.Checked ?
				"こうげき："+f.AtkPokemon.RealOffsetSt[(int)Status.A]:
				"とくこう：" + f.AtkPokemon.RealOffsetSt[(int)Status.C];
			sDSt.Text = sPhy.Checked ?
				"ぼうぎょ：" + f.DefPokemon.RealOffsetSt[(int)Status.B] :
				"とくぼう：" + f.DefPokemon.RealOffsetSt[(int)Status.D];
			sTypeScale.Text =
				s.Type.Scale(f.DefPokemon.Type1, f.DefPokemon.Type2).ToString();
			sSameType.Text =
				s.Type == f.AtkPokemon.Type1 || s.Type == f.AtkPokemon.Type2 ?
				"1.5" : "1.0";
		}

		private void paintRestHp(int hp, int minDmg, int maxDmg)
		{
			Graphics g = paintArea.CreateGraphics();
			// メモリを書く
			int baseX = 0;
			int baseY = 5;
			int inter = paintArea.Width / 9;
			for (int i = 0; i <= 10; i++)
			{
				int x1 = paintArea.Width * i / 10;
				x1 -= (i == 10) ? 1 : 0;
				int y1 = (i == 0 || i == 5 || i == 10) ? 0 : 2;
				int x2 = x1;
				int y2 = 5;
				g.DrawLine(Pens.Black, new Point(x1, y1), new Point(x2, y2));
			}
			int w, h;
			w = paintArea.Width;
			h = paintArea.Height;
			g.FillRectangle(Brushes.Gray, baseX, baseY, w, h);
			w = paintArea.Width * maxDmg / hp;
			g.FillRectangle(Brushes.LightGreen, baseX, baseY, w, h);
			w = paintArea.Width * minDmg / hp;
			g.FillRectangle(Brushes.Green, baseX, baseY, w, h);
			g.Dispose();
		}

		private void oppSave_Click(object sender, EventArgs e)
		{
			oppList.Add((Pokemon)oppPokemon.Clone());
			oppList.Write();
		}

		private void oppDel_Click(object sender, EventArgs e)
		{
			oppList.Remove((Pokemon)oppListBox.SelectedItem);
			oppList.Write();
			if (oppListBox.Items.Count > 0)
			{
				oppListBox.SetSelected(0, true);
			}
		}

		private void oppConf_Click(object sender, EventArgs e)
		{
			oppPokemon = (Pokemon)((Pokemon)oppListBox.SelectedItem).Clone();
			renderOppSt();
		}


		#endregion

	}
}
