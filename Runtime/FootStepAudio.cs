using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  public class FootStepAudio : MonoBehaviour {
    public AudioSource source;
    public AudioClip defaultClip;
    public FootStepSurfaceType[] surfaces;
    public AudioClip[] clips;

    public void OnFootStep(FootStepEvent evt) {
      var volume = 0.5f;
      // var volume = Mathf.InverseLerp(0, 0.1f, Mathf.Abs(evt.velocity));

      var surfaceIndex = System.Array.IndexOf(surfaces, evt.surface);
      if (surfaceIndex >= 0 && clips.Length > surfaceIndex && clips[surfaceIndex] != null) {
        source.PlayOneShot(clips[surfaceIndex], volume);
      } else {
        source.PlayOneShot(defaultClip, volume);
      }
    }
  }
}