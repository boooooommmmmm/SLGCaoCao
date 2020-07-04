using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    public class GameController : Singleton<GameController>
    {
        public void Invoke(Action a, float delay)
        {
            StartCoroutine(InvokeCoroutine(a, delay));
        }

        public IEnumerator InvokeCoroutine(Action a, float delay)
        {
            yield return new WaitForSeconds(delay);
            a.Invoke();
        }
    }
}