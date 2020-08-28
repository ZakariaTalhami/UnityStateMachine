using UnityEngine;

public class LookAtPoint : MonoBehaviour {
    public Vector3 point;

    private void Update() {
        transform.LookAt(point, Vector3.up);
    }
}