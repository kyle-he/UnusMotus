using UnityEngine;

public class InitOnLoad : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadGlobalStuff() {
        var main = Instantiate(Resources.Load("Global Game Stuff"));
        DontDestroyOnLoad(main);
    }

}
