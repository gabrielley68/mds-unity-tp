using System;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator))]
    public class MoveCharacter : MonoBehaviour
    {
        private const string ROLL = "roll";

        [Tooltip("Speed in Unit per second")] public float speed = 5f;

        private SpriteRenderer spriteRenderer;
        private SpritesheetAnimator animator;

        // COOLDOWNS
        [Tooltip("Cooldown of a roll in seconds")]
        public float rollCooldownDuration = 1;

        private float rollCooldown = 0;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<SpritesheetAnimator>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 acceleration = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                acceleration += Vector3.up;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                acceleration += Vector3.down;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                acceleration += Vector3.left;
                spriteRenderer.flipX = true;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                acceleration += Vector3.right;
                spriteRenderer.flipX = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && rollCooldown <= 0)
            {
                animator.Play(Anims.Roll);
                rollCooldown = rollCooldownDuration;
            }

            if (animator.CurrentAnimation.name != Anims.Roll || animator.LoopCount >= 1)
            {
                if (acceleration.magnitude > 0)
                {
                    animator.Play(Anims.Run);
                }
                else
                {
                    animator.Play(Anims.Iddle);
                }
            }
    
            transform.position += speed * acceleration.normalized * Time.deltaTime;
            rollCooldown -= Time.deltaTime;
        }
    }
}