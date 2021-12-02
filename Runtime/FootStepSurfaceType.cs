using UnityEngine;

namespace Dropecho {
  public abstract class FootStepSurfaceType : ScriptableObject {
    public virtual bool CheckOnSurface(Transform transform) {
      return true;
    }

    public virtual AudioClip GetAudioClip() {
      Debug.LogWarning("Attempted to use a Foot Step Surface Type with no attached audio clips.");
      return null;
    }
  }
}