using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using Zenject;

namespace SpaceDemo
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerController PlayerShip {
            get { return _playerShip; }
        }
        private PlayerController _playerShip;
        public TradePoint currentTradePoint;
        [SerializeField]
        private CinemachineVirtualCamera cmVCamera;

        public UnityEvent OnLoadEnded;

        public int AmountOfObjectsToLoad
        {
            get { 
                return _amountOfObjectsToLoad;
            }
            set {
                _amountOfObjectsToLoad = value;
                _loadStarted = true;
            }
        }
        private int _amountOfObjectsToLoad = 0;
        private bool _loadStarted = false;

        private GUIManager _guiManager;

        #region Zenject
        [Inject]
        public void Constructor(PlayerController playerShip, GUIManager guiManager)
        {
            _playerShip = playerShip;
            _guiManager = guiManager;

        }
        #endregion

        #region MonoBehavior
        public void Update()
        {
            ProcessLoad();
        }
        #endregion

        #region GameManager
        private void ProcessLoad()
        {
            if (!_loadStarted)
                return;
            if(AmountOfObjectsToLoad <= 0)
            {
                _guiManager.ShowLoadingScreen(false);
                cmVCamera.m_Follow = _playerShip.transform;
                cmVCamera.m_LookAt = _playerShip.transform;
                OnLoadEnded.Invoke();
            }
        }
        #endregion
    }
}