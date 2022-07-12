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


        public void PutPlayerShipBack()
        {
            Transform returnPoint = GetRandomReturnPoint();
            GameManager.Instance.playerShip.transform.position = returnPoint.position;
            GameManager.Instance.playerShip.transform.rotation = returnPoint.rotation;
            GameManager.Instance.playerShip.playerMovement.Stop();
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

        public void UpdateTradePanel()
        {
            _storage.UpdateTradePanel(tradeName, _wallet.Money);
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
    }
}