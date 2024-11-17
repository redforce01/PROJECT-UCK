using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public enum SoundType
    {
        BGM_01,
        BGM_02,

        Fire_01,
        Fire_02,
    }

    public class SoundSystem : SingletonBase<SoundSystem>
    {
        private Dictionary<SoundType, AudioClip> soundAssets = new Dictionary<SoundType, AudioClip>();

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            AudioClip[] soundAssetsBGM = Resources.LoadAll<AudioClip>("Sound/BGM/");
            AudioClip[] soundAssetsSFX = Resources.LoadAll<AudioClip>("Sound/SFX/");

            for (int i = 0; i < soundAssetsBGM.Length; i++)
            {
                string fileName = soundAssetsBGM[i].ToString();
                int index = fileName.IndexOf("(UnityEngine.AudioClip)");
                if (index >= 0)
                {
                    string trimFileName = fileName.Substring(0, index);
                    if (System.Enum.TryParse(trimFileName, out SoundType result))
                    {
                        soundAssets.Add(result, soundAssetsBGM[i]);
                    }
                }
            }

            for (int i = 0; i < soundAssetsSFX.Length; i++)
            {
                string fileName = soundAssetsSFX[i].ToString();
                int index = fileName.IndexOf("(UnityEngine.AudioClip)");
                if (index >= 0)
                {
                    string trimFileName = fileName.Substring(0, index);
                    if (System.Enum.TryParse(trimFileName, out SoundType result))
                    {
                        soundAssets.Add(result, soundAssetsSFX[i]);
                    }
                }                
            }
        }

        public void PlayBGM(SoundType type)
        {
            if (soundAssets.ContainsKey(type))
            {
                GameObject newSoundSource = new GameObject("BGM_" + type.ToString());
                AudioSource newAudioSource = newSoundSource.AddComponent<AudioSource>();
                newAudioSource.clip = soundAssets[type];
                newAudioSource.loop = true;
                newAudioSource.spatialBlend = 0f; // 2D Sound Setup
                newAudioSource.Play();
            }
        }

        public void PlaySFX(SoundType type, Vector3 position)
        {
            if (soundAssets.ContainsKey(type))
            {
                GameObject newSoundSource = new GameObject("SFX_" + type.ToString());
                AudioSource newAudioSource = newSoundSource.AddComponent<AudioSource>();
                newAudioSource.clip = soundAssets[type];
                newAudioSource.loop = false;
                newAudioSource.spatialBlend = 1f; // 3D Sound Setup
                newAudioSource.transform.position = position;
                newAudioSource.Play();

                Destroy(newSoundSource, newAudioSource.clip.length);
            }
        }
    }
}
