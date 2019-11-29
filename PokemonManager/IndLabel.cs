using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PokemonManager
{
	public class IndLabel : Label
	{
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				setDeffaultColor();
			}
		}

		private void setDeffaultColor()
		{
			int num = int.Parse(base.Text);
			this.ForeColor = Color.Black;
			switch (num % 5)
			{
				case 0:
					this.BackColor = Color.Orange;
					break;
				case 1:
					this.BackColor = Color.Pink;
					break;
				case 2:
					this.BackColor = Color.LimeGreen;
					break;
				case 3:
					this.BackColor = Color.Violet;
					break;
				case 4:
					this.BackColor = Color.SkyBlue;
					break;

			}
		}

		private bool selected;
		public bool Selected {
			get { return selected; }
			set
			{
				if (value)
				{
					ForeColor = Color.White;
					BackColor = Color.Black;
				}else{
					setDeffaultColor();
				}
				selected = value;
			}
		}
	}
}
