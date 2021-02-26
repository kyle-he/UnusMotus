using UnityEngine;

public class HideChallenge : MonoBehaviour {
    protected void Update() {
        if (GameManager.Instance.GameMode == GameMode.Relaxed) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (GameManager.Instance.GameMode == GameMode.Challenge) {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
