using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class SoundPlayController : MonoBehaviour
    {
        public SoundType soundType;
        public bool isBGM = false;

        public void Start()
        {
            if (isBGM)
            {
                SoundSystem.Singleton.PlayBGM(soundType);
            }
            else
            {
                SoundSystem.Singleton.PlaySFX(soundType, transform.position);
            }
        }
    }
}
