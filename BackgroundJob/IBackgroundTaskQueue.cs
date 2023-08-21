﻿namespace BackGroundServiceOnDemand.BackgroundJob
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem);
        Task<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }
}
