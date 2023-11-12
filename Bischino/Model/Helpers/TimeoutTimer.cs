using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bischino.Model.Helpers
{
    public class TimeoutTimer<T>
    {
        public event EventHandler<T> TimeoutEvent;
        public T Tag { get; }
        private readonly int _timeoutMs;
        private CancellationTokenSource _tokenSource;

        public bool IsEnabled { get; private set; }

        public TimeoutTimer(int timeoutMs, T tag)
        {
            Tag = tag;
            _timeoutMs = timeoutMs;
        }

        private async Task TimerRoutine(CancellationToken token)
        {        
            try
            {
                await Task.Delay(_timeoutMs, _tokenSource.Token);
                token.ThrowIfCancellationRequested();
                TimeoutEvent?.Invoke(this, Tag);
            }
            catch (TaskCanceledException) when (token.IsCancellationRequested)
            {
           
            }
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _ = TimerRoutine(_tokenSource.Token);
            IsEnabled = true;
        }

        public void Reset()
        {
            Stop(); 
            Start();
        }

        public void Stop()
        {
            var tokenSource = _tokenSource;
            _tokenSource = null;
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            IsEnabled = false;
        }
    }
}