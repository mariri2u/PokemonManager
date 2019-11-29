using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PokemonManager
{
	public partial class IndStForm : Form
	{
		private IndLabel[,] indSt = new IndLabel[6, 32];
		private Label[] indValue = new Label[32];
		private Pokemon pokemon;
		public Pokemon Pokemon
		{
			get { return pokemon; }
			set
			{
				Pokemon p = value;
				pokeBox.Text = p.Name;
				lvIn.Value = p.Lv;
				p.Personality =
					(p.Personality != Personality.none) ?
					p.Personality :
					PersonalityEnum.Encode(personalityIn.Items[0].ToString());
				personalityIn.Text = p.Personality.Decode();
				pokemon = p;
				listupIndSt(p);
			}
		}
		public MainForm MainForm { get; set; }

		public IndStForm()
		{
			InitializeComponent();
			// 個体値選択ラベルの位置決定パラメータ
			int xDist = 45;
			int yDist = 18;
			int xBase = 45;
			int yBase = 32;
			// 個体値選択ラベルのヘッダを配置
			for (int i = 0; i < indValue.Length; i++)
			{
				indValue[i] = new Label();
				indValue[i].Text = (indValue.Length - i - 1).ToString();
				Point loc = new Point(xBase - xDist + 10, yBase + i * yDist);
				indValue[i].Location = loc;
				Size size = new Size(17, 12);
				indValue[i].Size = size;
				indBox.Controls.Add(indValue[i]);
			}
			// 個体値選択ラベルのボディを配置
			for (int i = 0; i < indSt.GetLength(1); i++)
			{
				for (int j = 0; j < indSt.GetLength(0); j++)
				{
					IndLabel currIndSt = new IndLabel();
					switch (j)
					{
						case (int)Status.H:
							currIndSt.Click += new EventHandler(indH_Click);
							break;
						case (int)Status.A:
							currIndSt.Click += new EventHandler(indA_Click);
							break;
						case (int)Status.B:
							currIndSt.Click += new EventHandler(indB_Click);
							break;
						case (int)Status.C:
							currIndSt.Click += new EventHandler(indC_Click);
							break;
						case (int)Status.D:
							currIndSt.Click += new EventHandler(indD_Click);
							break;
						case (int)Status.S:
							currIndSt.Click += new EventHandler(indS_Click);
							break;
					}
					indSt[j, indSt.GetLength(1) - i - 1] = currIndSt;
					Point loc = new Point(xBase + j * xDist, yBase + i * yDist);
					currIndSt.Location = loc;
					Size size = new Size(xDist - 5, yDist);
					currIndSt.Size = size;
					currIndSt.Text = (indSt.GetLength(1) - i - 1).ToString();
					currIndSt.Selected = false;
					indBox.Controls.Add(currIndSt);
				}
			}
			//
			// データを読み込む
			using (StreamReader reader = new StreamReader(@"data/personality.txt",
				System.Text.Encoding.GetEncoding("shift_jis")))
			{
				string line;
				// 引数に該当するデータまでファイルを読み込む
				while ((line = reader.ReadLine()) != null)
				{
					personalityIn.Items.Add(line);
				}
			}
		}

		// 個体値－実測値 の対応をリストアップ
		private void listupIndSt(Pokemon pokemon)
		{
			for (int i = 0; i < indSt.GetLength(0); i++)
			{
				int selectedNum = 0;
				for (int j = 0; j < indSt.GetLength(1); j++)
				{
					int indTmp = pokemon.MaxIndSt[i];
					pokemon.MaxIndSt[i] = j;
					indSt[i, j].Text = pokemon.MaxRealSt[i].ToString();
					pokemon.MaxIndSt[i] = indTmp;
					if (indSt[i, j].Selected) { selectedNum = j; }
				}
				indSt[i, selectedNum].Selected = true;
			}
		}

		/// <summary>
		/// ポケモンのステータスが変更されたとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void value_Changed(object sender, EventArgs e)
		{
			try
			{
				Pokemon.Lv = (int)lvIn.Value;
				Pokemon.Personality = PersonalityEnum.Encode(personalityIn.Text);
				listupIndSt(Pokemon);
			}
			catch (NullReferenceException) { }
		}

		/// <summary>
		/// 選択された個体値のポケモンを厳選中リストに入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button2_Click(object sender, EventArgs e)
		{
			Pokemon pokeClone = (Pokemon)pokemon.Clone();
			for (int i = 0; i < indSt.GetLength(0); i++)
			{
				for (int j = 0; j < indSt.GetLength(1); j++)
				{
					if (indSt[i, j].Selected)
					{
						pokeClone.MaxIndSt[i] = j;
						pokeClone.setIndStFromRealSt(
							(Status)Enum.ToObject(typeof(Status), i),
							pokeClone.MaxRealSt[i]);
					}
				}
			}
			// なぜかメインウインドウがアクティブになるから対策
            this.TopMost = true;
			MainForm.addPokemon(pokeClone);
            this.TopMost = false;
		}

		#region 個体値の表が入力されたときのイベントハンドラ
		private void indH_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.H);
		}
		private void indA_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.A);
		}
		private void indB_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.B);
		}
		private void indC_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.C);
		}
		private void indD_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.D);
		}
		private void indS_Click(object sender, EventArgs e)
		{
			ind_Click((IndLabel)sender, Status.S);
		}

		private void ind_Click(IndLabel curr, Status st)
		{
			for (int i = 0; i < indSt.GetLength(1); i++)
			{
				indSt[(int)st, i].Selected = false;
			}
			curr.Selected = true;
		}
		#endregion

	}
}
