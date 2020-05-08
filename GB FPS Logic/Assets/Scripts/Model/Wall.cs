using UnityEngine;


namespace FPSLogic
{
    public sealed class Wall : BaseSceneObject, ISelectObject, IPenetrable
    {
        #region Fields

        [SerializeField] private float _penetratingDamageReduce = 1.0f;

        #endregion


        #region ISelectObj

        public string GetMessage()
        {
            return Name;
        }

        #endregion


        #region IPenetrable

        public float GetDamageReduce()
        {
            return _penetratingDamageReduce;
        }

        #endregion
    }
}