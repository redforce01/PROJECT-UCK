using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UCK
{
    public class LoadingUI : UIBase
    {
        public static LoadingUI Instance { get; private set; }

        public float LoadingProgress 
        {
            get => progress;
            set
            {
                progress = value;
                progressText.text = $"{progress * 100f:0}%";
                progressImage.fillAmount = progress;                
            } 
        }

        private float progress = 0f;

        public TextMeshProUGUI progressText;
        public Image progressImage;

        public void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
