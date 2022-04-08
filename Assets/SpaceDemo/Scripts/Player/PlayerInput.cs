using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class PlayerInput : MonoBehaviour
    {
        public string MoveInputAxis;
        public string InventoryAxis;
        public string PauseAxis;

        private PlayerMovement _plMov;

        void Awake()
        {
           _plMov = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Input.GetButtonDown(InventoryAxis))
            {
                GUIManager.Instance.ShowInventoryPanel(!GUIManager.Instance.IsInventoryPanelOpen());
            }

            if (Input.GetButtonDown(PauseAxis))
            {
                GUIManager.Instance.ShowPausePanel(!GUIManager.Instance.IsPausePanelOpen());
            }

            if (TimeManager.Instance.paused)
            {
                _plMov.Stop();
                return;
            }

            if (Input.GetAxisRaw(MoveInputAxis) != 0)
            {
                _plMov.MoveTo(Input.mousePosition);
            }
            ProcessMouseOverPlanet();
        }

        Ray ray;
        RaycastHit hit;
        private void ProcessMouseOverPlanet()
        {
            GUIManager.Instance.ShowPlanetPreview(false);

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Planet")
                {
                    hit.collider.gameObject.GetComponent<PlanetController>().ShowPreviewPanel();
                }
            }            
        }
    }
}