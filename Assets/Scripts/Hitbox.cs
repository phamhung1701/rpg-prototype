using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private GameObject RightHandHitbox;
    [SerializeField] private GameObject LeftHandHitbox;
    [SerializeField] private GameObject LeftLegHitbox;
    [SerializeField] private GameObject RightLegHitbox;
    [SerializeField] private GameObject RightElbowHitbox;

    private void EnableRightHand()
    {
        RightHandHitbox.SetActive(true);
    }

    private void DisableRightHand()
    {
        RightHandHitbox.SetActive(false);
    }

    private void EnableLeftHand()
    {
        LeftHandHitbox.SetActive(true);
    }

    private void DisableLeftHand()
    {
        LeftHandHitbox.SetActive(false);
    }
    private void EnableRightElbow()
    {
        RightElbowHitbox.SetActive(true);
    }

    private void DisableRightElbow()
    {
        RightElbowHitbox.SetActive(false);
    }

    private void EnableLeftLeg()
    {
        LeftLegHitbox.SetActive(true);
    }

    private void DisableLeftLeg()
    {
        LeftLegHitbox.SetActive(false);
    }

    private void EnableRightLeg()
    {
        RightLegHitbox.SetActive(true);
    }

    private void DisableRightLeg()
    {
        RightLegHitbox.SetActive(false);
    }
}
