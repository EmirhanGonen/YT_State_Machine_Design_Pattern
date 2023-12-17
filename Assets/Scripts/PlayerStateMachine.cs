using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.00f;
    [SerializeField] private float _jumpPower = 0.00f;

    [SerializeField] private Transform _groundCheckTransform = null;
    [SerializeField] private float _groundCheckRadius = 0.00f;
    [SerializeField] private LayerMask _groundLayer = 0;

    public float MoveSpeed => _moveSpeed;
    public float JumpPower => _jumpPower;

    public Transform GroundCheckTransform => _groundCheckTransform;
    public float GroundCheckRadius => _groundCheckRadius;
    public LayerMask GroundLayer => _groundLayer;


    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerGroundState GroundState { get; private set; }
    public PlayerAirState AirState { get; private set; }


    private AbstractPlayerState _currentMoveState = null;
    private AbstractPlayerState _currentGroundState = null;

    private Animator _animator = null;
    private Rigidbody2D _rigidbody2D = null;
    private SpriteRenderer _spriteRenderer = null;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheckTransform.position, _groundCheckRadius);
    }

    private void Awake() {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        IdleState = new PlayerIdleState(this, _animator);
        MoveState = new PlayerMoveState(this, _animator, _rigidbody2D, _spriteRenderer);

        GroundState = new PlayerGroundState(this, _animator, _rigidbody2D);
        AirState = new PlayerAirState(this, _animator, _rigidbody2D);

        SetMoveState(IdleState);
        SetGroundState(GroundState);
    }

    private void Update() {
        _currentMoveState?.Tick();
        _currentGroundState?.Tick();
    }
    private void FixedUpdate() {
        _currentMoveState?.FixedTick();
        _currentGroundState?.FixedTick();
    }
    private void LateUpdate() {
        _currentMoveState?.LateTick();
        _currentGroundState?.LateTick();
    }

    public void SetMoveState(AbstractPlayerState NewState) {
        _currentMoveState?.Exit();
        _currentMoveState = NewState;
        _currentMoveState?.Enter();
    }
    public void SetGroundState(AbstractPlayerState NewState) {
        _currentGroundState?.Exit();
        _currentGroundState = NewState;
        _currentGroundState?.Enter();
    }
}