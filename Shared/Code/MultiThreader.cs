using System;
using System.Threading;
using System.Collections.Generic;

public class MultiThreader
{
    List<IJob> _currentJobs = new List<IJob>();

    public void ManualUpdate()
    {
        InvokeMainThreadCallbacks();
    }

    void InvokeMainThreadCallbacks()
    {
        for (int i = _currentJobs.Count - 1; i >= 0; i--)
        {
            IJob currentJob = _currentJobs[i];
            if (!currentJob.GetThread().IsAlive)
            {
                currentJob.MainThreadCallback();
                _currentJobs.RemoveAt(i);
            }
        }
    }

    public void DoThreaded<T>(Func<T> inMethodToThread, Action<T> inMainThreadCallback)
    {
        Job<T> newJob = new Job<T>();

        lock (_currentJobs)
            _currentJobs.Add(newJob);

        newJob.methodToThread     = inMethodToThread;
        newJob.mainThreadCallback = inMainThreadCallback;

        newJob.jobThread = new Thread(newJob.Execute);
        newJob.jobThread.Start();

        
    }

    interface IJob
    {
        Thread GetThread();
        void Execute();
        void MainThreadCallback();
    }

    class Job<T> : IJob
    {
        public Func<T>   methodToThread;     // The method that needs to be threaded. It returns an object.
        public Action<T> mainThreadCallback; // The callback that should run when the work is complete. It takes an object as a parameter.

        public Thread jobThread;             // The thread which the threaded method is run on

        public T result;                     // The result of the thread. This will be set whenever the job is done.


        public Thread GetThread() => jobThread;

        public void Execute() => 
            result = methodToThread();

        public void MainThreadCallback() =>
            mainThreadCallback(result);
    }
}