using System;
using System.Threading;
using System.Collections.Generic;

public class MultiThreader
{
    List<Job> _currentJobs = new List<Job>();

    public void ManualUpdate()
    {
        InvokeMainThreadCallbacks();
    }

    void InvokeMainThreadCallbacks()
    {
        for (int i = _currentJobs.Count - 1; i >= 0; i--)
        {
            Job currentJob = _currentJobs[i];
            if (!currentJob.jobThread.IsAlive)
            {
                currentJob.mainThreadCallback(currentJob.result);
                _currentJobs.RemoveAt(i);
            }
        }
    }

    public void DoThreaded(Func<object> inMethodToThread, Action<object> inMainThreadCallback)
    {
        Job newJob = new Job();

        lock (_currentJobs)
            _currentJobs.Add(newJob);

        newJob.methodToThread     = inMethodToThread;
        newJob.mainThreadCallback = inMainThreadCallback;

        newJob.jobThread = new Thread(newJob.Execute);
        newJob.jobThread.Start();
    }


    class Job
    {
        public Func<object> methodToThread;       // The method that needs to be threaded. It returns an object.
        public Action<object> mainThreadCallback; // The callback that should run when the work is complete. It takes an object as a parameter.

        public Thread jobThread;                  // The thread which the threaded method is run on

        public object result;                     // The result of the thread. This will be set whenever the job is done.

        public void Execute() => result = methodToThread();
    }
}