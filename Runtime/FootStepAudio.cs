using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  public class FootStepAudio : MonoBehaviour {
    public AudioClip defaultClip;

    public FootStepSurfaceType[] surfaces;
    public AudioClip[] clips;

    public AudioSource source;

    public void OnFootStep(FootStepEvent evt) {
      source.volume = Mathf.InverseLerp(0, 0.2f, Mathf.Abs(evt.velocity));

      var surfaceIndex = System.Array.IndexOf(surfaces, evt.surface);
      if (surfaceIndex != -1 && clips.Length > surfaceIndex && clips[surfaceIndex] != null) {
        source.PlayOneShot(clips[surfaceIndex]);
      } else {
        source.PlayOneShot(defaultClip);
      }
    }
  }
}