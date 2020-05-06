using UnityEngine;


namespace FPSLogic
{
    public abstract class BaseSceneObject : MonoBehaviour
    {
        #region Fields

        private int _layer;

        #endregion


        #region Properties

        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }

        #endregion


        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = GetComponent<Transform>();
        }


        #region Methods

        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        #endregion
    }
}