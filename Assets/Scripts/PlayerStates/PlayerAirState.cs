using UnityEngine;

public class PlayerAirState : AbstractPlayerState
{
    private Rigidbody2D _rigidbody2D = null;
    public PlayerAirState(PlayerStateMachine PlayerStateMachine, Animator Animator,Rigidbody2D Rigidbody2D) : base(PlayerStateMachine, Animator) {
        _rigidbody2D = Rigidbody2D;
    }

    public override void Enter() {
    }
    public override void Tick() {
        Animator.SetFloat("VelocityY", _rigidbody2D.velocity.y);
        if (_rigidbody2D.velocity.y >= 0.00f)
            return;

        int Count = Physics2D.OverlapCircleNonAlloc(PlayerStateMachine.GroundCheckTransform.position, PlayerStateMachine.GroundCheckRadius,
            new Collider2D[1], PlayerStateMachine.GroundLayer);

        if (Count.Equals(0))
            return;

        PlayerStateMachine.SetGroundState(PlayerStateMachine.GroundState);
    }

    public override void FixedTick() {
    }

    public override void LateTick() {
    }
    public override void Exit() {
    }

}