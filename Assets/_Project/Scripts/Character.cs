namespace Lake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 3.0f;
        private float currentMovingDirection = 0.0f;
        
        private bool isSprinting = false;
        private bool isMoving = false;
        
        [SerializeField]
        private float turningSpeed = 0.5f * Mathf.PI;
        private float currentTurningDirection = 0.0f;

        [SerializeField]
        private Animator animator = default;

        private float projectedForwardAngle = 90.0f * Mathf.Deg2Rad;
        private Vector3 projectedForward;
        private Quaternion projectedRotation;

        private PlayerController playerController = default;
        private CharacterController characterController = default;

        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public PlayerController PlayerController { get => playerController; }

        public CharacterController CharacterController { get => characterController; }

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

        private void LateUpdate()
        {
            float currentSpeed = this.movementSpeed * this.currentMovingDirection * (this.isSprinting ? 2.0f : 1.0f);
            
            this.characterController.Move(this.transform.forward * currentSpeed * Time.deltaTime);
            this.animator.SetFloat("MovementSpeed", currentSpeed);
            this.animator.SetBool("IsMoving", Mathf.Abs(currentSpeed) > 0);

            float currentTurningSpeed = this.currentTurningDirection * this.turningSpeed;

            this.projectedForwardAngle += currentTurningSpeed * Time.deltaTime;
            this.projectedForward = new Vector3(Mathf.Cos(this.projectedForwardAngle), 0.0f, Mathf.Sin(this.projectedForwardAngle));
            this.projectedRotation = Quaternion.LookRotation(this.projectedForward, Vector3.up);
            this.animator.SetFloat("TurningSpeed", currentTurningSpeed);
            this.animator.SetBool("IsTurning", Mathf.Abs(currentTurningSpeed) > 0);

            this.currentMovingDirection = 0.0f;
            this.currentTurningDirection = 0.0f;

            this.isSprinting = false;

            this.transform.forward = Vector3.Lerp(this.transform.forward, this.projectedForward, 0.1f);
        }

        private void Awake()
        {
            this.playerController = new PlayerController(this);
            this.characterController = this.GetComponent<CharacterController>();
        }
    }

}