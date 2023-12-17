using UnityEngine;

public class PlayerMoveState : AbstractPlayerState
{
    private Rigidbody2D _rigidbody2D = null;
    private SpriteRenderer _spriteRenderer = null;

    private float _horizontalInput = 0.00f;

    public PlayerMoveState(PlayerStateMachine PlayerStateMachine, Animator Animator, Rigidbody2D Rigidbody2D, SpriteRenderer SpriteRenderer) : base(PlayerStateMachine, Animator) {
        _rigidbody2D = Rigidbody2D;
        _spriteRenderer = SpriteRenderer;
    }


    public override void Enter() {
        Animator.SetBool("IsMoving", true);
    }

    public override void Tick() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        if (_horizontalInput.Equals(0.00f))
        {
            PlayerStateMachine.SetMoveState(PlayerStateMachine.IdleState);
            return;
        }
        _spriteRenderer.flipX = Mathf.Sign(_horizontalInput).Equals(-1.00f);
    }

    public override void FixedTick() {
        _rigidbody2D.velocity = new Vector2(_horizontalInput * Time.fixedDeltaTime * PlayerStateMachine.MoveSpeed, _rigidbody2D.velocity.y);
    }

    public override void LateTick() {
    }
    public override void Exit() {
        _rigidbody2D.velocity = new Vector2(0.00f, _rigidbody2D.velocity.y);
    }
}