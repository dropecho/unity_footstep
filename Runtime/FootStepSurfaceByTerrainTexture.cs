using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  [CreateAssetMenu(menuName = "Dropecho/Character/Foot Step Surface/By Terrain Texture Index")]
  public class FootStepSurfaceByTerrainTexture : FootStepSurfaceType {
    [System.Serializable]
    private class TerrainTextureClips {
      [SerializeField, Tooltip("When more than one clip is in list, they are randomly selected.")]
      public List<AudioClip> clips = new List<AudioClip>();
    }

    [SerializeField, Tooltip("Index in this list must match index of texture on terrain.")]
    List<TerrainTextureClips> textures;
    [SerializeField]
    LayerMask groundLayers;
    TerrainTextureDetector _terrainDetector;
    int _currentIndex;

    void OnEnable() {
      _terrainDetector = new TerrainTextureDetector();
    }

    public override bool CheckOnSurface(Transform transform) {
      if (Physics.Raycast(transform.position + Vector3.up * 0.25f, Vector3.down, out RaycastHit hit, 0.5f, groundLayers)) {
        _currentIndex = _terrainDetector.GetMainTexture(hit.point);
        return true;
      }

      return false;
    }

    public override AudioClip GetAudioClip() {
      if (textures.Count > 0) {
        var currentTexClips = textures[_currentIndex].clips;
        return currentTexClips[Random.Range(0, currentTexClips.Count)];
      }

      return base.GetAudioClip();
    }
  }
}