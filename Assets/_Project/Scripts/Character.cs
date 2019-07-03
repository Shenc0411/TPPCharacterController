namespace Lake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Character : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed;

        private PlayerController playerController;

        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public PlayerController PlayerController { get => playerController; }

        // Bug: Diagonal movement speed is not normalized yet
        public void Move(Vector3 direction)
        {
            this.transform.position += direction.normalized * movementSpeed * Time.deltaTime;
        }

        private void Awake()
        {
            playerController = new PlayerController(this);
        }
    }

}