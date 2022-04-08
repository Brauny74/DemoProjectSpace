using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceDemo
{
    public class DealViewUI : MonoBehaviour
    {
        public TMP_Text goodNameText;
        public TMP_Text amountText;
        public Image icon;
        public TMP_Text dealText;
        public Slider amountSlider;
        public TMP_Text priceText;
        public TMP_Text unitsAmountText;
        public TMP_Text unitsText;
        public Button dealButton;

        private StoredGood currentGood;
        private int currentAmount;
        private bool isPlayer;

        public void SetDeal(StoredGood good, bool isPlayerActually)
        {
            currentGood = good;
            goodNameText.text = currentGood.goodData.displayName;
            amountText.text = currentGood.amountInStack.ToString();
            icon.sprite = currentGood.goodData.icon;
            isPlayer = isPlayerActually;
            if (isPlayer)
                dealText.text = "Sell";
            else
                dealText.text = "Buy";
            amountSlider.value = 0;
            amountSlider.maxValue = currentGood.amountInStack;
            currentAmount = 0;
            priceText.text = (currentGood.actualPrice * currentAmount).ToString();
            unitsAmountText.text = currentGood.goodData.unitName;
            unitsText.text = currentAmount.ToString();
            CheckAfford();
        }

        public void OnAmountChanged()
        {
            currentAmount = Mathf.RoundToInt(amountSlider.value);
            unitsText.text = currentAmount.ToString();
            priceText.text = (currentGood.actualPrice * currentAmount).ToString();
            CheckAfford();
        }

        public void MakeDeal()
        {
            GameManager.Instance.MakeDeal(currentGood, currentAmount, isPlayer);
        }

        /// <summary>
        /// Disable the Deal button, if the buyer can't afford the deal or there's no good anymore
        /// </summary>
        private void CheckAfford()
        {
            if (currentGood.amountInStack == 0)
            {
                dealButton.interactable = false;
                return;
            }

            if (isPlayer)
            {
                if (currentGood.actualPrice * currentAmount > GameManager.Instance.currentTradePoint.GetMoney())
                {
                    dealButton.interactable = false;
                }
                else
                {
                    dealButton.interactable = true;
                }
            }
            else
            {
                if (currentGood.actualPrice * currentAmount > GameManager.Instance.playerShip.playerWallet.Money)
                {
                    dealButton.interactable = false;
                }
                else
                {
                    dealButton.interactable = true;
                }
            }
        }
    }
}