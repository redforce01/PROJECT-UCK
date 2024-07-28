using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UCK
{
    public class GameHUD_UI : MonoBehaviour
    {
        public static GameHUD_UI Instance;

        public Image hpBar;
        public Image spBar;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SetHPValue(float value)
        {
            hpBar.fillAmount = value;
        }

        public void SetSPValue(float value)
        {
            spBar.fillAmount = value;
        }
    }
}
