using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class BootStrapper : MonoBehaviour
    {
        public static bool IsSystemLoaded { get; private set; } = false;

        private static List<string> AutoBootStrapperScenes = new List<string>()
        {
            "MainScene",
            "GameScene", // <- 자동으로 시스템이 부팅 되는 씬의 이름을 추가. [유니티 씬 파일이름]
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SystemBoot()
        {
            IsSystemLoaded = false;

#if UNITY_EDITOR
            var activeScene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
            for (int i = 0; i < AutoBootStrapperScenes.Count; i++)
            {
                if (activeScene.name.Equals(AutoBootStrapperScenes[i]))
                {
                    InternalSystemBoot();
                    break;
                }
            }
            IsSystemLoaded = true;
#else
            InternalSystemBoot();
            IsSystemLoaded = true;
#endif
        }

        private static void InternalSystemBoot()
        {
            // TODO : 시스템 초기화가 필요한 것들을 추가.


            // UI System 초기화
            UIManager.Singleton.Initialize();
        }
    }
}
