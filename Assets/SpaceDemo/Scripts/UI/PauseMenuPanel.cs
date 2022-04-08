using UnityEngine;

namespace SpaceDemo
{
    public class PauseMenuPanel : MonoBehaviour
    {
        public void Continue()
        {
            GUIManager.Instance.ShowPausePanel(false);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}