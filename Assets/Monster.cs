using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Scarecrow
{
    class Monster : Toggleable
    {
        static readonly int _chase = Animator.StringToHash("Chase");
        
        enum State { Patrol, Chase, Kill }
        NavMeshAgent _agent;
        Animator _animator;
        Transform _destination;
        State _state;
        float _patrolSpeed;

        [SerializeField] UnityEvent _onKill;
        [SerializeField] PlayerMovement _player;
        [SerializeField] float _chaseSpeed;
        [SerializeField] float _transitionSpeed = 0.6f;
        [SerializeField] float _startChaseDistance = 40f;
        [SerializeField] float _endChaseDistance = 20f;
        [SerializeField] Transform[] _spawnPoints;
        [SerializeField] Transform[] _destinationPoints;
        [SerializeField] float _spawnYPos = -0.11f;
        [SerializeField] float _findPlayerCutoff = 15f;
        [SerializeField] float _killDistance = 5f;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _patrolSpeed = _agent.speed;
        }

        protected override void Start()
        {
            base.Start();
            var spawn = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            transform.position = new(spawn.position.x, _spawnYPos, spawn.position.z);
        }

        void Update()
        {
            if (!IsEnabled)
                return;
            var distance = (_player.transform.position - transform.position).sqrMagnitude;
            var dir = (_player.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, dir * _startChaseDistance, Color.red);
            Debug.DrawRay(transform.position, ExpectedPlayerPosition() - transform.position, Color.blue);
            if (_state == State.Patrol)
            {
                var destination = ClosestDestination();
                if (destination != _destination)
                {
                    _destination = destination;
                    _agent.SetDestination(_destination.position);
                }

                if (distance <= _startChaseDistance * _startChaseDistance)
                    if (Physics.Raycast(transform.position, dir, out var hit, _startChaseDistance))
                        if (hit.collider.CompareTag("Player"))
                        {
                            _animator.SetBool(_chase, true);
                            DOTween.To(() => _agent.speed, s => _agent.speed = s, _chaseSpeed, _transitionSpeed);
                            _state = State.Chase;
                        }
            }
            else if (_state == State.Chase)
            {
                _agent.SetDestination(_player.transform.position);
                if (distance <= _killDistance)
                {
                    _onKill?.Invoke();
                }
                else if (distance >= _endChaseDistance * _endChaseDistance)
                {
                    _state = State.Patrol;
                    _animator.SetBool(_chase, false);
                    DOTween.To(() => _agent.speed, s => _agent.speed = s, _patrolSpeed, _transitionSpeed);
                }
            }
        }
        
        Vector3 ExpectedPlayerPosition() => _player.transform.position + _player.Direction.normalized * _findPlayerCutoff;

        Transform ClosestDestination()
        {
            var shortest = Mathf.Infinity;
            var closest = _destinationPoints[0];
            foreach (var point in _destinationPoints)
            {
                var dist = (ExpectedPlayerPosition() - point.position).sqrMagnitude;
                if (dist < shortest)
                {
                    closest = point;
                    shortest = dist;
                }
            }
            return closest;
        }

        public override void Disable()
        {
            base.Disable();
            _agent.isStopped = true;
        }

        public void Kill() => _onKill?.Invoke();
    }
}
