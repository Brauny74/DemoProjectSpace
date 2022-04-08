using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    public class PlayerController : MonoBehaviour
    {
        public string playerName;

        public Storage playerStorage;
        public Wallet playerWallet;

        private void Awake()
        {
            playerStorage = GetComponent<Storage>();
            playerWallet = GetComponent<Wallet>();
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.playerShip = this;
        }
    }
}