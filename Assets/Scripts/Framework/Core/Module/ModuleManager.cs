using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public class ModuleManager : Singleton<ModuleManager>
    {
        private Dictionary<string, IModule> _modules;

        public ModuleManager()
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
            foreach (var module in _modules.Values)
            {
                module.Shutdown();
            }

            _modules.Clear();
        }


        public T CreateModule<T>(string name) where T : class, IModule, new()
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

        public T GetModule<T>(string name) where T : class, IModule
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
    }
}
