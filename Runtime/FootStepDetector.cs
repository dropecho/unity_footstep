using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dropecho {
  public struct FootStepEvent {
    public float velocity;
    public Vector3 position;
    public FootStepSurfaceType surface;
  }

  public class FootStepDetector : MonoBehaviour {
    [Tooltip("Should the player attempt to auto detect footsteps, if false, you can still trigger this via events.")]
    public bool autoDetectFootSteps;
    [SerializeField, Tooltip("The animator to detect footsteps for (must be humanoid).")]
    [field: SerializeField] public Animator animator { get; private set; }
    [Tooltip("The minimum time between playing footstep sounds.")]
    [field: SerializeField] public float minTimeBetweenFootsteps { get; private set; } = 0.33f;
    [Tooltip("The height at which a foot is marked as no longer being on the ground (so it will trigger a sound later).")]
    [field: SerializeField] public float heightToMarkOffGround { get; private set; } = 0.02f;
    [Tooltip("The height at which a foot is marked as being on the ground (so it will trigger a sound if was marked as off the ground).")]
    [field: SerializeField] public float heightToMarkOnGround { get; private set; } = 0.02f;

    [field: SerializeField] public List<FootStepSurfaceType> surfaceTypes { get; private set; }

    public UnityEvent<FootStepEvent> OnLeftFootStep = new UnityEvent<FootStepEvent>();
    public UnityEvent<FootStepEvent> OnRightFootStep = new UnityEvent<FootStepEvent>();
    public UnityEvent<FootStepEvent> OnFootStep = new UnityEvent<FootStepEvent>();

    float _timeSinceLastEvent = 0;

    float _originalLFHeight;
    float _originalRFHeight;
    Transform _LFTransform;
    Transform _RFTransform;
    float _previousLFHeight;
    float _previousRFHeight;
    Vector3 _previousPosition;
    bool _LFOffGround = false;
    bool _RFOffGround = false;

    void OnEnable() {
      if (animator == null) {
        return;
      }
      _originalLFHeight = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position.y - transform.position.y;
      _originalRFHeight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position.y - transform.position.y;

      _LFTransform = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
      _RFTransform = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }

    void OnValidate() {
      if (animator == null) {
        Debug.LogWarning("Foot Step Detector: cannot have a null animator.");
      }
    }

    void LateUpdate() {
      if (animator == null) {
        return;
      }

      if (autoDetectFootSteps) {
        CheckLeftFoot();
        CheckRightFoot();
      }
      _timeSinceLastEvent += Time.deltaTime;
      _previousPosition = transform.position;
    }

    void CheckLeftFoot() {
      var currentLFHeight = _LFTransform.position.y - transform.position.y;
      var LFVelocity = currentLFHeight - _previousLFHeight;
      _previousLFHeight = currentLFHeight;

      if (_LFOffGround == false && currentLFHeight > _originalLFHeight + heightToMarkOffGround) {
        _LFOffGround = true;
      }

      if (LFVelocity < 0 && currentLFHeight <= _originalLFHeight + heightToMarkOnGround && _LFOffGround && _timeSinceLastEvent > minTimeBetweenFootsteps) {
        var evt = new FootStepEvent() { velocity = LFVelocity, position = _LFTransform.position, surface = GetSurfaceType(_LFTransform.position) };
        OnLeftFootStep?.Invoke(evt);
        OnFootStep?.Invoke(evt);
        _LFOffGround = false;
        _timeSinceLastEvent = 0;
      }
    }

    void CheckRightFoot() {
      var currentRFHeight = _RFTransform.position.y - transform.position.y;
      var RFVelocity = currentRFHeight - _previousRFHeight;
      _previousRFHeight = currentRFHeight;

      if (_RFOffGround == false && currentRFHeight > _originalRFHeight + heightToMarkOffGround) {
        _RFOffGround = true;
      }

      if (RFVelocity < 0 && currentRFHeight <= _originalRFHeight + heightToMarkOnGround && _RFOffGround && _timeSinceLastEvent > minTimeBetweenFootsteps) {
        var evt = new FootStepEvent() { velocity = RFVelocity, position = _RFTransform.position, surface = GetSurfaceType(_RFTransform.position) };
        OnRightFootStep?.Invoke(evt);
        OnFootStep?.Invoke(evt);
        _RFOffGround = false;
        _timeSinceLastEvent = 0;
      }
    }

    // These exist so the events can be triggered by animations, allowing surface detection without the auto detect of footsteps.
    public void LeftFootStep() {
      var evt = new FootStepEvent() { velocity = 1, position = _LFTransform.position, surface = GetSurfaceType(_LFTransform.position) };
      OnLeftFootStep?.Invoke(evt);
      OnFootStep?.Invoke(evt);
      _timeSinceLastEvent = 0;
    }

    public void RightFootStep() {
      var evt = new FootStepEvent() { velocity = 1, position = _RFTransform.position, surface = GetSurfaceType(_RFTransform.position) };
      OnRightFootStep?.Invoke(evt);
      OnFootStep?.Invoke(evt);
      _timeSinceLastEvent = 0;
    }

    public void FootStep() {
      var velocity = (transform.position - _previousPosition).magnitude;
      var evt = new FootStepEvent() { velocity = velocity, position = transform.position, surface = GetSurfaceType(transform.position) };
      OnFootStep?.Invoke(evt);
      _timeSinceLastEvent = 0;
    }

    FootStepSurfaceType GetSurfaceType(Vector3 position) {
      foreach (var type in surfaceTypes) {
        if (type.CheckIfPointOnSurface(position)) {
          return type;
        }
      }

      return null;
    }
  }
}