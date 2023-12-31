using System;
using UnityEngine;

namespace MyPlayerController
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        public bool reverseGravity;

        private float horizontalRG;
        public float reverseGravityPower = -5;
        public float speedRG = 14f;
        public float jumpingPowerRG = 10f;

        private bool isFacingRight = false;

        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void Update()
        {
            checkGravity();

            _time += Time.deltaTime;
            GatherInput();

            //if (Input.GetButtonDown("SwitchGrav"))
            //{
            //    switchGravity();
            //}

            if (reverseGravity)
            {
                horizontalRG = Input.GetAxisRaw("Horizontal");

                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, -jumpingPowerRG);
                }
            }

            FlipPlayer();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }

        private void FixedUpdate()
        {
            if (reverseGravity)
            {
                _rb.gravityScale = reverseGravityPower;
            }
            else
            {
                _rb.gravityScale = 20;
            }

            if (!reverseGravity)
            {
                CheckCollisions();

                HandleJump();
                HandleDirection();
                HandleGravity();
                HandleDirection();

                ApplyMovement();
            }
            else
            {
                HandleMovementRG();
            }
        }

        #region Collisions

        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Max(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }


        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }

        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {

            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }

        }

        private void HandleMovementRG()
        {
            _rb.velocity = new Vector2(horizontalRG * speedRG, _rb.velocity.y);
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);            
        }

        public void switchGravity(int normal = 2)
        {   
            if (normal == 0)
            {
                reverseGravity = false;
            }else if (normal == 1)
            {
                reverseGravity = true;
            } else
            {
                reverseGravity = !reverseGravity;
            }
        }

        private void FlipPlayer()
        {
            if (reverseGravity)
            {
                if (isFacingRight && horizontalRG < 0 || !isFacingRight & horizontalRG > 0)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }
            } else
            {
                if (isFacingRight && _frameVelocity.x < 0 || !isFacingRight & _frameVelocity.x > 0)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }
            }
        }

        private void checkGravity()
        {
            if (transform.localPosition.y < 3)
            {
                switchGravity(0);
            }
            else if (transform.localPosition.y > 3 && transform.localPosition.y < 8)
            {
                switchGravity(1);
            } else if (transform.localPosition.y >= 8 && transform.localPosition.y < 13)
            {
                switchGravity(0);
            }
            else if (transform.localPosition.y > 13 && transform.localPosition.y < 19)
            {
                switchGravity(1);
            }
            else if (transform.localPosition.y > 19 && transform.localPosition.y < 23)
            {
                switchGravity(0);
            }
            else if (transform.localPosition.y > 23 && transform.localPosition.y < 28)
            {
                switchGravity(1);
            }
            else if (transform.localPosition.y > 28 && transform.localPosition.y < 33)
            {
                switchGravity(0);
            }
            else if (transform.localPosition.y > 33 && transform.localPosition.y < 38)
            {
                switchGravity(1);
            }
        }
        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}