using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    // Mouse Cursor(Ŀ��) �� Visible ���¸� �����ϴ� Ŭ����
    public class UCKCursorSystem : MonoBehaviour
    {
        private void Start()
        {
            // ó�� ���� �� Start Ÿ�ֿ̹���. Cursor�� ������ �ʰ� �����Ѵ�.
            SetCursorState(false);
        }

        // Cursor�� Visible ���¸� �������ִ� static �Լ�
        public static void SetCursorState(bool isVisible)
        {
            if (isVisible) // Cursor�� Visible�� ���̰� �����.
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else // Cursor�� �Ⱥ��̰� �����, Lock(���) ��Ų��.
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
