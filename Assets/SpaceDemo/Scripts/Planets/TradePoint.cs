using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(Storage))]
    [RequireComponent(typeof(Wallet))]
    public class TradePoint : MonoBehaviour
    {
        public string playerTag = "Player";
        public string npcShipTag = "NPC";
        public List<Transform> returnPoints;

        private Storage _storage;
        private Wallet _wallet;
        private string tradeName;

        #region MonoBehavior
        void Awake()
        {
            _storage = GetComponent<Storage>();
            _wallet = GetComponent<Wallet>();
            //Check, what kind of trade point it is.

            //If it's a planet, use planet's name
            PlanetController _planet = GetComponent<PlanetController>();
            if (_planet != null)
                tradeName = _planet.planetName;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == playerTag)
            {
                ProcessPlayerEnter();
            }
            if (collision.gameObject.tag == npcShipTag)
            {
                ProcessNPCEnter(collision.gameObject.GetComponent<NPCShipController>());
            }
        }
        #endregion

        #region TradePoint
        public void PutPlayerShipBack()
        {
            Transform returnPoint = GetRandomReturnPoint();
            GameManager.Instance.PlayerShip.transform.position = returnPoint.position;
            GameManager.Instance.PlayerShip.transform.rotation = returnPoint.rotation;
            GameManager.Instance.PlayerShip.PlayerMovement.Stop();
        }

        public Transform GetRandomReturnPoint()
        {
            return returnPoints[Random.Range(0, returnPoints.Count - 1)];
        }

        public void AddGoods(TradeGood good, int amount)
        {
            _storage.Add(good, amount);
        }

        public void AddMoney(int value)
        {
            _wallet.AddMoney(value);
        }

        public int GetMoney()
        {
            return _wallet.Money;
        }

        private void UpdateTradePanel()
        {
            _storage.UpdateTradePanel(tradeName, _wallet.Money);
        }

        public void MakeDeal(StoredGood good, int amount, bool isSelling)
        {
            var playerShip = GameManager.Instance.PlayerShip;
            if (isSelling)
            {
                playerShip.PlayerStorage.Add(good.goodData, 0 - amount);
                playerShip.PlayerWallet.AddMoney(good.actualPrice * amount);
                AddGoods(good.goodData, amount);
                AddMoney(0 - (good.actualPrice * amount));
            }
            else
            {
                playerShip.PlayerStorage.Add(good.goodData, amount);
                playerShip.PlayerWallet.AddMoney(0 - (good.actualPrice * amount));
                AddGoods(good.goodData, 0 - amount);
                AddMoney(good.actualPrice * amount);
            }
            GUIManager.Instance.ShowDealPanel(true, good, isSelling);
            UpdateTradePanel();
        }

        private void ProcessPlayerEnter()
        {
            GameManager.Instance.currentTradePoint = this;
            UpdateTradePanel();
            PutPlayerShipBack();
        }

        private void ProcessNPCEnter(NPCShipController npcShip)
        {
            foreach (StoredGood good in npcShip.storage.contents)
            {
                _storage.Add(good.goodData, good.amountInStack);
            }
            _wallet.AddMoney(npcShip.wallet.Money);
            npcShip.gameObject.SetActive(false);
        }
        #endregion
    }
}