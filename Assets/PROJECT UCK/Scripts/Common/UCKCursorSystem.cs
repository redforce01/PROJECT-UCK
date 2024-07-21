using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    // Mouse Cursor(커서) 의 Visible 상태를 제어하는 클래스
    public class UCKCursorSystem : MonoBehaviour
    {
        private void Start()
        {
            // 처음 실행 시 Start 타이밍에서. Cursor를 보이지 않게 설정한다.
            SetCursorState(false);
        }

        // Cursor의 Visible 상태를 변경해주는 static 함수
        public static void SetCursorState(bool isVisible)
        {
            if (isVisible) // Cursor의 Visible을 보이게 만든다.
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else // Cursor를 안보이게 만들고, Lock(잠금) 시킨다.
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
