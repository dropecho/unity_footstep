using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Terrain Texture Index")]
  public class FootStepSurfaceByTerrainTexture : FootStepSurfaceType {
    public int terrainTextureIndex = 0;
    TerrainTextureDetector _terrainDetector;

    void OnEnable() {
      _terrainDetector = new TerrainTextureDetector();
    }

    public override bool CheckIfPointOnSurface(Vector3 position) {
      if (Physics.Raycast(position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        return terrainTextureIndex == _terrainDetector.GetMainTexture(hit.point);
      }

      return false;
    }
  }
}