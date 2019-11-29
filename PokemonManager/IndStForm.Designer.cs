namespace PokemonManager
{
	partial class IndStForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.indBox = new System.Windows.Forms.GroupBox();
			this.headerH = new System.Windows.Forms.Label();
			this.headerS = new System.Windows.Forms.Label();
			this.headerD = new System.Windows.Forms.Label();
			this.headerC = new System.Windows.Forms.Label();
			this.headerB = new System.Windows.Forms.Label();
			this.headerA = new System.Windows.Forms.Label();
			this.personalityIn = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.pokeBox = new System.Windows.Forms.GroupBox();
			this.lvIn = new System.Windows.Forms.NumericUpDown();
			this.indBox.SuspendLayout();
			this.pokeBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lvIn)).BeginInit();
			this.SuspendLayout();
			// 
			// indBox
			// 
			this.indBox.Controls.Add(this.headerH);
			this.indBox.Controls.Add(this.headerS);
			this.indBox.Controls.Add(this.headerD);
			this.indBox.Controls.Add(this.headerC);
			this.indBox.Controls.Add(this.headerB);
			this.indBox.Controls.Add(this.headerA);
			this.indBox.Location = new System.Drawing.Point(12, 12);
			this.indBox.Name = "indBox";
			this.indBox.Size = new System.Drawing.Size(330, 629);
			this.indBox.TabIndex = 4;
			this.indBox.TabStop = false;
			this.indBox.Text = "個体値";
			// 
			// headerH
			// 
			this.headerH.AutoSize = true;
			this.headerH.Location = new System.Drawing.Point(45, 15);
			this.headerH.Name = "headerH";
			this.headerH.Size = new System.Drawing.Size(20, 12);
			this.headerH.TabIndex = 0;
			this.headerH.Text = "HP";
			// 
			// headerS
			// 
			this.headerS.AutoSize = true;
			this.headerS.Location = new System.Drawing.Point(270, 15);
			this.headerS.Name = "headerS";
			this.headerS.Size = new System.Drawing.Size(43, 12);
			this.headerS.TabIndex = 0;
			this.headerS.Text = "すばやさ";
			// 
			// headerD
			// 
			this.headerD.AutoSize = true;
			this.headerD.Location = new System.Drawing.Point(225, 15);
			this.headerD.Name = "headerD";
			this.headerD.Size = new System.Drawing.Size(36, 12);
			this.headerD.TabIndex = 0;
			this.headerD.Text = "とくぼう";
			// 
			// headerC
			// 
			this.headerC.AutoSize = true;
			this.headerC.Location = new System.Drawing.Point(180, 15);
			this.headerC.Name = "headerC";
			this.headerC.Size = new System.Drawing.Size(34, 12);
			this.headerC.TabIndex = 0;
			this.headerC.Text = "とくこう";
			// 
			// headerB
			// 
			this.headerB.AutoSize = true;
			this.headerB.Location = new System.Drawing.Point(135, 15);
			this.headerB.Name = "headerB";
			this.headerB.Size = new System.Drawing.Size(39, 12);
			this.headerB.TabIndex = 0;
			this.headerB.Text = "ぼうぎょ";
			// 
			// headerA
			// 
			this.headerA.AutoSize = true;
			this.headerA.Location = new System.Drawing.Point(90, 15);
			this.headerA.Name = "headerA";
			this.headerA.Size = new System.Drawing.Size(39, 12);
			this.headerA.TabIndex = 0;
			this.headerA.Text = "こうげき";
			// 
			// personalityIn
			// 
			this.personalityIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.personalityIn.FormattingEnabled = true;
			this.personalityIn.Location = new System.Drawing.Point(62, 43);
			this.personalityIn.Name = "personalityIn";
			this.personalityIn.Size = new System.Drawing.Size(100, 20);
			this.personalityIn.TabIndex = 17;
			this.personalityIn.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(453, 97);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 15;
			this.button1.Text = "採用";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button2_Click);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(14, 46);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(29, 12);
			this.label11.TabIndex = 9;
			this.label11.Text = "性格";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "Lv";
			// 
			// pokeBox
			// 
			this.pokeBox.Controls.Add(this.lvIn);
			this.pokeBox.Controls.Add(this.label3);
			this.pokeBox.Controls.Add(this.label11);
			this.pokeBox.Controls.Add(this.personalityIn);
			this.pokeBox.Location = new System.Drawing.Point(348, 12);
			this.pokeBox.Name = "pokeBox";
			this.pokeBox.Size = new System.Drawing.Size(180, 79);
			this.pokeBox.TabIndex = 18;
			this.pokeBox.TabStop = false;
			// 
			// lvIn
			// 
			this.lvIn.Location = new System.Drawing.Point(62, 18);
			this.lvIn.Name = "lvIn";
			this.lvIn.Size = new System.Drawing.Size(100, 19);
			this.lvIn.TabIndex = 19;
			this.lvIn.ValueChanged += new System.EventHandler(this.value_Changed);
			// 
			// IndStForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(543, 649);
			this.Controls.Add(this.pokeBox);
			this.Controls.Add(this.indBox);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "IndStForm";
			this.Text = "厳選 - Pokemon Manager";
			this.indBox.ResumeLayout(false);
			this.indBox.PerformLayout();
			this.pokeBox.ResumeLayout(false);
			this.pokeBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lvIn)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox indBox;
		private System.Windows.Forms.Label headerH;
		private System.Windows.Forms.Label headerS;
		private System.Windows.Forms.Label headerD;
		private System.Windows.Forms.Label headerC;
		private System.Windows.Forms.Label headerB;
		private System.Windows.Forms.Label headerA;
		private System.Windows.Forms.ComboBox personalityIn;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox pokeBox;
		private System.Windows.Forms.NumericUpDown lvIn;

	}
}