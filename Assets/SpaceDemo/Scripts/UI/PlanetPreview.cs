using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceDemo
{
    public class PlanetPreview : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text planetNameText;
        [SerializeField]
        protected TMP_Text planetTypeText;
        [SerializeField]
        protected TMP_Text planetEventText;

        public void SetPanel(string planetName, string planetType, string eventName = "None")
        {
            planetNameText.text = planetName;
            planetTypeText.text = planetType;
            planetEventText.text = eventName;
        }
    }
}