namespace Lake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 4.0f;
        private float currentSpeed = 0.0f;
        [SerializeField]
        private float rotationSpeed = 1.0f * Mathf.PI;
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
            this.currentSpeed += this.movementSpeed;
        }

        public void MoveBackward()
        {
            this.currentSpeed -= this.movementSpeed;
        }

        public void RotateLeft()
        {
            this.projectedForwardAngle += rotationSpeed * Time.deltaTime;
            this.projectedForward = new Vector3(Mathf.Cos(this.projectedForwardAngle), 0.0f, Mathf.Sin(this.projectedForwardAngle));
            this.projectedRotation = Quaternion.LookRotation(this.projectedForward, Vector3.up);
        }

        public void RotateRight()
        {
            this.projectedForwardAngle -= rotationSpeed * Time.deltaTime;
            this.projectedForward = new Vector3(Mathf.Cos(this.projectedForwardAngle), 0.0f, Mathf.Sin(this.projectedForwardAngle));
            this.projectedRotation = Quaternion.LookRotation(this.projectedForward, Vector3.up);
        }

        private void LateUpdate()
        {
            this.characterController.Move(this.transform.forward * this.currentSpeed * Time.deltaTime);
            this.animator.SetFloat("MovementSpeed", this.currentSpeed);
            this.currentSpeed = 0.0f;
            this.transform.forward = Vector3.Lerp(this.transform.forward, this.projectedForward, 0.1f);
        }

        private void Awake()
        {
            this.playerController = new PlayerController(this);
            this.characterController = this.GetComponent<CharacterController>();
        }
    }

}