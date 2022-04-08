using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceDemo
{
    public class PlanetPreview : MonoBehaviour
    {
        public TMP_Text planetNameText;
        public TMP_Text planetTypeText;
        public TMP_Text planetEventText;

        public void SetPanel(string planetName, string planetType, string eventName = "None")
        {
            planetNameText.text = planetName;
            planetTypeText.text = planetType;
            planetEventText.text = eventName;
        }
    }
}