using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CMAsset.Runtime.Task
{
    public abstract class BaseTask : IReference
    {
        public int ID { get; protected set; }

        public string Name { get; protected set; }

        public TaskRunner Owner { get; protected set; }

        public TaskGroup Group { get; protected set; }

        public TaskState State { get; set; }

        public virtual float Progress { get; }

        //TODO
        public BaseTask MainTask;

        protected readonly List<BaseTask> list_mergedTasks = new();
        public int mergeTaskCount => list_mergedTasks.Count;

        private bool isCancekFuncCalled;

        protected CancellationToken CancelToken { get; private set; }

        public bool IsCanceled => isCancekFuncCalled || (CancelToken != default && CancelToken.IsCancellationRequested);

        public bool IsAllCanceled
        {
            get
            {
                foreach (BaseTask mergedTask in MainTask.list_mergedTasks)
                {
                    if (!mergedTask.IsCanceled)
                        return false;
                }

                return true;
            }
        }

        #region Behaviour

        public abstract void Run();
        public abstract void Update();
        public virtual void Cancel()
        {
            isCancekFuncCalled = true;
        }

        #endregion

        public void MergeTask(BaseTask task)
        {
            list_mergedTasks.Add(task);
        }

        public virtual void OnPriorityChanged()
        {
        }

        public override string ToString()
        {
            return Name;
        }

        protected void CreateBase(TaskRunner owner, string name, CancellationToken token = default)
        {
            Owner = owner;
            //ID = ++Ta
            Name = name;
            CancelToken = token;
        }

        public void RefClear()
        {
            ID = default;
            //Name = default;
            Owner = default;
            Group = default;
            State = default;
            //TODO
            list_mergedTasks.Clear();
        }
    }
}

