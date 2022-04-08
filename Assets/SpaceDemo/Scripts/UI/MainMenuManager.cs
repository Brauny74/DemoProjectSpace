using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SpaceDemo
{
    public class MainMenuManager : MonoBehaviour
    {
        public void LoadScene(string sceneAddress)
        { 
            Addressables.LoadSceneAsync(sceneAddress);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}