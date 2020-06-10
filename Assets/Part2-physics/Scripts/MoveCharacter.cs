using System;
using UnityEngine;

namespace Part2
{
    [RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
    public class MoveCharacter : MonoBehaviour
    {
        public enum PlayerControls
        {
            ZQSDF,
            ArrowAnd0
        }
        private const string ROLL = "roll";

        [Tooltip("Speed in Unit per second")] public float speed = 5f;

        private SpriteRenderer spriteRenderer;
        private SpritesheetAnimator animator;
        private Rigidbody2D body;

        public PlayerControls controls = PlayerControls.ArrowAnd0;
        public float ShootStrength = 5f;
        public ParticleSystem ShootFX;
        public ParticleSystem DirtFX;
        public AudioSource[] ShootSounds;

        // COOLDOWNS
        [Tooltip("Cooldown of a roll in seconds")]
        public float rollCooldownDuration = 1;
        private float rollCooldown = 0;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<SpritesheetAnimator>();
            body = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 vitesse = Vector2.zero;
            if (Input.GetKey(KeyCode.UpArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.Z) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.up;
            }

            if (Input.GetKey(KeyCode.DownArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.S) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.down;
            }

            if (Input.GetKey(KeyCode.LeftArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.Q) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.left;
                spriteRenderer.flipX = true;
            }

            if (Input.GetKey(KeyCode.RightArrow) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.D) && controls == PlayerControls.ZQSDF)
            {
                vitesse += Vector2.right;
                spriteRenderer.flipX = false;
            }

            if ((Input.GetKeyDown(KeyCode.Keypad1) && controls == PlayerControls.ArrowAnd0
            || Input.GetKey(KeyCode.F) && controls == PlayerControls.ZQSDF) && rollCooldown <= 0)
            {
                animator.Play(Anims.Roll);
                rollCooldown = rollCooldownDuration;
            }

            if (animator.CurrentAnimation.name != Anims.Roll || animator.LoopCount >= 1)
            {
                var dirtFx = DirtFX.emission;
                if (vitesse.magnitude > 0)
                {
                    animator.Play(Anims.Run);
                    dirtFx.enabled = true;
                }
                else
                {
                    animator.Play(Anims.Iddle);
                    dirtFx.enabled = false;
                }
            }

            rollCooldown -= Time.deltaTime;
            body.velocity = vitesse.normalized * speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.rigidbody.bodyType == RigidbodyType2D.Dynamic && animator.CurrentAnimation.name == Anims.Roll)
            {
                foreach (AudioSource audio in ShootSounds)
                {
                    audio.Play(0);
                }
                GameObject.Find("Game").GetComponent<GameRules>().slowMotion();
                other.rigidbody.AddForce(-other.GetContact(0).normal * ShootStrength, ForceMode2D.Impulse);
                Instantiate(ShootFX, other.GetContact(0).point, Quaternion.identity);
            }
        }
    }
}