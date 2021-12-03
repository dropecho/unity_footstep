using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropecho {
  public class FootStepAudio : MonoBehaviour {
    public AudioClip defaultClip;
    public AudioClip groundClip;
    public FootStepSurfaceType ground;

    public AudioSource source;

    public void OnFootStep(FootStepEvent evt) {
      source.volume = Mathf.InverseLerp(0, 0.2f, Mathf.Abs(evt.velocity));

      if (evt.surface == ground) {
        source.PlayOneShot(groundClip);
      } else {
        source.PlayOneShot(defaultClip);
      }
    }
  }
}