namespace Lake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed;
        private Vector3 movingDirection;


        private PlayerController playerController;

        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public PlayerController PlayerController { get => playerController; }

        public void Move(Vector3 direction)
        {
            this.movingDirection += direction.normalized;
        }

        private void LateUpdate()
        {
            this.transform.position += this.movingDirection.normalized * this.movementSpeed * Time.deltaTime;
            this.movingDirection = Vector3.zero;
        }

        private void Awake()
        {
            playerController = new PlayerController(this);
        }
    }

}