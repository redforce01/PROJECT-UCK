using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UCK
{
    public class GameHUD_UI : MonoBehaviour
    {
        public static GameHUD_UI Instance;

        public Image hpBar;
        public Image spBar;

        public TextMeshProUGUI hpText;
        public TextMeshProUGUI spText;


        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public void SetHPValue(float current, float max)
        {
            hpBar.fillAmount = current / max;
            hpText.text = string.Format("{0} / {1}", current, max); // {0} {1} : 0번째 인자, 1번째 인자
        }

        public void SetSPValue(float current, float max)
        {
            spBar.fillAmount = current / max;
            spText.text = string.Format("{0} / {1}", current, max);
        }

        public TextMeshProUGUI weaponNameText;
        public TextMeshProUGUI weaponAmmoText;

        public void SetWeaponInfo(string weaponName, int currentAmmo, int maxAmmo)
        {
            weaponNameText.text = weaponName;
            SetWeaponAmmo(currentAmmo, maxAmmo);
        }

        public void SetWeaponAmmo(int currentAmmo, int maxAmmo)
        {
            weaponAmmoText.text = string.Format("{0} / {1}", currentAmmo, maxAmmo);
        }
    }
}
