using System;

namespace GameMain
{
    using System.Collections;
    using UnityEngine.UI;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditorInternal;
#endif

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteImage;

        [SerializeField, HideInInspector] Sprite[] spriteTextures;

        [SerializeField] float animationFrameSeconds;

        [SerializeField] bool playOnStart;

        public bool loop;

        Coroutine runCoroutine;

        private void Start()
        {
            if (!playOnStart)
                return;

            SpriteAnimeStart();
        }

        public void SpriteAnimeStart()
        {
            if (runCoroutine == null)
            {
                runCoroutine = StartCoroutine(SpriteAnimeCoroutine());
            }
        }

        IEnumerator SpriteAnimeCoroutine()
        {
            if (loop)
            {
                while (loop)
                {
                    for (int i = 0; i < spriteTextures.Length; i++)
                    {
                        spriteImage.sprite = spriteTextures[i];
                        yield return new WaitForSeconds(animationFrameSeconds);
                    }
                }

                runCoroutine = null;
                yield break;
            }
            else
            {
                for (int i = 0; i < spriteTextures.Length; i++)
                {
                    spriteImage.sprite = spriteTextures[i];
                    yield return new WaitForSeconds(animationFrameSeconds);
                }

                runCoroutine = null;
            }
        }


#if UNITY_EDITOR
        [CustomEditor(typeof(SpriteAnimation))]
        public class SpriteAnimationEditor : Editor
        {
            ReorderableList reorderableList;

            void OnEnable()
            {
                SerializedProperty prop = serializedObject.FindProperty("spriteTextures");

                reorderableList = new ReorderableList(serializedObject, prop);

                reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "アニメーションに使用する画像");
                reorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    SerializedProperty element = prop.GetArrayElementAtIndex(index);
                    rect.height -= 4;
                    rect.y += 2;
                    EditorGUI.PropertyField(rect, element, new GUIContent("フレーム" + index));
                };
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                serializedObject.Update();
                reorderableList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}