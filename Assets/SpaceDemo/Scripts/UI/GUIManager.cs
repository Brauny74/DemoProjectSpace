using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDemo
{
    public class GUIManager : Singleton<GUIManager>
    {
        public Image timeProgressBar;
        public PlanetPreview planetPreview;
        public TradePanel tradePanel;
        public DealViewUI dealPanel;
        public GameObject inventoryPanel;
        public GameObject loadingScreen;
        public GameObject pausePanel;

        public void SetDayImage(float currentFill)
        {
            if (timeProgressBar == null)
                return;

            timeProgressBar.fillAmount = currentFill;
        }

        public void ShowPlanetPreview(bool value)
        {
            planetPreview.gameObject.SetActive(value);
        }

        public void ShowPlanetPreview(bool value, string planetName, string planetType, string planetEvent = "None")
        {
            planetPreview.gameObject.SetActive(value);
            planetPreview.SetPanel(planetName, planetType, planetEvent);
        }

        public void ShowTradePanel(bool value)
        {
            tradePanel.gameObject.SetActive(value);
        }

        public void ShowTradePanel(bool value, List<StoredGood> partnerGoods, string pname, int pmoney)
        {
            tradePanel.gameObject.SetActive(value);
            tradePanel.ShowPartnerPanel(partnerGoods, pname, pmoney);
            PlayerController playerShip = GameManager.Instance.playerShip;
            tradePanel.ShowPlayerPanel(playerShip.playerStorage.contents, playerShip.playerName, playerShip.playerWallet.Money);
        }

        public void ShowDealPanel(bool value)
        {
            dealPanel.gameObject.SetActive(value);
        }

        public void ShowDealPanel(bool value, StoredGood dealGood, bool isPlayer)
        {
            dealPanel.gameObject.SetActive(value);
            dealPanel.SetDeal(dealGood, isPlayer);
        }

        public void ShowInventoryPanel(bool value)
        {
            inventoryPanel.SetActive(value);
            if (value)
            {
                TimeManager.Instance.paused = true;
                GoodsList list = inventoryPanel.GetComponentInChildren<GoodsList>();
                list.ClearList();
                list.SetList(GameManager.Instance.playerShip.playerStorage.contents);
            }
            else
            {
                TimeManager.Instance.paused = false;
            }
        }

        public bool IsInventoryPanelOpen()
        {
            return inventoryPanel.activeInHierarchy;
        }

        public void ShowLoadingScreen(bool value)
        {
            loadingScreen.SetActive(value);
        }

        public void ShowPausePanel(bool value)
        {
            pausePanel.SetActive(value);
            if (value)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1f;
        }

        public bool IsPausePanelOpen()
        {
            return pausePanel.activeInHierarchy;
        }
    }
}