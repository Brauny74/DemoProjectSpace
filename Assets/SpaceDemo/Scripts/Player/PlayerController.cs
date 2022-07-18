using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceDemo
{
    [RequireComponent(typeof(Storage))]
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        public string PlayerName;

        public Storage PlayerStorage
        {
            get { return _playerStorage; }
        }
        private Storage _playerStorage;

        public Wallet PlayerWallet 
        {
            get { return _playerWallet; }
        }
        private Wallet _playerWallet;

        public PlayerMovement PlayerMovement
        {
            get { return _playerMovement; }
        }
        private PlayerMovement _playerMovement;

        [Inject]
        public void Constructor(Storage storage, Wallet wallet, PlayerMovement playerMovement)
        {
            _playerStorage = storage;
            _playerWallet = wallet;
            _playerMovement = playerMovement;
        }
    }
}