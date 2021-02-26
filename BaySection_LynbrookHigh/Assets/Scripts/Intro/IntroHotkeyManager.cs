using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHotkeyManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            LevelManager.Instance.SelectLevel("Main Menu");
        }
    }
}
