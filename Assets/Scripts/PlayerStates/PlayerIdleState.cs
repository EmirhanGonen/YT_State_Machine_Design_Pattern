using UnityEngine;

public class PlayerIdleState : AbstractPlayerState
{
    public PlayerIdleState(PlayerStateMachine PlayerStateMachine, Animator Animator) : base(PlayerStateMachine, Animator) {
    }

    public override void Enter() {
        Animator.SetBool("IsMoving", false);
    }
    public override void Tick() {
        if (Input.GetAxisRaw("Horizontal").Equals(0.00f))
            return;

        PlayerStateMachine.SetMoveState(PlayerStateMachine.MoveState);
    }


    public override void FixedTick() {
    }

    public override void LateTick() {
    }
    public override void Exit() {
    }
}