// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ThreadPoolWorker
using System;
using System.Collections.Generic;
using System.Threading;

namespace FluffyUnderware.DevTools
{
	public class ThreadPoolWorker : IDisposable
	{
		public void QueueWorkItem(WaitCallback callback)
		{
			this.QueueWorkItem(callback, null);
		}

		public void QueueWorkItem(Action act)
		{
			this.QueueWorkItem(act, null);
		}

		public void ParralelFor<T>(Action<T> action, List<T> list)
		{
			int val = Environment.ProcessorCount - 1;
			int num = 1 + Math.Min(val, Environment.ProcessorCount - 1);
			int count = list.Count;
			if (num == 1 || count == 1)
			{
				for (int i = 0; i < count; i++)
				{
					action(list[i]);
				}
			}
			else
			{
				int num2 = (int)Math.Ceiling((double)((float)count / (float)num));
				int num3;
				for (int j = 0; j < count; j = num3 + 1)
				{
					QueuedCallback queuedCallback = new QueuedCallback();
					num3 = Math.Min(j + num2, count - 1);
					queuedCallback.State = new LoopState<T>
					{
						StartIndex = (short)j,
						EndIndex = (short)num3,
						Action = action,
						Items = list
					};
					queuedCallback.Callback = delegate(object state)
					{
						LoopState<T> loopState = (LoopState<T>)state;
						for (int k = (int)loopState.StartIndex; k <= (int)loopState.EndIndex; k++)
						{
							loopState.Action(loopState.Items[k]);
						}
					};
					this.QueueWorkItem(queuedCallback);
				}
			}
		}

		private void QueueWorkItem(QueuedCallback callback)
		{
			this.ThrowIfDisposed();
			object done = this._done;
			lock (done)
			{
				this._remainingWorkItems++;
			}
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.HandleWorkItem), callback);
		}

		public void QueueWorkItem(WaitCallback callback, object state)
		{
			this.QueueWorkItem(new QueuedCallback
			{
				Callback = callback,
				State = state
			});
		}

		public void QueueWorkItem(Action act, object state)
		{
			this.QueueWorkItem(new QueuedCallback
			{
				Callback = delegate(object x)
				{
					act();
				},
				State = state
			});
		}

		public bool WaitAll()
		{
			return this.WaitAll(-1, false);
		}

		public bool WaitAll(TimeSpan timeout, bool exitContext)
		{
			return this.WaitAll((int)timeout.TotalMilliseconds, exitContext);
		}

		public bool WaitAll(int millisecondsTimeout, bool exitContext)
		{
			this.ThrowIfDisposed();
			this.DoneWorkItem();
			bool flag = this._done.WaitOne(millisecondsTimeout, exitContext);
			object done = this._done;
			lock (done)
			{
				if (flag)
				{
					this._remainingWorkItems = 1;
					this._done.Reset();
				}
				else
				{
					this._remainingWorkItems++;
				}
			}
			return flag;
		}

		private void HandleWorkItem(object state)
		{
			QueuedCallback queuedCallback = (QueuedCallback)state;
			try
			{
				queuedCallback.Callback(queuedCallback.State);
			}
			finally
			{
				this.DoneWorkItem();
			}
		}

		private void DoneWorkItem()
		{
			object done = this._done;
			lock (done)
			{
				this._remainingWorkItems--;
				if (this._remainingWorkItems == 0)
				{
					this._done.Set();
				}
			}
		}

		private void ThrowIfDisposed()
		{
			if (this._done == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		public void Dispose()
		{
			if (this._done != null)
			{
				((IDisposable)this._done).Dispose();
				this._done = null;
			}
		}

		private int _remainingWorkItems = 1;

		private ManualResetEvent _done = new ManualResetEvent(false);
	}
}
