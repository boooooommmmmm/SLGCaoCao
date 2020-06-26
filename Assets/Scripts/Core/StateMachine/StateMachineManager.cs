using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public class StateMachineManager
    {
        private static StateMachineManager instance = new StateMachineManager();
        private

        public static StateMachineManager Get()
        {
            return instance;
        }

        private Dictionary<string, IModule> _modules;

        private StateMachineManager()
        {
            _modules = new Dictionary<string, IModule>();

            var kernel = GameKernel.GetInstance();
            if (kernel == null)
            {
                throw new SystemException("Access module manager before application start.");
            }

            // application quits
            kernel.OnOnDestroy += delegate ()
            {
                Shutdown();
            };
        }

        void Shutdown()
        {
        }

        public T CreateState<T>(string statetName, string parentState) where T : class, IModule, new()
        {

        }


        public void EnterState(string name)
        {
            if (_modules.ContainsKey(name))
            {
                throw new SystemException(string.Format("Module with name {0} already exists", name));
            }

            var module = new T();
            _modules.Add(name, module);

            // module init
            module.Start();

            return module;
        }

        public void ExitState(string name)
        {
            IModule module = null;
            if (_modules.TryGetValue(name, out module) && module != null)
            {
                if (module is T)
                {
                    return (module as T);
                }
                else
                {
                    throw new SystemException(string.Format("Module type mismatch, name : {0}, type : {1}", name, typeof(T)));
                }
            }
            else
            {
                return null;
            }
        }

        public void SwitchState()
        { }

        public void SwitchToParent()
        { }

        public void SwitchToChild()
        { }
    }


    class NTree<T>
    {
        private T data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
            this.data = data;
            children = new LinkedList<NTree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new NTree<T>(data));
        }

        public NTree<T> GetChild(int i)
        {
            foreach (NTree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }
    }
}