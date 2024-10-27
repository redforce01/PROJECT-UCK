//===========================================================
// # ReadMe
//  - �� UI���� enum �� �̸��� prefab ���� �̸��� ���ƾ� �Ѵ�.
//  - "enum Type name" and "prefab name" are same!
//  - Popup �� Panel�� �����ϴ� ��� :
//   > Game���� ESC Ű�� ������ UI�� ������ ��쿡�� Popup����,
//   > �׷��� ���� ��쿡�� Panel�� �����Ѵ�.
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
