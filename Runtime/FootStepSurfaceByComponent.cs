using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Component")]
  public class FootStepSurfaceByComponent : FootStepSurfaceType {
    public override bool CheckIfPointOnSurface(Vector3 position) {
      if (Physics.Raycast(position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        var surface = hit.transform.GetComponent<FootStepSurface>();
        if (surface != null) {
          return surface.type == this;
        }
      }

      return false;
    }
  }
}