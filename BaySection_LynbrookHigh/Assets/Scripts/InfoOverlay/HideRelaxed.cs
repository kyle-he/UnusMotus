using UnityEngine;

public class HideRelaxed : MonoBehaviour {
    protected void Update() {
        if (GameManager.Instance.GameMode == GameMode.Relaxed) {
            transform.localScale = new Vector3(0, 0, 0);
        }
        if (GameManager.Instance.GameMode == GameMode.Challenge) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
