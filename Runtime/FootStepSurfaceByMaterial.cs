using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  // [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Material")]
  // public class FootStepSurfaceByMaterial : FootStepSurfaceType {
  //   [SerializeField]
  //   Material material;

  //   public override bool CheckOnSurface(Transform transform) {
  //     if (Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
  //       return hit.transform.GetComponent<Renderer>()?.sharedMaterial == material;
  //     }

  //     return false;
  //   }
  // }
}