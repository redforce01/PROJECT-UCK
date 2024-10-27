//===========================================================
// # ReadMe
//  - 각 UI들의 enum 형 이름은 prefab 파일 이름과 같아야 한다.
//  - "enum Type name" and "prefab name" are same!
//  - Popup 과 Panel을 구분하는 방법 :
//   > Game에서 ESC 키를 눌러서 UI가 닫히는 경우에는 Popup으로,
//   > 그렇지 않은 경우에는 Panel로 구분한다.
//===========================================================

namespace UCK
{
    public enum UIList
    {
        PANEL_START,

        LoadingUI,

        GameHUD_UI,
        Indicator_UI,
        MenuPopup_UI,
        MinimapUI,


        PANEL_END,
        POPUP_START,

        Interaction_UI,


        POPUP_END,
    }
}
