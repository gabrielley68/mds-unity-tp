using System;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class BallMoveVisual : MonoBehaviour
    {

        private SpriteRenderer spriteRenderer;
        private SpritesheetAnimator animator;
        private Rigidbody2D body;
        
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var velocity = body.velocity;
            this.body.rotation = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
            animator.animationSpeed = body.velocity.magnitude * 10;
        }
    }
}