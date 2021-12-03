using UnityEngine;

namespace Dropecho {
  public abstract class FootStepSurfaceType : ScriptableObject {
    [field: SerializeField] public LayerMask groundLayers { get; private set; }

    public virtual bool CheckIfPointOnSurface(Vector3 point) {
      return false;
    }
  }
}