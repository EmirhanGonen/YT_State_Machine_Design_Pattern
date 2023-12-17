using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.00f;
    [SerializeField] private float _jumpPower = 0.00f;


    private bool _isGround = true;

    private Rigidbody2D _rigidbody2D = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;
    private float _horizontalInput = 0.00f;

    public Rigidbody2D Rigidbody => _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        _rigidbody2D.freezeRotation = true;
    }

    private void Update() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _spriteRenderer.flipX = _horizontalInput != 0.00f ? Mathf.Sign(_horizontalInput).Equals(-1.00f) : _spriteRenderer.flipX;

        _animator.SetBool("IsMoving", !_horizontalInput.Equals(0.00f));
        _animator.SetFloat("VelocityY", _rigidbody2D.velocity.y);
        _animator.SetBool("IsGround", _isGround);

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
            Jump();
    }
    private void Jump() {
        _rigidbody2D.AddForce(Vector2.up * _jumpPower);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        _isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Ground"))
            return;

        _isGround = false;
    }

    private void FixedUpdate() {
        _rigidbody2D.velocity = new Vector2(_horizontalInput * Time.fixedDeltaTime * _moveSpeed, _rigidbody2D.velocity.y);
    }
}