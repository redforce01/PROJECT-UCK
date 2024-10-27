using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class UIManager : SingletonBase<UIManager>
    {
        public static T Show<T>(UIList uiName) where T : UIBase
        {
            var targetUI = Singleton.GetUI<T>(uiName);
            targetUI.Show();

            Singleton.activatedUIs.Add(targetUI);

            return targetUI;
        }

        public static T Hide<T>(UIList uiName) where T : UIBase
        {
            var targetUI = Singleton.GetUI<T>(uiName);
            targetUI.Hide();

            Singleton.activatedUIs.Remove(targetUI);

            return targetUI;
        }

        private Dictionary<UIList, UIBase> panels = new Dictionary<UIList, UIBase>();
        private Dictionary<UIList, UIBase> popups = new Dictionary<UIList, UIBase>();

        [SerializeField] private Transform panelRoot;
        [SerializeField] private Transform popupRoot;

        private List<UIBase> activatedUIs = new List<UIBase>();
        private List<UIList> autoHideExceptList = new List<UIList>()
        {
            UIList.LoadingUI,
        };

        public void Initialize()
        {
            if (panelRoot == null)
            {
                GameObject goPanelRoot = new GameObject("PanelRoot");
                panelRoot = goPanelRoot.transform;
                panelRoot.SetParent(transform);
                panelRoot.localPosition = Vector3.zero;
                panelRoot.localRotation = Quaternion.identity;
                panelRoot.localScale = Vector3.one;
            }

            if (popupRoot == null)
            {
                GameObject goPopupRoot = new GameObject("PopupRoot");
                popupRoot = goPopupRoot.transform;
                popupRoot.SetParent(transform);
                popupRoot.localPosition = Vector3.zero;
                popupRoot.localRotation = Quaternion.identity;
                popupRoot.localScale = Vector3.one;
            }

            for (int index = (int)UIList.PANEL_START + 1; index < (int)UIList.PANEL_END; index++)
            {
                panels.Add((UIList)index, null);
            }

            for (int index = (int)UIList.POPUP_START + 1; index < (int)UIList.POPUP_END; index++)
            {
                popups.Add((UIList)index, null);
            }
        }

        public T GetUI<T>(UIList uiName) where T : UIBase
        {
            if (UIList.PANEL_START < uiName && uiName < UIList.PANEL_END)
            {
                if (panels[uiName] == null)
                {
                    string path = $"UI/{uiName}";
                    UIBase uiResource = Resources.Load<UIBase>(path);

                    if (uiResource == null)
                        return null;

                    UIBase newInstance = Instantiate(uiResource, panelRoot);
                    newInstance.gameObject.SetActive(false);

                    panels[uiName] = newInstance;
                    return newInstance as T;
                }
                else
                {
                    return panels[uiName] as T;
                }
            }

            if (UIList.POPUP_START < uiName && uiName < UIList.POPUP_END)
            {
                if (popups[uiName] == null)
                {
                    string path = $"UI/{uiName}";
                    UIBase uiResource = Resources.Load<UIBase>(path);

                    if (uiResource == null)
                        return null;

                    UIBase newInstance = Instantiate(uiResource, popupRoot);
                    newInstance.gameObject.SetActive(false);

                    popups[uiName] = newInstance;
                    return newInstance as T;
                }
                else
                {
                    return popups[uiName] as T;
                }
            }

            return null;
        }

        public void HideAllUI()
        {
            HideAllPopup();
            HideAllPanel();
        }

        public void HideAllPopup()
        {
            foreach (var popup in popups)
            {
                if (autoHideExceptList.Contains(popup.Key))
                    continue;

                if (popup.Value != null)
                {
                    popup.Value.Hide();
                }
            }
        }

        public void HideAllPanel()
        {
            foreach (var panel in panels)
            {
                if (autoHideExceptList.Contains(panel.Key))
                    continue;

                if (panel.Value != null)
                {
                    panel.Value.Hide();
                }
            }
        }
    }
}
