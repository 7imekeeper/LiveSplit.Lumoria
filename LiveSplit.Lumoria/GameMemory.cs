using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.Lumoria
{
	class GameData : MemoryWatcherList
	{
		public MemoryWatcher<int> BSP { get; }

		public GameData()
		{
			this.BSP = new MemoryWatcher<int>(new DeepPointer(0x6397D0));

			this.AddRange(this.GetType().GetProperties()
				.Where(p => !p.GetIndexParameters().Any())
				.Select(p => p.GetValue(this, null) as MemoryWatcher)
				.Where(p => p != null));
		}
	}

	class GameMemory
	{
		public event EventHandler OnFirstCutscene;
		public event EventHandler OnStart;
		public event EventHandler OnChapterChanged;
		public event EventHandler OnBSPChanged;

		private GameData _data;
		private Process _process;
		
		public GameMemory() { }

		public void Update()
		{
			if (_process == null || _process.HasExited)
			{
				if (!this.TryGetGameProcess())
					return;
			}

			TimedTraceListener.Instance.UpdateCount++;

			_data.UpdateAll(_process);

			if (_data.BSP.Changed)
			{
				if (_data.BSP.Current == 10)
				{
					this.OnFirstCutscene?.Invoke(this, EventArgs.Empty);
				}
				else if (_data.BSP.Current == 0)
				{
					if (_data.BSP.Old == 10)
						this.OnStart?.Invoke(this, EventArgs.Empty);
					else if (_data.BSP.Old == 8 || _data.BSP.Old == 3 || _data.BSP.Old == 1)
						this.OnChapterChanged(this, EventArgs.Empty);
				}
				else
					this.OnBSPChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		bool TryGetGameProcess()
		{
			Process game = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.ToLower() == "haloce"
				&& !p.HasExited);
			if (game == null)
				return false;

			_data = new GameData();
			_process = game;

			return true;
		}
	}
}
