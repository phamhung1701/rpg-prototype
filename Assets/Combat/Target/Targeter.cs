using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    [field : SerializeField] private CinemachineTargetGroup targetGroup;
    public List<Target> targets = new List<Target>();
    private Camera mainCamera;
    public Target currentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Target>(out Target target))
        {
            targets.Add(target);
            target.TargetDestroyedEvent += RemoveTarget;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent <Target>() == null)
        {
            return;
        }

        RemoveTarget(other.GetComponent<Target>());
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }
        currentTarget = null;
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;
        Vector2 center = new Vector2(0.5f, 0.5f);

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);
            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            {
                continue;
            }

            Vector2 DistanceToCenter = viewPos - center;
            if(DistanceToCenter.magnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = DistanceToCenter.magnitude;
            }
        }

        if (closestTarget != null) 
        {
            currentTarget = closestTarget;
            targetGroup.AddMember(currentTarget.transform, 1, 2);
            return true;
        }

        return false;
    }

    public void Cancel()
    {
        targetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            targetGroup.RemoveMember(target.transform);
            currentTarget = null;
        }

        target.TargetDestroyedEvent -= RemoveTarget;
        targets.Remove(target);
    }
}
