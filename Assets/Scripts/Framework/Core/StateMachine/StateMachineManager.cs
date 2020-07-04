using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public class StateMachineManager
    {
        private static StateMachineManager instance = new StateMachineManager();

        public static StateMachineManager Get()
        {
            return instance;
        }

        private NTree<string, StateMachineBase> states;
        private NTree<string, StateMachineBase> currentState;

        private StateMachineManager()
        {
            states = new NTree<string, StateMachineBase>("Root", new RootStateMachine(), null);
            currentState = states;

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
            //exit and clear states
        }

        public T CreateState<T>(string statetName, string parentState = "Root") where T : StateMachineBase, IState, new()
        {
            if (states.GetChild(statetName) != null || states.GetDeepChild(statetName) != null)
            {
                throw new SystemException(string.Format("State with name {0} already exists", statetName));
            }

            if (!parentState.Equals("Root") && (states.GetChild(parentState) == null || states.GetDeepChild(parentState) == null))
            {
                throw new SystemException(string.Format("Parent State with name {0} not exists", parentState));
            }

            var state = new T();
            if (parentState.Equals("Root"))
            {
                states.AddChild(statetName, state);
            }
            else
            {
                var _parentState = states.GetDeepChild(parentState);
                _parentState.AddChild(statetName, state);
            }

            return state;
        }


        public void SwitchState(string name)
        {
            //check if had this state
            if (states.GetChild(name) == null && states.GetDeepChild(name) == null)
                throw new SystemException(string.Format("State with name {0} not exists", name));

            if (currentState.Name.Equals("Root"))
                SwitchToChild(name);
            else
            {
                SwitchToBrother(name);
            }
        }

        public void EnterState()
        { }

        public void ExitState(string name)
        { }

        public void SwitchToParent()
        {
            //check if had parent state
            if (currentState.Name.Equals("Root"))
                throw new SystemException("Root state cannot switch to parent state!");
            else
            {
                currentState.Data.Exit();
                currentState = currentState.Parent;
                //no need enter to parent state
            }
        }

        public void SwitchToBrother(string name)
        {
            if (currentState.Name.Equals("Root"))
                throw new SystemException("Root state cannot switch to brother state!");

            var _parentState = currentState.Parent;
            if (_parentState.GetChild(name) != null)
            {
                currentState.Data.Exit();
                currentState = _parentState.GetChild(name);
                currentState.Data.Enter();
            }
            else
                throw new SystemException(String.Format("Switch to brother state [{0}] failed!", name));
        }

        public void SwitchToChild(string name)
        {
            if (currentState.GetChild(name) != null)
            {
                //switch to child state, no need to exit state
                currentState = currentState.GetChild(name);
                currentState.Data.Enter();
            }
            else
                throw new SystemException(string.Format("Cannot enter to state [{0}] ", name));
        }
    }


    class NTree<String, T>
    {
        private T data;
        private T parentData;
        private String name;
        private LinkedList<NTree<String, T>> children;
        private NTree<String, T> parent;

        public T Data { get { return data; } }
        public T ParentData { get { return parentData; } }
        public String Name { get { return name; } }
        public NTree<String, T> Parent { get { return parent; } }

        public NTree(String name, T data, NTree<String, T> parent)
        {
            if (parent != null)
            {
                this.data = data;
                parentData = parent.Data;
                this.name = name;
                children = new LinkedList<NTree<String, T>>();
                this.parent = parent;
            }
            else
            {
                this.data = data;
                //parentData = null;
                this.name = name;
                children = new LinkedList<NTree<String, T>>();
                //this.parent = parent;
            }
        }

        public void AddChild(String name, T data)
        {
            children.AddFirst(new NTree<String, T>(name, data, this));
        }

        public NTree<String, T> GetChild(int i)
        {
            foreach (NTree<String, T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public NTree<String, T> GetChild(String name)
        {
            foreach (NTree<String, T> n in children)
                if (n.name.Equals(name))
                    return n;
            return null;
        }

        public NTree<String, T> GetDeepChild(String name)
        {
            foreach (NTree<String, T> n in children)
            {
                if (n.name.Equals(name))
                {
                    return n;
                }
                else
                {
                    var res = n.GetDeepChild(name);
                    if (res != null)
                        return res;
                }
            }

            return null;
        }

        public void RemoveChild(String name)
        {
            foreach (NTree<String, T> n in children)
                if (n.name.Equals(name))
                    children.Remove(n);
        }

        public void RemoveAllChildren()
        {
            children.Clear();
        }
    }
}