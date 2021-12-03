using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Material")]
  public class FootStepSurfaceByMaterial : FootStepSurfaceType {
    [field: SerializeField] public Material material { get; private set; }

    public override bool CheckIfPointOnSurface(Vector3 position) {
      if (Physics.Raycast(position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        var renderer = hit.transform.GetComponent<Renderer>();
        if (renderer != null) {
          return renderer.sharedMaterial == material;
        }
      }

      return false;
    }
  }
}