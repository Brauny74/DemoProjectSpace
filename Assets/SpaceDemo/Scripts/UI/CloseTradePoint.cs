using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class CloseTradePoint : MonoBehaviour
    {
        public void ClosePanel()
        {
            GUIManager.Instance.ShowTradePanel(false);
        }
    }
}