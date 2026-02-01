using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outloud.Common
{
    public class AudioManager : MonoBehaviour
    {
        [System.Serializable]
        public struct AudioSelection
        {
            public string audioID;
            public AudioClip[] clips;
        }

        public AudioSelection[] AudioList;
        public AudioSource Source;

        List<GameObject> loops = new List<GameObject>();

        static AudioManager _instance;
        static AudioManager Instance
        {
            get
            {
                if (_instance == null || !_instance)
                {
                    _instance = FindFirstObjectByType<AudioManager>();
                    if (_instance == null)
                    {
                        var go = new GameObject("AudioManager");
                        _instance = go.AddComponent<AudioManager>();
                    }
                }

                return _instance;
            }
            set => _instance = value;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                if (Instance && Instance != this)
                    Destroy(Instance);
            }

            Instance = this;
        }

        public static bool Exists
        {
            get => Instance != null && Instance;
        }

        public static void PlaySound(string ID)
        {
            var clip = GetClip(ID);
            if (clip != null)
                Instance.Source.PlayOneShot(clip);
        }

        public static void PlaySound(AudioClip clip)
        {
            if (clip != null)
                Instance.Source.PlayOneShot(clip);
        }

        static AudioClip GetClip(string ID)
        {
            foreach (var item in Instance.AudioList)
            {
                if (item.audioID.Equals(ID, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    var clip = item.clips[Random.Range(0, item.clips.Length)];
                    return clip;
                }
            }
            Debug.LogWarning($"Sound '{ID}' not found!");
            return null;
        }

        public static void PlayLoop(string ID)
        {
            var clip = GetClip(ID);
            if (clip != null)
            {
                var go = new GameObject(ID);
                var source = go.AddComponent<AudioSource>();
                source.loop = true;
                source.clip = clip;
                source.Play();
                Instance.loops.Add(go);
            }
        }

        public static void StopLoop(string ID)
        {
            foreach (var loop in Instance.loops)
            {
                if (loop.name == ID)
                {
                    Instance.loops.Remove(loop);
                    Destroy(loop);
                    return;
                }
            }
        }

        public static void StopAllLoops()
        {
            foreach (var loop in Instance.loops)
            {
                Destroy(loop);
            }
            Instance.loops.Clear();
        }
    }
}
