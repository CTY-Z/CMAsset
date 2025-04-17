using System;
using UnityEngine;

namespace CMAsset.Runtime
{
    public abstract class BaseHandler : IReference, IDisposable
    {
        public string Name { get; private set; }

        public float Porgress
        {
            get
            {
                switch(State)
                {
                    case HandlerState.InValid:
                        return 0;
                    case HandlerState.Doing:
                        //TODO
                        return 1;
                    case HandlerState.Success:
                    case HandlerState.Failed:
                        return 1;

                    default:
                        return 0;
                }
            }
        }


        public HandlerState State {  get; private set; }

        public void Dispose()
        {

        }

        public virtual void RefClear()
        {

        }
    }
}

