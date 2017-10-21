using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveSplit.UI;
using System.Xml;
using System.Runtime.CompilerServices;

namespace LiveSplit.Lumoria
{
	class LumoriaComponent : LogicComponent
	{
		public override string ComponentName => "Lumoria";

		protected LumoriaSettings Settings { get; set; }

		private TimerModel _timer;
		private LiveSplitState _state;
		private GameMemory _gameMemory;
		private Timer _updateTimer;

		private int chapter;
		private int bsp;

		public LumoriaComponent(LiveSplitState state)
		{
#if DEBUG
			Debug.Listeners.Clear();
			Debug.Listeners.Add(TimedTraceListener.Instance);
#endif
			_state = state;

			this.Settings = new LumoriaSettings();

			_timer = new TimerModel { CurrentState = state };

			_updateTimer = new Timer() { Interval = 15, Enabled = true };
			_updateTimer.Tick += _updateTimer_Tick;

			_gameMemory = new GameMemory();
			_gameMemory.OnFirstCutscene += gameMemory_OnFirstCutscene;
			_gameMemory.OnStart += gameMemory_OnStart;
			_gameMemory.OnChapterChanged += gameMemory_OnChapterChanged;
			_gameMemory.OnBSPChanged += gameMemory_OnBSPChanged;
		}

		public override void Dispose()
		{
			_updateTimer?.Dispose();
		}

		private void _updateTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				_gameMemory.Update();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }


		private void gameMemory_OnFirstCutscene(object sender, EventArgs e)
		{
			chapter = 1;
			_timer.Reset();
		}

		private void gameMemory_OnStart(object sender, EventArgs e)
		{
			bsp = 0;
			_timer.Start();
		}

		private void gameMemory_OnChapterChanged(object sender, EventArgs e)
		{
			chapter++;
			bsp = 0;
			_timer.Split();
		}

		private void gameMemory_OnBSPChanged(object sender, EventArgs e)
		{
			bsp++;
			if (chapter == 1)
				switch (bsp)
				{
					case 1:
						if (this.Settings.AbSplit) _timer.Split();
						break;
					case 2:
						if (this.Settings.AcSplit) _timer.Split();
						break;
					case 3:
						if (this.Settings.AdSplit) _timer.Split();
						break;
					case 4:
						if (this.Settings.AeSplit) _timer.Split();
						break;
					case 5:
						if (this.Settings.AfSplit) _timer.Split();
						break;
					case 6:
						if (this.Settings.AgSplit) _timer.Split();
						break;
					default:
						break;
				}
			else if(chapter == 2)
				switch (bsp)
				{
					case 1:
						if (this.Settings.BbSplit) _timer.Split();
						break;
					case 2:
						if (this.Settings.BcSplit) _timer.Split();
						break;
					case 3:
						if (this.Settings.BdSplit) _timer.Split();
						break;
				}
			else if (chapter == 3)
				switch (bsp)
				{
					case 1:
						if (this.Settings.CbSplit) _timer.Split();
						break;
				}
		}

		public override Control GetSettingsControl(LayoutMode mode)
		{
			return this.Settings;
		}

		public override XmlNode GetSettings(XmlDocument document)
		{
			return this.Settings.GetSettings(document);
		}

		public override void SetSettings(XmlNode settings)
		{
			this.Settings.SetSettings(settings);
		}
	}

	public class TimedTraceListener : DefaultTraceListener
	{
		private static TimedTraceListener _instance;
		public static TimedTraceListener Instance => _instance ?? (_instance = new TimedTraceListener());

		private TimedTraceListener() { }

		public int UpdateCount
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			get;
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}
		
		public override void WriteLine(string message)
		{
			base.WriteLine("Lumoria: " + this.UpdateCount + " " + message);
		}
	}
}