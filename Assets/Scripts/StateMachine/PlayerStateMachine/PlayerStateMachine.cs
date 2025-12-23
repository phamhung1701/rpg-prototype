using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field : SerializeField] public InputReader InputReader { get; private set; }
    [field : SerializeField] public CharacterController controller { get; private set; }
    [field : SerializeField] public float movementSpeed { get; private set; }
    [field : SerializeField] public float RotationDamping { get; private set; }
    [field : SerializeField] public Animator animator { get; private set; }
    [field : SerializeField] public Targeter targeter { get; private set; }
    [field : SerializeField] public ForceReceiver forceReceiver { get; private set; }
    [field : SerializeField] public Attack[] Attacks { get; private set; }
    [field : SerializeField] public Damage Damage { get; private set; }

    public Transform MainCamera { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }
}
