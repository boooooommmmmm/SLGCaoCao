using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IModule
    {
        void Start();
        void Shutdown();
    }

    public abstract class ModuleBase : IModule
    {
        public virtual void Start()
        {
        }

        public virtual void Shutdown()
        {
        }
    }
}
