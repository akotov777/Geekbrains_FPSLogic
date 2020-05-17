using System;
using UnityEngine;
using UnityEngine.AI;


namespace FPSLogic
{
    public sealed class BotBase : BaseSceneObject, IExecute, ISelectObject
    {

        #region Fields

        [SerializeField] private Vision _vision;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _visionOrigin;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _timeToResetState = 2.0f;
        [SerializeField] private float _timeToLoseEnemy = 3.0f;

        private NavMeshAgent _agent;
        private float _currentHealt;
        private float _timeToDestroy = 10.0f;
        private Vector3 _patrolPoint;
        private Vector3 _lastTargetPosition;
        private BotState _botState;
        private ITimeRemaining _resetStateWhileInspecting;
        private ITimeRemaining _resetStateWhileLostTarget;

        public event Action<BotBase> OnDieChange;

        #endregion


        #region Properties

        public Transform Target { get { return _target; } private set { } }

        public float CurrentHealth { get { return _currentHealt; } private set { } }

        public float MaxHealth { get { return _maxHealth; } private set { } }

        public bool IsSeeingEnemy
        {
            get
            {
                return _vision.IsInVision(_visionOrigin, Target);
            }
        }

        public bool AtPatrolPoint
        {
            get
            {
                return (_patrolPoint - Transform.position).sqrMagnitude <= 1;
            }
        }

        private BotState BotState
        {
            get => _botState;
            set
            {
                _botState = value;
                switch (value)
                {
                    case BotState.None:
                        Color = Color.white;
                        break;
                    case BotState.Patroling:
                        Color = Color.green;
                        break;
                    case BotState.Inspecting:
                        Color = Color.yellow;
                        break;
                    case BotState.HasDetectedEnemy:
                        Color = Color.red;
                        break;
                    case BotState.HasLostEnemy:
                        Color = Color.blue;
                        break;
                    case BotState.Dead:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _currentHealt = _maxHealth;
            _agent = GetComponent<NavMeshAgent>();
            _resetStateWhileInspecting = new TimeRemaining(ResetBotState, _timeToResetState);
            _resetStateWhileLostTarget = new TimeRemaining(ResetBotState, _timeToLoseEnemy);
        }

        private void OnEnable()
        {
            foreach (BaseEnemyBodyPart p in GetComponentsInChildren<BaseEnemyBodyPart>())
            {
                p.EnemyPartDamage += Hurt;
            }
        }

        private void OnDisable()
        {
            foreach (BaseEnemyBodyPart p in GetComponentsInChildren<BaseEnemyBodyPart>())
            {
                p.EnemyPartDamage -= Hurt;
            }
        }

        #endregion


        #region Methods

        private void Hurt(InfoCollision info)
        {
            if (BotState == BotState.Dead) return;
            if (_currentHealt > 0)
            {
                _currentHealt -= info.Damage;
            }

            if (_currentHealt <= 0)
            {
                BotState = BotState.Dead;
                _agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRB = child.GetComponent<Rigidbody>();
                    if (!tempRB)
                    {
                        tempRB = child.gameObject.AddComponent<Rigidbody>();
                    }

                    Destroy(child.gameObject, _timeToDestroy);
                }
                OnDieChange?.Invoke(this);
            }
        }

        private void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        private void Inspect()
        {
            BotState = BotState.Inspecting;
            _resetStateWhileInspecting.AddTimeRemaining();
        }

        private void GetNewPatrolPoint()
        {
            BotState = BotState.Patroling;
            _patrolPoint = Patrol.GeneratePoint(Transform);
            MoveToPoint(_patrolPoint);
        }

        private void LoseEnemy()
        {
            BotState = BotState.HasLostEnemy;
            _lastTargetPosition = Target.position;
            _resetStateWhileLostTarget.AddWithReplace();
            _resetStateWhileInspecting.RemoveTimeRemaining();
            MoveToPoint(_lastTargetPosition);
        }

        private void ResetBotState()
        {
            BotState = BotState.None;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (BotState == BotState.Dead) return;

            if (BotState != BotState.HasDetectedEnemy)
            {
                if (!_agent.hasPath)
                {
                    if (BotState != BotState.Inspecting)
                    {
                        if (BotState != BotState.Patroling)
                        {
                            GetNewPatrolPoint();
                        }
                        else
                        {
                            if (AtPatrolPoint)
                            {
                                Inspect();
                            }
                        }
                    }
                }
                if (IsSeeingEnemy)
                {
                    BotState = BotState.HasDetectedEnemy;
                }
            }
            else
            {
                if (IsSeeingEnemy)
                {
                    _weapon.Fire();
                }
                else
                {
                    LoseEnemy();
                }
            }
            if (BotState == BotState.HasLostEnemy)
            {
                _lastTargetPosition = Target.position;
                MoveToPoint(_lastTargetPosition);
                _agent.stoppingDistance = 0;
            }
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}