using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchDoctor.GameResources.Utils.ScriptableObjects;
using WitchDoctor.Managers.InputManagement;

namespace WitchDoctor.GameResources.CharacterScripts
{
    public class PlayerManager : PlayerEntity
    {
        #region Movement and Physics
        [SerializeField]
        private Transform _characterRenderTransform;
        [SerializeField]
        private Transform _cameraFollowTransform;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private TrailRenderer _dashTrail;
        [SerializeField]
        private PlayerStats _baseStats;

        [Space(5)]
        
        [Header("Platform Checks")]
        [SerializeField]
        private Transform _groundTransform;
        [SerializeField]
        private Transform _ledgeTransform;
        [SerializeField]
        private Transform _roofTransform;
        
        private Rigidbody2D _rb;
        private PlayerStates _playerStates;
        #endregion

        #region Entity Managers
        [Space(5)]
        [Header("Entity Managers")]
        [SerializeField] private PlayerCameraManager _playerCameraManager;
        [SerializeField] private PlayerMovementManager _playerMovementManager;
        #endregion

        #region Overrides
        protected override void SetManagerContexts()
        {
            _managerList = new List<IGameEntityManager>()
            {
                _playerCameraManager.SetContext(
                    new PlayerCameraManagerContext(
                        _rb,
                        _characterRenderTransform, 
                        _cameraFollowTransform)),
                _playerMovementManager.SetContext(
                    new PlayerMovementManagerContext(
                        _characterRenderTransform, _cameraFollowTransform, _animator,
                        _dashTrail, _baseStats, _groundTransform, _ledgeTransform, 
                        _roofTransform, _rb, _playerStates, _playerCameraManager
                        ))
            };
        }

        protected override void InitCharacter()
        {
            if (_rb == null) _rb = GetComponent<Rigidbody2D>();
            if (_animator == null) throw new NullReferenceException("Missing Animator Component");
            if (_playerStates == null) _playerStates = new PlayerStates();
            _playerStates.Reset();

            base.InitCharacter();
        }

        protected override void DeInitCharacter()
        {
            _playerStates.Reset();

            base.DeInitCharacter();
        }
        #endregion
    }
}