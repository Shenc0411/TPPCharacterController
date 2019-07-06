namespace Lake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController), typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 3.0f;
        private float currentSpeed = 0.0f;
        private float currentMovingDirection = 0.0f;
        
        private bool isSprinting = false;
        private bool isMoving = false;
        
        [SerializeField]
        private float turningSpeed = 0.5f * Mathf.PI;
        private float currentTurningSpeed = 0.0f;
        private float currentTurningDirection = 0.0f;

        [SerializeField]
        private float jumpSpeed = 20.0f;
        private bool shouldJump = false;
        private Vector3 verticalSpeed = Vector3.zero;

        [SerializeField]
        private Animator animator = default;

        private float projectedForwardAngle = 90.0f * Mathf.Deg2Rad;
        private Vector3 projectedForward;
        private Quaternion projectedRotation;

        private PlayerController playerController = default;
        private CharacterController characterController = default;
        private new Rigidbody rigidbody = default;

        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public PlayerController PlayerController { get => playerController; }

        public CharacterController CharacterController { get => characterController; }

        public Rigidbody Rigidbody { get => rigidbody; }

        public void MoveForward()
        {
            this.currentMovingDirection += 1.0f;
        }

        public void MoveBackward()
        {
            this.currentMovingDirection -= 1.0f;
        }

        public void RotateLeft()
        {
            this.currentTurningDirection += 1.0f;
        }

        public void RotateRight()
        {
            this.currentTurningDirection -= 1.0f;
        }

        public void Sprint()
        {
            this.isSprinting = true;
        }

        public void Jump()
        {
            this.shouldJump = true;
        }

        private void LateUpdate()
        {
            

            if (this.characterController.isGrounded)
            {
                currentSpeed = this.movementSpeed * this.currentMovingDirection * (this.isSprinting ? 2.0f : 1.0f);
                currentTurningSpeed = this.currentTurningDirection * this.turningSpeed;
                verticalSpeed.y = -1.0f;
                if (shouldJump)
                {
                    verticalSpeed.y += this.jumpSpeed;
                    this.animator.SetTrigger("Jump");
                }
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0.0f, 1.0f * Time.deltaTime);
                currentTurningSpeed = Mathf.Lerp(currentTurningSpeed, 0.0f, 0.2f);
                verticalSpeed.y -= 9.8f * Time.deltaTime;
            }

            this.characterController.Move((this.transform.forward * currentSpeed + verticalSpeed) * Time.deltaTime);
            this.animator.SetFloat("MovementSpeed", currentSpeed);
            this.animator.SetBool("IsMoving", Mathf.Abs(currentSpeed) > 0);

            this.projectedForwardAngle += currentTurningSpeed * Time.deltaTime;
            this.projectedForward = new Vector3(Mathf.Cos(this.projectedForwardAngle), 0.0f, Mathf.Sin(this.projectedForwardAngle));
            this.projectedRotation = Quaternion.LookRotation(this.projectedForward, Vector3.up);
            this.animator.SetFloat("TurningSpeed", currentTurningSpeed);
            this.animator.SetBool("IsTurning", Mathf.Abs(currentTurningSpeed) > 0);
            this.animator.SetBool("IsGrounded", this.characterController.isGrounded);

            this.currentMovingDirection = 0.0f;
            this.currentTurningDirection = 0.0f;

            this.isSprinting = false;
            this.shouldJump = false;

            this.transform.forward = Vector3.Lerp(this.transform.forward, this.projectedForward, 0.1f);
        }

        private void Awake()
        {
            this.playerController = new PlayerController(this);
            this.characterController = this.GetComponent<CharacterController>();
            this.rigidbody = this.GetComponent<Rigidbody>();
        }
    }

}