using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


namespace FPSLogic.Editor
{
    public sealed class CustomWindow : EditorWindow
    {
        #region Fields

        private char[] _vowels =
        {
            'a',
            'e',
            'i',
            'o',
            'u'
        };

        private char[] _consonants =
        {
            'b',
            'c',
            'd',
            'f',
            'g',
            'h',
            'j',
            'k',
            'l',
            'm',
            'n',
            'p',
            'q',
            'r',
            's',
            't',
            'v',
            'w',
            'x',
            'y',
            'z'
        };

        private System.Random r = new System.Random();

        

        #endregion


        #region Properties

        private char RandomConsonant
        {
            get
            {
                return _consonants[r.Next(0, _consonants.Length - 1)];
            }
        }

        private char RandomVowel
        {
            get
            {
                return _vowels[r.Next(0, _vowels.Length - 1)];
            }
        }

        #endregion


        #region UnityMethods

        private void OnGUI()
        {
            GUILayout.Label("Функции", EditorStyles.boldLabel);

            if(GUILayout.Button("Переименовать объекты"))
            {
                foreach (BaseSceneObject obj in GameObject.FindObjectsOfType<BaseSceneObject>())
                {
                    if (!(obj is ISelectObject)) continue;
                    obj.Name = GenerateSimpleName();
                    EditorUtility.SetDirty(obj.gameObject);
                    EditorSceneManager.MarkSceneDirty(obj.gameObject.scene);
                }
            }

            if (GUILayout.Button("Удалить объекты"))
            {
                foreach (BaseSceneObject obj in GameObject.FindObjectsOfType<BaseSceneObject>())
                {
                    if (obj.transform.childCount != 0) continue;
                    DestroyImmediate(obj.gameObject);
                }
            }
        }

        #endregion


        #region Methods

        private string GenerateSimpleName()
        {
            char[] result = new char[]
                {
                char.ToUpper(RandomConsonant),
                RandomVowel,
                RandomConsonant,
                RandomConsonant,
                RandomVowel
                };

            return new string(result);
        }

        #endregion
    }
}