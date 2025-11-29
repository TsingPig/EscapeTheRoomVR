using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 添加UI命名空间
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Image))] // 用UI.Image替换已过时的GUITexture
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
