using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace SpaceDemo
{
    public class GoodViewUI : MonoBehaviour
    {
        public TMP_Text goodName;
        public Image icon;
        public TMP_Text price;
        public TMP_Text amount;
        public TMP_Text units;
        public TMP_Text dealText;

        private StoredGood currentGood;
        private bool isPlayer;

        public void SetGood(StoredGood good, int actualPrice, int actualAmount, bool isPlayerActually)
        {
            goodName.text = good.goodData.displayName;
            icon.sprite = good.goodData.icon;
            price.text = actualPrice.ToString();
            amount.text = actualAmount.ToString();
            units.text = good.goodData.unitName;
            isPlayer = isPlayerActually;
            currentGood = good;
            if (dealText != null)
            {
                if (isPlayer)
                    dealText.text = "Sell";
                else
                    dealText.text = "Buy";
            }
        }

        public void ShowDeal()
        {
            GUIManager.Instance.ShowDealPanel(true, currentGood, isPlayer);
        }
    }
}