using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UCK
{
    public class Main : MonoBehaviour
    {
        // Main 클래스는 프로젝트의 시작점으로.
        // #1. 각 종 시스템의 초기화
        // #2. Scene을 관리하는 기능을 내포하고 있다.
        // #3. Main 이라는 GameObject는 항상 죽지않고, 살아있으며, 어느곳에서든 접근이 가능한 형태.

        public static Main Instance { get; private set; } = null;

        public enum SceneType
        {
            TitleScene,
            GameScene,
        }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            // BootStrapper 클래스에서 처리하기 힘든 시스템 구조인것들을 초기화.
            // 예시 => Resources 에서 Instantiate를 해서 prefab을 생성해야하는 시스템 이라던지..?

            // Next Workflow ?
            // Game Scene[ex: Title Scene?] 으로 이동.

            LoadScene(SceneType.TitleScene);
        }

        private SceneBase currentSceneController = null;

        public void LoadScene(SceneType sceneType)
        {
            GameObject sceneController = new GameObject($"{sceneType}_Controller");
            sceneController.transform.SetParent(transform);
            SceneBase newSceneController = null;
            switch (sceneType)
            {
                case SceneType.TitleScene:
                    newSceneController = sceneController.AddComponent<TitleScene>();
                    break;
                case SceneType.GameScene:
                    newSceneController = sceneController.AddComponent<GameScene>();
                    break;
            }

            StartCoroutine(SceneLoadCoroutine(newSceneController));
        }

        private IEnumerator SceneLoadCoroutine<T>(T newSceneController) where T : SceneBase
        {
            // Loading UI를 띄워놓자.
            LoadingUI loadingUI = UIManager.Show<LoadingUI>(UIList.LoadingUI);
            loadingUI.LoadingProgress = 0f;

            // Empty Scene을 로드해서, 비어있는 씬을 하나 올려놓자.
            AsyncOperation emptySceneLoad = SceneManager.LoadSceneAsync("Empty", LoadSceneMode.Additive);
            while (!emptySceneLoad.isDone)
            {
                yield return null;
            }

            // 만약에 기존에 불러다놓은 Scene이 있다면?
            //  => 기존 Scene의 SceneEnd를 호출하고 제거한다.
            if (currentSceneController != null)
            {
                yield return StartCoroutine(currentSceneController.SceneEnd());
                loadingUI.LoadingProgress = 0.4f;
                yield return new WaitForSeconds(0.1f);
            }

            // 기존 Scene이 제거되고 나면, 새로운 Scene을 로드한다.
            yield return StartCoroutine(newSceneController.SceneStart());
            loadingUI.LoadingProgress = 0.8f;
            yield return new WaitForSeconds(0.1f);

            currentSceneController = newSceneController;

            // Loading UI를 다시 닫아주자.
            loadingUI.LoadingProgress = 1f;
            yield return new WaitForSeconds(0.1f);

            UIManager.Hide<LoadingUI>(UIList.LoadingUI);
        }
    }
}
