using UnityEngine;

public class Mover : MonoBehaviour {
  Vector3 _startPos;

  // Start is called before the first frame update
  void Start() {
    _startPos = transform.position;
  }

  // Update is called once per frame
  void Update() {
    transform.position += transform.right * Time.deltaTime * 2;
    if (transform.position.x > 5) {
      transform.position = _startPos;
    }
  }
}
