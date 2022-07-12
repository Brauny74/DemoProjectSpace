using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        protected string MoveInputAxis;
        [SerializeField]
        protected string InventoryAxis;
        [SerializeField]
        protected string PauseAxis;

        protected PlayerMovement _plMov;

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
                _plMov.MoveToClick(Input.mousePosition);
            }
        }
    }
}