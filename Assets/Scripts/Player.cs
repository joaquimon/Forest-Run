using UnityEngine;
using Fusion;
using Fusion.Addons.KCC;

namespace Starter.ThirdPersonCharacter
{
    /// <summary>
    /// Main player script - controls player movement and animations.
    /// </summary>
    public sealed class Player : NetworkBehaviour
    {
        [Header("References")]
        public KCC kcc;
        public PlayerInput PlayerInput;
        public Animator Animator;
        public Transform CameraPivot;
        public Transform CameraHandle;
        public PlayerLapController PlayerLapController;

        [Header("Movement Setup")]
        public float WalkSpeed = 2f;
        public float SprintSpeed = 5f;
        public float JumpImpulse = 10f;
        public float RotationSpeed = 8f;

        [Header("Movement Accelerations")]
        public float GroundAcceleration = 55f;
        public float GroundDeceleration = 25f;
        public float AirAcceleration = 25f;
        public float AirDeceleration = 1.3f;

        [Header("Sounds")]
        public AudioClip[] FootstepAudioClips;
        public AudioClip LandingAudioClip;
        [Range(0f, 1f)]
        public float FootstepAudioVolume = 0.5f;

        [Header("Ice Settings")]
        public float IceSlideFactor = 0.5f; // cuánto se resbala
        private bool _onIce;

        [Networked]
        private NetworkBool _isJumping { get; set; }

        private Vector3 _moveVelocity;

        // Animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

        public override void Spawned()
        {
            kcc.SetManualUpdate(true);
        }

        public override void FixedUpdateNetwork()
        {
            CheckIce();
            ProcessInput(PlayerInput.CurrentInput);

            if (kcc.FixedData.IsGrounded)
            {
                // Stop jumping
                _isJumping = false;
            }
            kcc.ManualFixedUpdate();

            PlayerInput.ResetInput();
        }

        public override void Render()
        {
            Animator.SetFloat(_animIDSpeed, kcc.Data.RealSpeed, 0.15f, Time.deltaTime);
            Animator.SetFloat(_animIDMotionSpeed, 1f);
            Animator.SetBool(_animIDJump, _isJumping);
            Animator.SetBool(_animIDGrounded, kcc.Data.IsGrounded);
            Animator.SetBool(_animIDFreeFall, kcc.Data.RealVelocity.y < -10f);
            kcc.ManualRenderUpdate();
        }

        private void Awake()
        {
            AssignAnimationIDs();
        }

        private void LateUpdate()
        {
            if (HasStateAuthority == false)
                return;

            CameraPivot.rotation = Quaternion.Euler(PlayerInput.CurrentInput.LookRotation);
            Camera.main.transform.SetPositionAndRotation(CameraHandle.position, CameraHandle.rotation);
        }

        private void CheckIce()
        {
            // Raycast hacia abajo para detectar hielo
            if (Physics.Raycast(kcc.Transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 1.5f))
            {
                _onIce = hit.collider.CompareTag("IcePlatform"); // asegúrate de que tus plataformas de hielo tengan esta tag
            }
            else
            {
                _onIce = false;
            }
        }

        private void ProcessInput(GameplayInput input)
        {
            float jumpImpulse = 0f;

            if (kcc.FixedData.IsGrounded && input.Jump)
            {
                _isJumping = true;
                jumpImpulse = JumpImpulse;
            }

            float speed = input.Sprint ? SprintSpeed : WalkSpeed;
            var lookRotation = Quaternion.Euler(0f, input.LookRotation.y, 0f);
            var moveDirection = lookRotation * new Vector3(input.MoveDirection.x, 0f, input.MoveDirection.y);

            Vector3 desiredMoveVelocity = moveDirection * speed;
            if (_onIce)
            {
                desiredMoveVelocity += _moveVelocity * IceSlideFactor;
            }

            float acceleration;
            if (desiredMoveVelocity == Vector3.zero)
            {
                acceleration = kcc.FixedData.IsGrounded ? GroundDeceleration : AirDeceleration;
            }
            else
            {
                var currentRotation = kcc.FixedData.TransformRotation;
                var targetRotation = Quaternion.LookRotation(moveDirection);
                var nextRotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Runner.DeltaTime);

                kcc.Data.SetLookRotation(nextRotation.eulerAngles);

                acceleration = kcc.FixedData.IsGrounded ? GroundAcceleration : AirAcceleration;
            }

            _moveVelocity = Vector3.Lerp(_moveVelocity, desiredMoveVelocity, acceleration * Runner.DeltaTime);

            if (kcc.FixedData.IsGrounded &&
                 KCCPhysicsUtility.ProjectOnGround(kcc.FixedData.GroundNormal, _moveVelocity, out var projectedVector))
            {
                _moveVelocity = projectedVector;
            }

            kcc.SetInputDirection(_moveVelocity);
            kcc.FixedData.JumpImpulse += Vector3.up * jumpImpulse;
            
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight < 0.5f)
                return;

            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], kcc.Data.TargetPosition, FootstepAudioVolume);
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, kcc.Data.TargetPosition, FootstepAudioVolume);
        }
    }
}
