using UnityEngine;
using System.Collections;

public static class ParallelProcesses
{
    const float epsilon = 1e-4f;

    public delegate void Action();
    public delegate void ActionForFrame(float t);

    delegate float TimeAccess();

    static IEnumerator RunForSeconds(float time, ActionForFrame action, TimeAccess timeGetter)
    {
        float start = timeGetter();
        bool ready = false;
        while (!ready)
        {
            if (timeGetter() <= start + time)
            {
                action(timeGetter() - start);
            } else
            {
                ready = true;
                action(time);
            }
            yield return null;
        }
    }

    static IEnumerator WaitForRealSeconds(float delay, TimeAccess timeGetter)
    {
        float start = timeGetter();
        while (timeGetter() <= start + delay + epsilon)
        {
            yield return null;
        }
    }

    static IEnumerator RunAfterTime(float delay, MonoBehaviour objectExecuting, Action action, TimeAccess timeGetter)
    {
        yield return objectExecuting.StartCoroutine(WaitForRealSeconds(delay, timeGetter));
        action();
    }

    public static Coroutine RunAfterRealDelay(this MonoBehaviour objectExecuting, float delay, Action action)
    {
        return objectExecuting.StartCoroutine(RunAfterTime(delay, objectExecuting, action, ()=>{ return Time.realtimeSinceStartup; }));
    }

    public static Coroutine RunForGivenTime(this MonoBehaviour objectExecuting, float delay, ActionForFrame action)
    {
        return objectExecuting.StartCoroutine(RunForSeconds(delay, action, () => { return Time.realtimeSinceStartup; }));
    }

    public static Coroutine RunAfterLevelDelay(this MonoBehaviour objectExecuting, float delay, Action action)
    {
        return objectExecuting.StartCoroutine(RunAfterTime(delay, objectExecuting, action, () => { return Time.time; }));
    }

    public static Coroutine RunForGivenLevelTime(this MonoBehaviour objectExecuting, float delay, ActionForFrame action)
    {
        return objectExecuting.StartCoroutine(RunForSeconds(delay, action, () => { return Time.time; }));
    }

}
