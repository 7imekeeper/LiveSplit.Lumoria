using LiveSplit.Lumoria;
using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: ComponentFactory(typeof(LumoriaFactory))]

namespace LiveSplit.Lumoria
{
	public class LumoriaFactory : IComponentFactory
	{
		public string ComponentName => "Lumoria";
		public string Description => "Automates splitting for the custom Halo CE campaign, Lumoria.";
		public ComponentCategory Category => ComponentCategory.Control;

		public IComponent Create(LiveSplitState state)
		{
			return new LumoriaComponent(state);
		}

		public string UpdateName => this.ComponentName;
		public string UpdateURL => "https://raw.githubusercontent.com/7imekeeper/LiveSplit.Lumoria/master/";
		public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
		public string XMLURL => this.UpdateURL + "LiveSplit.Lumoria/Components/update.LiveSplit.Lumoria.xml";
	}
}
