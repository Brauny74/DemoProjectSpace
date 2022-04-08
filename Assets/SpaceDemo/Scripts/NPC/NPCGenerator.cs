using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(BasePooler))]
    public class NPCGenerator : MonoBehaviour
    {
        public float timeBetweenNewShips;

        public List<TradePoint> tradePoints;
        public BasePooler shipPooler;

        private float currentTime;
        private bool isGenerating;

        void Awake()
        {
            currentTime = timeBetweenNewShips;
            GameManager.Instance.OnLoadEnded.AddListener(StartGenerating);
        }

        private void StartGenerating()
        {
            FindTradePoints();
            isGenerating = true;
        }

        // Update is called once per frame
        void Update()
        {
            ProcessTime();
        }

        private void ProcessTime()
        {
            if (!isGenerating)
                return;

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                CreateNewShip();
                currentTime = timeBetweenNewShips;
            }
        }

        private void CreateNewShip()
        {
            GameObject shipObject = shipPooler.GetPooledObject();
            if (shipObject != null)
            {
                NPCShipController npcShip = shipObject.GetComponent<NPCShipController>();
                npcShip.transform.position = pickRandomTradePoint().GetRandomReturnPoint().position;
                npcShip.CreateCargo();
                npcShip.ColorRandomly();
                npcShip.MoveTo(pickRandomTradePoint().transform.position);
            }
        }

        private TradePoint pickRandomTradePoint()
        {
            return tradePoints[Random.Range(0, tradePoints.Count - 1)];
        }

        private void FindTradePoints()
        {
            TradePoint[] tPs = FindObjectsOfType<TradePoint>();
            tradePoints = new List<TradePoint>(tPs);
        }
    }
}