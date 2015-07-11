using System;
using System.Text;
using System.Windows.Forms;

namespace Free.Core.Design
{
	public partial class ParametersNameInputForm : Form
	{
		public string Result { get; private set; }

		public ParametersNameInputForm()
		{
			InitializeComponent();

			buttonOK.Enabled=textBoxName.Text.Length>0;
		}

		public ParametersNameInputForm(string name)
		{
			InitializeComponent();

			textBoxName.Text=name;

			buttonOK.Enabled=textBoxName.Text.Length>0;
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			string name=textBoxName.Text;
			bool ok=true;

			int namelen=name.Length;
			if(namelen<1)
			{
				buttonOK.Enabled=false;
				return;
			}

			StringBuilder newName=new StringBuilder();

			// build clean name
			bool first=true;
			foreach(char c in name)
			{
				if(first)
				{
					if(!(c>='a'&&c<='z')&&!(c>='A'&&c<='Z')&&(c!='$')&&(c!='_'))
					{
						ok=false;
						continue;
					}

					newName.Append(c);
					first=false;
				}
				else
				{
					if(!(c>='a'&&c<='z')&&!(c>='A'&&c<='Z')&&!(c>='0'&&c<='9')&&(c!='$')&&(c!='_'))
					{
						ok=false;
						continue;
					}

					newName.Append(c);
				}
			}

			if(!ok) textBoxName.Text=newName.ToString();

			buttonOK.Enabled=textBoxName.Text.Length>0;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if(textBoxName.Text.Length==0) return;

			Result=textBoxName.Text;

			DialogResult=DialogResult.OK;
			Close();
		}
	}
}
