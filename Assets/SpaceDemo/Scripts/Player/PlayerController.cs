using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDemo
{
    [RequireComponent(typeof(Storage))]
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        public string playerName;

        public Storage playerStorage;
        public Wallet playerWallet;

        public PlayerMovement playerMovement;

        private void Awake()
        {
            playerStorage = GetComponent<Storage>();
            playerWallet = GetComponent<Wallet>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.playerShip = this;
        }
    }
}