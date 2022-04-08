using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceDemo
{
    public class TradePanel : MonoBehaviour
    {
        public GoodsList partnerGoodsList;
        public TMP_Text partnerName;
        public TMP_Text partnerMoney;
        public GoodsList playerGoodsList;
        public TMP_Text playerName;
        public TMP_Text playerMoney;

        public void ShowPartnerPanel(List<StoredGood> goods, string pName, int pMoney)
        {
            partnerGoodsList.ClearList();
            partnerName.text = pName;
            partnerGoodsList.gameObject.SetActive(true);
            partnerGoodsList.SetList(goods, false);
            partnerMoney.text = pMoney.ToString();
        }

        public void ShowPlayerPanel(List<StoredGood> goods, string pName, int pMoney)
        {
            playerGoodsList.ClearList();
            playerName.text = pName;
            playerGoodsList.gameObject.SetActive(true);
            playerGoodsList.SetList(goods, true);
            playerMoney.text = pMoney.ToString();
        }
    }
}