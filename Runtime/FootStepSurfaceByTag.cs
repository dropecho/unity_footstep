using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Tag")]
  public class FootStepSurfaceByTag : FootStepSurfaceType {
    [SerializeField, Tooltip("When list has more than one clip, clips are selected randomly.")]
    List<AudioClip> clips = new List<AudioClip>();
    [SerializeField]
    string Tag;
    [SerializeField]
    LayerMask groundLayers;

    public override bool CheckOnSurface(Transform transform) {
      if (Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        return hit.transform.tag == Tag;
      }

      return false;
    }

    public override AudioClip GetAudioClip() {
      if (clips.Count > 0) {
        return clips[Random.Range(0, clips.Count)];
      }

      return base.GetAudioClip();
    }
  }
}