// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.TimeMeasure
using System;
using System.Diagnostics;

namespace FluffyUnderware.DevTools
{
	public class TimeMeasure : Ring<long>
	{
		public TimeMeasure(int size) : base(size)
		{
		}

		public void Start()
		{
			this.mWatch.Start();
		}

		public void Stop()
		{
			this.mWatch.Stop();
			base.Add(this.mWatch.ElapsedTicks);
			this.mWatch.Reset();
		}

		public void Pause()
		{
			this.mWatch.Stop();
		}

		public double LastTicks
		{
			get
			{
				return (double)base[base.Count - 1];
			}
		}

		public double LastMS
		{
			get
			{
				return this.LastTicks / 10000.0;
			}
		}

		public double AverageMS
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < base.Count; i++)
				{
					num += base[i];
				}
				return DTMath.FixNaN((double)num / 10000.0 / (double)base.Count);
			}
		}

		public double MinimumMS
		{
			get
			{
				long num = long.MaxValue;
				for (int i = 0; i < base.Count; i++)
				{
					num = Math.Min(num, base[i]);
				}
				return DTMath.FixNaN((double)num / 10000.0);
			}
		}

		public double MaximumMS
		{
			get
			{
				long num = long.MinValue;
				for (int i = 0; i < base.Count; i++)
				{
					num = Math.Max(num, base[i]);
				}
				return DTMath.FixNaN((double)num / 10000.0);
			}
		}

		public double AverageTicks
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < base.Count; i++)
				{
					num += base[i];
				}
				return (double)num / (double)base.Count;
			}
		}

		public double MinimumTicks
		{
			get
			{
				long num = long.MaxValue;
				for (int i = 0; i < base.Count; i++)
				{
					num = Math.Min(num, base[i]);
				}
				return (double)num;
			}
		}

		public double MaximumTicks
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < base.Count; i++)
				{
					num = Math.Max(num, base[i]);
				}
				return (double)num;
			}
		}

		public Stopwatch mWatch = new Stopwatch();
	}
}
