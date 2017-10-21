using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Lumoria
{
	public partial class LumoriaSettings : UserControl
	{
		public bool AbSplit { get; set; }
		public bool AcSplit { get; set; }
		public bool AdSplit { get; set; }
		public bool AeSplit { get; set; }
		public bool AfSplit { get; set; }
		public bool AgSplit { get; set; }
		public bool BbSplit { get; set; }
		public bool BcSplit { get; set; }
		public bool BdSplit { get; set; }
		public bool CbSplit { get; set; }

		public LumoriaSettings()
		{
			InitializeComponent();
			this.AbCheckBox.DataBindings.Add("Checked", this, "AbSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AcCheckBox.DataBindings.Add("Checked", this, "AcSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AdCheckBox.DataBindings.Add("Checked", this, "AdSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AeCheckBox.DataBindings.Add("Checked", this, "AeSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AfCheckBox.DataBindings.Add("Checked", this, "AfSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.AgCheckBox.DataBindings.Add("Checked", this, "AgSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.BbCheckBox.DataBindings.Add("Checked", this, "BbSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.BcCheckBox.DataBindings.Add("Checked", this, "BcSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.BdCheckBox.DataBindings.Add("Checked", this, "BdSplit", false, DataSourceUpdateMode.OnPropertyChanged);
			this.CbCheckBox.DataBindings.Add("Checked", this, "CbSplit", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		static XmlElement ToElement<T>(XmlDocument doc, string name, T value)
		{
			XmlElement element = doc.CreateElement(name);
			element.InnerText = value.ToString();
			return element;
		}

		static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
		{
			bool val;
			return Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_;
		}

		public XmlNode GetSettings(XmlDocument doc)
		{
			XmlElement settingsNode = doc.CreateElement("Settings");
			settingsNode.AppendChild(ToElement(doc, "AbSplit", this.AbSplit));
			settingsNode.AppendChild(ToElement(doc, "AcSplit", this.AcSplit));
			settingsNode.AppendChild(ToElement(doc, "AdSplit", this.AdSplit));
			settingsNode.AppendChild(ToElement(doc, "AeSplit", this.AeSplit));
			settingsNode.AppendChild(ToElement(doc, "AfSplit", this.AfSplit));
			settingsNode.AppendChild(ToElement(doc, "AgSplit", this.AgSplit));
			settingsNode.AppendChild(ToElement(doc, "BbSplit", this.BbSplit));
			settingsNode.AppendChild(ToElement(doc, "BcSplit", this.BcSplit));
			settingsNode.AppendChild(ToElement(doc, "BdSplit", this.BdSplit));
			settingsNode.AppendChild(ToElement(doc, "CbSplit", this.CbSplit));
			return settingsNode;
		}

		public void SetSettings(XmlNode settings)
		{
			this.AbSplit = ParseBool(settings, "AbSplit");
			this.AcSplit = ParseBool(settings, "AcSplit");
			this.AdSplit = ParseBool(settings, "AdSplit");
			this.AeSplit = ParseBool(settings, "AeSplit");
			this.AfSplit = ParseBool(settings, "AfSplit");
			this.AgSplit = ParseBool(settings, "AgSplit");
			this.BbSplit = ParseBool(settings, "BbSplit");
			this.BcSplit = ParseBool(settings, "BcSplit");
			this.BdSplit = ParseBool(settings, "BdSplit");
			this.CbSplit = ParseBool(settings, "CbSplit");
		}
	}
}
