using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UCK
{
    public class LoadingUI : UIBase
    {
        public static LoadingUI Instance => UIManager.Singleton.GetUI<LoadingUI>(UIList.LoadingUI);

        /// <summary> Progress �� �׻� 0 ~ 1 ���� ������ ���� �� </summary>
        public float LoadingProgress
        {
            set
            {
                loadingBar.fillAmount = value;
                loadingText.text = $"{value * 100f :0.0} %";
            }
        }

        [SerializeField] private UnityEngine.UI.Image loadingBar;
        [SerializeField] private TextMeshProUGUI loadingText;
    }
}
