using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  public class FootStepPlayer : MonoBehaviour {
    [Tooltip("Should the player attempt to auto detect footsteps, if false, you can still trigger this via events.")]
    public bool autoDetectFootSteps;
    [SerializeField, Tooltip("The source to play the audio from.")]
    AudioSource audioSource;
    [SerializeField, Tooltip("The animator to detect footsteps for (must be humanoid).")]
    Animator animator;
    [Tooltip("The minimum time between playing footstep sounds.")]
    public float minTimeBetweenFootsteps = 0.33f;
    [Tooltip("The height at which a foot is marked as no longer being on the ground (so it will trigger a sound later).")]
    public float heightToMarkOffGround = 0.02f;
    [Tooltip("The height at which a foot is marked as being on the ground (so it will trigger a sound if was marked as off the ground).")]
    public float heightToMarkOnGround = 0.02f;
    [Tooltip("The velocity at which a foot moving triggers the maximum volume (1), lower values mean louder sounds. (i.e. a slower foot is louder the lower the value)")]
    public float velocityForMaxVolume = 0.2f;

    [SerializeField, SerializeReference, Tooltip("The list of surface types to detect.")]
    List<FootStepSurfaceType> surfaceTypes = new List<FootStepSurfaceType>();

    float _timeSinceLastPlayed = 0;

    float _originalLFHeight;
    float _originalRFHeight;
    Transform _LFTransform;
    Transform _RFTransform;
    float _previousLFHeight;
    float _previousRFHeight;
    bool _LFOffGround = false;
    bool _RFOffGround = false;

    void OnEnable() {
      _originalLFHeight = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position.y - transform.position.y;
      _originalRFHeight = animator.GetBoneTransform(HumanBodyBones.RightFoot).position.y - transform.position.y;

      _LFTransform = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
      _RFTransform = animator.GetBoneTransform(HumanBodyBones.RightFoot);
    }

    void LateUpdate() {
      if (autoDetectFootSteps) {
        CheckLeftFoot();
        CheckRightFoot();
      }
      _timeSinceLastPlayed += Time.deltaTime;
    }

    void CheckLeftFoot() {
      var currentLFHeight = _LFTransform.position.y - transform.position.y;
      var LFVelocity = currentLFHeight - _previousLFHeight;
      _previousLFHeight = currentLFHeight;

      if (_LFOffGround == false && currentLFHeight > _originalLFHeight + heightToMarkOffGround) {
        _LFOffGround = true;
      }

      if (LFVelocity < 0 && currentLFHeight <= _originalLFHeight + heightToMarkOnGround && _LFOffGround && _timeSinceLastPlayed > minTimeBetweenFootsteps) {
        audioSource.volume = Mathf.InverseLerp(0, velocityForMaxVolume, Mathf.Abs(LFVelocity));
        DoFootStepLeft();
        _LFOffGround = false;
      }
    }

    void CheckRightFoot() {
      var currentRFHeight = _RFTransform.position.y - transform.position.y;
      var RFVelocity = currentRFHeight - _previousRFHeight;
      _previousRFHeight = currentRFHeight;

      if (_RFOffGround == false && currentRFHeight > _originalRFHeight + heightToMarkOffGround) {
        _RFOffGround = true;
      }

      if (RFVelocity < 0 && currentRFHeight <= _originalRFHeight + heightToMarkOnGround && _RFOffGround && _timeSinceLastPlayed > minTimeBetweenFootsteps) {
        audioSource.volume = Mathf.InverseLerp(0, velocityForMaxVolume, Mathf.Abs(RFVelocity));
        DoFootStepRight();
        _RFOffGround = false;
      }
    }

    // These are for handling events either from animations, or from auto detection.
    void DoFootStepLeft() {
      DoFootStep(_LFTransform);
    }

    // These are for handling events either from animations, or from auto detection.
    void DoFootStepRight() {
      DoFootStep(_RFTransform);
    }

    void DoFootStep(Transform transform) {
      // audioSource.pitch = Random.Range(0.8f, 1.4f);

      foreach (var type in surfaceTypes) {
        if (type.CheckOnSurface(transform)) {
          audioSource.PlayOneShot(type.GetAudioClip());
          _timeSinceLastPlayed = 0;
        }
      }
    }
  }
}