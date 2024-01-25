using System;
using System.Collections;
using UnityEngine;
using Utility.Singletons;

namespace Utility.Time
{
    public class Timer : SingletonMonoBehaviour<Timer>
    {
        public void InSeconds(float seconds, Action action)
        {
            StartCoroutine(StartTimer(seconds, action));
        }

        public void InSeconds(float seconds, Action action, TimerCancellationToken cancellationToken)
        {
            StartCoroutine(StartTimer(seconds, action, cancellationToken));
        }

        private static IEnumerator StartTimer(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }

        private static IEnumerator StartTimer(float seconds, Action action, TimerCancellationToken cancellationToken)
        {
            while (seconds > 0)
            {
                if (cancellationToken.Cancel)
                    yield break;

                yield return new WaitForFixedUpdate();
                seconds -= UnityEngine.Time.fixedTime;
            }

            action.Invoke();
        }
    }
}