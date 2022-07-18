using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
        private TimeManager _timeManager;
        private GUIManager _guiManager;

        #region Zenject
        [Inject]
        public void Constructor(PlayerMovement playerMovement, GUIManager guiManager, TimeManager timeManager)
        {
            _plMov = playerMovement;
            _guiManager = guiManager;
            _timeManager = timeManager;
        }
        #endregion

        #region MonoBehavior

        // Update is called once per frame
        void Update()
        {
            ProcessInput();
        }
        #endregion

        #region PlayerInput
        private void ProcessInput()
        {
            if (Input.GetButtonDown(InventoryAxis))
            {
                _guiManager.ShowInventoryPanel(!_guiManager.IsInventoryPanelOpen());
            }

            if (Input.GetButtonDown(PauseAxis))
            {
                _guiManager.ShowPausePanel(!_guiManager.IsPausePanelOpen());
            }

            if (_timeManager.paused)
            {
                _plMov.Stop();
                return;
            }

            if (Input.GetAxisRaw(MoveInputAxis) != 0)
            {
                _plMov.MoveToClick(Input.mousePosition);
            }
        }
        #endregion
    }
}