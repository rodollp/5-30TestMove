using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float gravity = -9.81f;

    [Header("Reference")]
    [SerializeField] Transform cameraTransform;

    CharacterController controller;

    Vector2 moveInput;
    float verticalVelocity;

    // ▶ Input 안정화용 (중복 입력 방지)
    bool jumpRequested;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        ApplyGravityAndJump();

        // ▶ 점프 요청 1회 처리 후 초기화 (폭주 방지 핵심)
        jumpRequested = false;
    }

    // =========================
    // 이동 (안정화 버전)
    // =========================
    void Move()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * moveInput.y + right * moveInput.x;

        Vector3 horizontalMove = moveDir * moveSpeed;
        Vector3 verticalMove = Vector3.up * verticalVelocity;

        Vector3 finalMove = horizontalMove + verticalMove;

        controller.Move(finalMove * Time.deltaTime);
    }

    // =========================
    // 중력 + 점프
    // =========================
    void ApplyGravityAndJump()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        if (jumpRequested && controller.isGrounded)
        {
            verticalVelocity = jumpPower;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    // =========================
    // Move Input (폭주 방지 핵심)
    // =========================
    public void OnMove(InputValue value)
    {
        // ▶ 값 클램프 (이상값 방지)
        Vector2 input = value.Get<Vector2>();

        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    // =========================
    // Jump Input (1회 요청 방식)
    // =========================
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpRequested = true;
        }
    }
}