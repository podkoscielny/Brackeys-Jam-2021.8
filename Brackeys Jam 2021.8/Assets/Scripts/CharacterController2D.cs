using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 1)] [SerializeField] float crouchSpeed = .36f;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] bool airControl = false;

    [Header("Collision")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform ceilingCheck;

    [Header("Rigidbody")]
    [SerializeField] Rigidbody2D characterRb;

    [Header("Animation")]
    [SerializeField] AnimationController animationController;

    public bool IsGrounded { get; private set; }
    private bool _facingRight = true;
    private bool _isCrouching = false;
    private bool _hasGroundCollisionEntered = false;
    private bool _isCheckingCeiling = false;
    private bool _canResetVelocity = true;
    private float _movementMultiplier = 1f;
    private const float GROUNDED_RADIUS = .15f;
    private const float CEILING_RADIUS = .2f;

    public void Move(float moveAmount)
    {
        //only control the player if grounded or airControl is turned on
        if ((IsGrounded || airControl) && moveAmount != 0)
        {
            if (_isCheckingCeiling) CeilingCheck(); //check if the player can stand up after releasing crouch key

            moveAmount *= _movementMultiplier;
            characterRb.velocity = new Vector2(moveAmount, characterRb.velocity.y);

            if (moveAmount > 0 && !_facingRight || moveAmount < 0 && _facingRight)
                FlipCharacter();

            _canResetVelocity = true;
        }
        else
        {
            if (_canResetVelocity)
            {
                characterRb.velocity = new Vector2(0, characterRb.velocity.y);
                _canResetVelocity = false;
            }
        }
    }

    public void Crouch(bool isCrouching)
    {
        if (isCrouching)
        {
            _movementMultiplier = crouchSpeed;
            _isCrouching = true;
            animationController.OnCrouching(true); //Start Crouching animation
        }
        else
        {
            _isCheckingCeiling = true;

        }
    }

    public void Jump()
    {
        if (IsGrounded && !_isCrouching)
        {
            IsGrounded = false;
            _hasGroundCollisionEntered = false;
            animationController.OnJumping(); //Start jump animation
            characterRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FlipCharacter()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void GroundCheck()
    {
        if (IsGroundBeneath())
        {
            IsGrounded = true;
            animationController.OnLanding(); //Set Landing animation
        }
    }

    bool IsGroundBeneath() => Physics2D.OverlapCircle(groundCheck.position, GROUNDED_RADIUS, groundMask) != null;

    bool IsObjectsMaskSameAsGrounds(GameObject obj) => (groundMask.value & (1 << obj.layer)) > 0;

    void CeilingCheck()
    {
        _isCrouching = Physics2D.OverlapCircle(ceilingCheck.position, CEILING_RADIUS, groundMask) != null;
        if (!_isCrouching)
        {
            _movementMultiplier = 1f;
            _isCheckingCeiling = false;
            animationController.OnCrouching(false); //Stop Crouching animation
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsObjectsMaskSameAsGrounds(collision.gameObject))
        {
            _hasGroundCollisionEntered = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!IsGrounded && _hasGroundCollisionEntered) GroundCheck();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        _hasGroundCollisionEntered = false;

        if (IsGrounded && IsObjectsMaskSameAsGrounds(collision.gameObject) && !IsGroundBeneath()) //check if collisions layer is the same as grounds
        {
            IsGrounded = false;
            animationController.OnFalling(); //Set falling animation
        }
    }
}
