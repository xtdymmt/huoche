// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: FluffyUnderware.DevTools.ThreadPoolWorker`1
using System;
using System.Collections.Generic;
using System.Threading;

namespace FluffyUnderware.DevTools
{
	public class ThreadPoolWorker<T> : IDisposable
	{
		public ThreadPoolWorker()
		{
			this.handleWorkItemCallBack = delegate(object o)
			{
				QueuedCallback queuedCallback = (QueuedCallback)o;
				try
				{
					queuedCallback.Callback(queuedCallback.State);
				}
				finally
				{
					object obj = this.queuedCallbackPool;
					lock (obj)
					{
						this.queuedCallbackPool.ReleaseItem(queuedCallback);
					}
					this.DoneWorkItem();
				}
			};
			this.handleLoopCallBack = delegate(object state)
			{
				LoopState<T> loopState = (LoopState<T>)state;
				for (int i = (int)loopState.StartIndex; i <= (int)loopState.EndIndex; i++)
				{
					loopState.Action(loopState.Items[i]);
				}
				object obj = this.loopStatePool;
				lock (obj)
				{
					this.loopStatePool.ReleaseItem(loopState);
				}
			};
		}

		public void ParralelFor(Action<T> action, List<T> list)
		{
			int val = Environment.ProcessorCount - 1;
			int num = 1 + Math.Min(val, Environment.ProcessorCount - 1);
			int count = list.Count;
			int num2 = (num != 1) ? ((int)Math.Ceiling((double)((float)count / (float)num))) : count;
			int num3;
			for (int i = 0; i < count; i = num3 + 1)
			{
				num3 = Math.Min(i + num2 - 1, count - 1);
				if (num3 == count - 1)
				{
					for (int j = i; j <= num3; j++)
					{
						action(list[j]);
					}
				}
				else
				{
					object obj = this.queuedCallbackPool;
					QueuedCallback item;
					lock (obj)
					{
						item = this.queuedCallbackPool.GetItem();
					}
					object obj2 = this.loopStatePool;
					LoopState<T> item2;
					lock (obj2)
					{
						item2 = this.loopStatePool.GetItem();
					}
					item2.StartIndex = (short)i;
					item2.EndIndex = (short)num3;
					item2.Action = action;
					item2.Items = list;
					item.State = item2;
					item.Callback = this.handleLoopCallBack;
					this.ThrowIfDisposed();
					object done = this._done;
					lock (done)
					{
						this._remainingWorkItems++;
					}
					ThreadPool.QueueUserWorkItem(this.handleWorkItemCallBack, item);
				}
			}
			this.WaitAll(-1, false);
		}

		private bool WaitAll(int millisecondsTimeout, bool exitContext)
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

		private void ThrowIfDisposed()
		{
			if (this._done == null)
			{
				throw new ObjectDisposedException(base.GetType().Name);
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

		public void Dispose()
		{
			if (this._done != null)
			{
				((IDisposable)this._done).Dispose();
				this._done = null;
			}
		}

		private readonly SimplePool<QueuedCallback> queuedCallbackPool = new SimplePool<QueuedCallback>(4);

		private readonly SimplePool<LoopState<T>> loopStatePool = new SimplePool<LoopState<T>>(4);

		private int _remainingWorkItems = 1;

		private ManualResetEvent _done = new ManualResetEvent(false);

		private WaitCallback handleWorkItemCallBack;

		private WaitCallback handleLoopCallBack;
	}
}
