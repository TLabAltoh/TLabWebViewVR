using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TLab.InputField
{
    public static class Util
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern bool IsMobile();
#endif

        public static bool mobile => _IsMobile();

        public static bool _IsMobile()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#elif UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }
    }

    public class SyncUtility : MonoBehaviour
    {
        static protected SyncUtility m_Instance;

        static public SyncUtility instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject o = new GameObject("SyncHandler");
                    DontDestroyOnLoad(o);
                    m_Instance = o.AddComponent<SyncUtility>();
                }

                return m_Instance;
            }
        }

        public void OnDisable()
        {
            if (m_Instance)
            {
                Destroy(m_Instance.gameObject);
            }
        }

        public static Coroutine StartStaticCoroutine(IEnumerator coroutine)
        {
            return instance.StartCoroutine(coroutine);
        }

        public static IEnumerator AfterFrame(UnityEvent callback, int delay)
        {
            for (int i = 0; i < delay; i++)
            {
                yield return null;
            }

            callback.Invoke();
        }

        public static IEnumerator AfterFrame(UnityAction callback, int delay)
        {
            for (int i = 0; i < delay; i++)
            {
                yield return null;
            }

            callback.Invoke();
        }

        public static IEnumerator AfterSecound(UnityEvent callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }

        public static IEnumerator AfterSecound(UnityAction callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }
    }

    public static class AudioUtility
    {
        public static void ShotAudio(AudioSource audioSource, AudioClip audioClip, float delay)
        {
            if (audioSource != null && audioClip != null)
            {
                SyncUtility.StartStaticCoroutine(
                    SyncUtility.AfterSecound(() => { audioSource.PlayOneShot(audioClip, 1.0f); }, delay));
            }
        }
    }
}
