using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour {
    public void ChangeScreen(string sceneName)
    {
        Debug.Log("Hello!!!");
        Application.LoadLevel(sceneName);
    }
}
