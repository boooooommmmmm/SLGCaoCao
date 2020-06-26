using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ModuleManager
    {
        private static ModuleManager instance = new ModuleManager();

        public static ModuleManager Get()
        {
            return instance;
        }

        private Dictionary<string, IModule> _modules;

        private ModuleManager()
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

        void Shutdown()
        {
            foreach (var module in _modules.Values)
            {
                module.Shutdown();
            }

            _modules.Clear();
        }
    }
}
