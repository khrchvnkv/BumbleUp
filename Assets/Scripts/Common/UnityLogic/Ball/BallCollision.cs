using System;
using System.Collections;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.StairsLogic;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Ball
{
    public sealed class BallCollision : MonoBehaviour
    {
        public event Action BallCollisionStarted;
        public event Action BallCollisionCompleted;
        
        [SerializeField] private GameObject _model;
        [SerializeField] private ParticleSystem _failParticle;

        private ICoroutineRunner _coroutineRunner;
        private IStaticDataService _staticDataService;

        private bool _isCollided;

        [Inject]
        private void Construct(ICoroutineRunner coroutineRunner, IStaticDataService staticDataService)
        {
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
        }
        public void ResetCollision()
        {
            _model.SetActive(true);
            _isCollided = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_isCollided) return;
            
            if (other.gameObject.TryGetComponent(out ObstacleMovement _))
            {
                _isCollided = true;
                _coroutineRunner.StartCoroutine(CollisionCoroutine());
            }
        }
        private IEnumerator CollisionCoroutine()
        {
            _model.SetActive(false);
            _failParticle.Play();
            BallCollisionStarted?.Invoke();
            yield return new WaitForSeconds(_staticDataService.GameStaticData.BallStaticData.DurationAfterCollision);
            
            BallCollisionCompleted?.Invoke();
        }
    }
}