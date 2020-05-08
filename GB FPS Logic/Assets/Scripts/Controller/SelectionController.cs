using System;
using UnityEngine;


namespace FPSLogic
{
    public sealed class SelectionController : BaseController, IExecute
    {
        #region Fields

        private readonly Camera _mailCamera;
        private readonly Vector2 _center;
        private readonly float _dedicatedDistance = 20.0f;
        private GameObject _dedicatedObject;
        private ISelectObject _selectedObject;
        private bool _nullString;
        private bool _isSelectedObject;

        #endregion


        #region ClassLifeCycles

        public SelectionController()
        {
            _mailCamera = Camera.main;
            _center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }

        #endregion


        #region Methods

        private void SelectObject(GameObject obj)
        {
            if (obj == _dedicatedObject) return;
            _selectedObject = obj.GetComponent<ISelectObject>();

            if (_selectedObject != null)
            {
                UI.SelectionObjMessageUI.Text = _selectedObject.GetMessage();
                _isSelectedObject = true;
            }
            else
            {
                UI.SelectionObjMessageUI.Text = string.Empty;
                _isSelectedObject = false;
            }
            _dedicatedObject = obj;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (Physics.Raycast(_mailCamera.ScreenPointToRay(_center),
                                out var hit,
                                _dedicatedDistance))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UI.SelectionObjMessageUI.Text = string.Empty;
                _nullString = true;
                _isSelectedObject = false;
                _dedicatedObject = null;
            }

            if (_isSelectedObject)
            {
                switch (_selectedObject)
                {
                    case Weapon aimedWeapon:
                        if (aimedWeapon.CanPickUp)
                        {
                            if (Input.GetKeyDown(ServiceLocator
                                                .Resolve<InputController>()
                                                .UseKey))
                            {
                                ServiceLocator.Resolve<Inventory>()
                                              .AddWeapon(aimedWeapon);
                            }
                        }
                        break;
                    case Wall wall:
                        break;
                }
            }
        }

        #endregion
    }
}