using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SpaceDemo
{
    [RequireComponent(typeof(Storage))]
    [RequireComponent(typeof(Wallet))]
    public class NPCShipController : MonoBehaviour
    {
        public Vector3 target;

        public Storage storage;
        public Wallet wallet;

        [Tooltip("How many stacks of goods the randomized storage moght have")]
        public int maxAmountOfStacks;
        public List<TradeGood> possibleGoods;
        public int minAmount;
        public int maxAmount;

        public int minMoney;
        public int maxMoney;

        public MeshRenderer model;
        private NavMeshAgent _agent;
        private Material coloredMaterial;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            storage = GetComponent<Storage>();
            wallet = GetComponent<Wallet>();
            coloredMaterial = model.material;
        }

        private void Update()
        {
            if (TimeManager.Instance.paused)
            {
                Stop();
            }
            else
            {
                Move();
            }
        }

        public void MoveTo(Vector3 t)
        {
            target = t;
            Move();
        }

        private void Move()
        {
            NavMeshPath path = new NavMeshPath();
            _agent.CalculatePath(target, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                _agent.isStopped = false;
                _agent.SetPath(path);
            }
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void ColorRandomly()
        {
            coloredMaterial.color = Random.ColorHSV();
        }

        public void CreateCargo()
        {
            storage.Clear();
            wallet.AddMoney(Random.Range(minMoney, maxMoney + 1));
            int amountOfStacks = Random.Range(1, maxAmountOfStacks + 1);
            for (int i = 0; i < amountOfStacks; i++)
            {
                storage.Add(possibleGoods[Random.Range(0, possibleGoods.Count)], Random.Range(minAmount, maxAmount + 1));
            }
        }

    }

}