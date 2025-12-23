using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> TargetDestroyedEvent;

    private void OnDestroy()
    {
        TargetDestroyedEvent?.Invoke(this);
    }
}
