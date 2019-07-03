namespace Lake
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 2.0f;


        private void Awake()
        {
            InputManager.Instance.RegisterKeyHoldCallBack(KeyCode.W, () => Move(this.transform.forward, moveSpeed));
            InputManager.Instance.RegisterKeyHoldCallBack(KeyCode.S, () => Move(-this.transform.forward, moveSpeed));
            InputManager.Instance.RegisterKeyHoldCallBack(KeyCode.A, () => Move(-this.transform.right, moveSpeed));
            InputManager.Instance.RegisterKeyHoldCallBack(KeyCode.D, () => Move(this.transform.right, moveSpeed));
        }

        private void Move(Vector3 direction, float amount)
        {
            this.transform.position += direction.normalized * amount * Time.deltaTime;
        }
    }

}