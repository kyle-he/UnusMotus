using UnityEngine;

public class TitleBackController : MonoBehaviour {
    /// <summary>
    /// Animate background
    /// </summary>
    void Update() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 100;
        mouse.x = -mouse.x / 50;
        mouse.y = -mouse.y / 50;
        transform.position = mouse;
    }
}
