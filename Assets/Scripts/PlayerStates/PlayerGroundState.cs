using UnityEngine;

public class PlayerGroundState : AbstractPlayerState
{
    private Rigidbody2D _rigidbody2D = null;
    public PlayerGroundState(PlayerStateMachine PlayerStateMachine, Animator Animator, Rigidbody2D Rigidbody2D) : base(PlayerStateMachine, Animator) {
        _rigidbody2D = Rigidbody2D;
    }

    public override void Enter() {
        Animator.SetBool("IsGround", true);
    }

    public override void Tick() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, PlayerStateMachine.JumpPower);
            PlayerStateMachine.SetGroundState(PlayerStateMachine.AirState);
        }
    }

    public override void FixedTick() {
    }

    public override void LateTick() {
    }
    public override void Exit() {
        Animator.SetBool("IsGround", false);
    }
}