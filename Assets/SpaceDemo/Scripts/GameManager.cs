using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

namespace SpaceDemo
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerController playerShip;
        public TradePoint currentTradePoint;
        public CinemachineVirtualCamera cmVCamera;

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

        public void Update()
        {
            ProcessLoad();
        }

        public void MakeDeal(StoredGood good, int amount, bool isSelling)
        {
            if (isSelling)
            {
                playerShip.playerStorage.Add(good.goodData, 0 - amount);
                playerShip.playerWallet.AddMoney(good.actualPrice * amount);
                currentTradePoint.AddGoods(good.goodData, amount);
                currentTradePoint.AddMoney(0 - (good.actualPrice * amount));
            }
            else
            {
                playerShip.playerStorage.Add(good.goodData, amount);
                playerShip.playerWallet.AddMoney(0 - (good.actualPrice * amount));
                currentTradePoint.AddGoods(good.goodData, 0 - amount);
                currentTradePoint.AddMoney(good.actualPrice * amount);
            }
            GUIManager.Instance.ShowDealPanel(true, good, isSelling);
            currentTradePoint.UpdateTradePanel();
        }

        private void ProcessLoad()
        {
            if (!_loadStarted)
                return;
            if(AmountOfObjectsToLoad <= 0)
            {
                GUIManager.Instance.ShowLoadingScreen(false);
                playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                cmVCamera.m_Follow = playerShip.transform;
                cmVCamera.m_LookAt = playerShip.transform;
                OnLoadEnded.Invoke();
            }
        }
    }
}