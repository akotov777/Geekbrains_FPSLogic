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
        [SerializeField] private float _timeToInspect = 2.0f;

        private NavMeshAgent _agent;
        private BaseBotState _currentState;
        private DeadBotState _deadBS;
        private HasADetectedTargetBotState _hasADetectedTargetBS;
        private HasLostTargetBotState _hasLostTargetBS;
        private InspectingBotState _inspectingBS;
        private PatrolingBotState _patrolingBotState;
        private ITimeRemaining _changeStateAfterInspecting;
        private float _currentHealt;
        private float _timeToDestroy = 10.0f;
        private Vector3 _patrolPoint;
        private Vector3 _lastTargetPosition;

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

        public float TimeToInspect
        {
            get { return _timeToInspect; }
        }

        public Vector3 PatrolPoint
        {
            get { return _patrolPoint; }
        }

        public Vector3 LastTargetPosition
        {
            get { return _lastTargetPosition; }
        }

        public DeadBotState DeadBotState
        {
            get { return _deadBS; }
        }

        public HasADetectedTargetBotState HasADetectedEnemyBotState
        {
            get { return _hasADetectedTargetBS; }
        }

        public HasLostTargetBotState HasLostEnemyBotState
        {
            get { return _hasLostTargetBS; }
        }

        public InspectingBotState InspectingBotState
        {
            get { return _inspectingBS; }
        }

        public PatrolingBotState PatrolingBotState
        {
            get { return _patrolingBotState; }
        }

        public ITimeRemaining ChangeStateAfterInspecting
        {
            get { return _changeStateAfterInspecting; }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _currentHealt = _maxHealth;
            _agent = GetComponent<NavMeshAgent>();

            _deadBS = new DeadBotState(this);
            _hasADetectedTargetBS = new HasADetectedTargetBotState(this);
            _hasLostTargetBS = new HasLostTargetBotState(this);
            _inspectingBS = new InspectingBotState(this);
            _patrolingBotState = new PatrolingBotState(this);
            GetNewPatrolPoint();
            _currentState = _patrolingBotState;

            _changeStateAfterInspecting = new TimeRemaining(SetBotStateToPatroling, _timeToInspect);
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
            if (_currentState is DeadBotState) return;
            if (_currentHealt > 0)
            {
                _currentHealt -= info.Damage;
            }

            if (_currentHealt <= 0)
            {
                SetBotState(DeadBotState);
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

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public bool IsAtPoint(Vector3 point)
        {
            return (point - Transform.position).sqrMagnitude <= 1;
        }

        public void SetBotState(BaseBotState state)
        {
            _currentState = state;
        }

        public void SetLastTargetPosition()
        {
            _lastTargetPosition = Target.position;
        }

        public void GetNewPatrolPoint()
        {
            _patrolPoint = Patrol.GeneratePoint(Transform);
        }

        public void FireWeapon()
        {
            _weapon.Fire();
        }

        private void SetBotStateToPatroling()
        {
            GetNewPatrolPoint();
            SetBotState(PatrolingBotState);
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            _currentState.Behave();
            if
               (
                   !(_currentState is HasADetectedTargetBotState)
                   &&
                   !(_currentState is DeadBotState)
               )
                   if(Time.frameCount % 4 == 0)
                       if (IsSeeingEnemy) SetBotState(HasADetectedEnemyBotState);
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