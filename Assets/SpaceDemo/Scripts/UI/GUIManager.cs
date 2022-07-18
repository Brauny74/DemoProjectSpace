using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceDemo
{
    public class GUIManager : Singleton<GUIManager>
    {
        [SerializeField]
        protected Image TimeProgressBar;
        [SerializeField]
        protected PlanetPreview PlanetPreview;
        [SerializeField]
        protected TradePanel TradePanel;
        [SerializeField]
        protected DealViewUI DealPanel;
        [SerializeField]
        protected GameObject InventoryPanel;
        [SerializeField]
        protected GameObject LoadingScreen;
        [SerializeField]
        protected GameObject PausePanel;

        private GameManager _gameManager;
        private TimeManager _timeManager;

        #region Zenject
        [Inject]
        public void Constructor(GameManager gameManager, TimeManager timeManager)
        {
            _gameManager = gameManager;
            _timeManager = timeManager;
        }
        #endregion

        #region GUIManager
        public void SetDayImage(float currentFill)
        {
            if (TimeProgressBar == null)
                return;

            TimeProgressBar.fillAmount = currentFill;
        }

        public void ShowPlanetPreview(bool value)
        {
            PlanetPreview.gameObject.SetActive(value);
        }

        public void ShowPlanetPreview(bool value, string planetName, string planetType, string planetEvent = "None")
        {
            PlanetPreview.gameObject.SetActive(value);
            PlanetPreview.SetPanel(planetName, planetType, planetEvent);
        }

        public void ShowTradePanel(bool value)
        {
            TradePanel.gameObject.SetActive(value);
            _gameManager.PlayerShip.PlayerMovement.Stop();//this is a little hack to stop ship from following the position of the close button.
            if (value)
                _timeManager.paused = true;
            else
                _timeManager.paused = false;
        }

        public void ShowTradePanel(bool value, List<StoredGood> partnerGoods, string pname, int pmoney)
        {
            TradePanel.gameObject.SetActive(value);
            TradePanel.ShowPartnerPanel(partnerGoods, pname, pmoney);
            PlayerController playerShip = _gameManager.PlayerShip;
            TradePanel.ShowPlayerPanel(playerShip.PlayerStorage.contents, playerShip.PlayerName, playerShip.PlayerWallet.Money);
        }

        public void ShowDealPanel(bool value)
        {
            DealPanel.gameObject.SetActive(value);
        }

        public void ShowDealPanel(bool value, StoredGood dealGood, bool isPlayer)
        {
            DealPanel.gameObject.SetActive(value);
            DealPanel.SetDeal(dealGood, isPlayer);
        }

        public void ShowInventoryPanel(bool value)
        {
            InventoryPanel.SetActive(value);
            if (value)
            {
                _timeManager.paused = true;
                GoodsList list = InventoryPanel.GetComponentInChildren<GoodsList>();
                list.ClearList();
                list.SetList(_gameManager.PlayerShip.PlayerStorage.contents);
            }
            else
            {
                _timeManager.paused = false;
            }
        }

        public bool IsInventoryPanelOpen()
        {
            return InventoryPanel.activeInHierarchy;
        }

        public void ShowLoadingScreen(bool value)
        {
            LoadingScreen.SetActive(value);
        }

        public void ShowPausePanel(bool value)
        {
            PausePanel.SetActive(value);
            if (value)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1f;
        }

        public bool IsPausePanelOpen()
        {
            return PausePanel.activeInHierarchy;
        }
        #endregion
    }
}