using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Tag")]
  public class FootStepSurfaceByTag : FootStepSurfaceType {
    [field: SerializeField] public string tag { get; private set; }

    public override bool CheckIfPointOnSurface(Vector3 position) {
      if (Physics.Raycast(position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        return hit.transform.tag == tag;
      }

      return false;
    }
  }
}