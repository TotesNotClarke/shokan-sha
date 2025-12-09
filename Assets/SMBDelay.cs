using UnityEngine;
using System.Collections;

public class SMBDelayHelper : MonoBehaviour
{
    public void RunAfterDelay(float delay, System.Action action)
    {
        StartCoroutine(DelayedAction(delay, action));
    }

    IEnumerator DelayedAction(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
