using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

namespace UCK
{
    public class TitleUI : UIBase
    {
        //private void Awake()
        //{
        //    DontDestroyOnLoad(gameObject);
        //}

        public void OnClickStartButton()
        {
            //LoadingUI.Instance.Show();
            //LoadingUI.Instance.LoadingProgress = 0f;

            //StartCoroutine(LoadGameScene());

            Main.Instance.LoadScene(Main.SceneType.GameScene);
        }

        //private IEnumerator LoadGameScene()
        //{
        //    var async = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        //    while (!async.isDone)
        //    {
        //        LoadingUI.Instance.LoadingProgress = async.progress / 0.9f;
        //        yield return null;
        //    }

        //    //float dummyProgress = 0f;
        //    //while (dummyProgress < 1f)
        //    //{
        //    //    dummyProgress += 0.001f;
        //    //    LoadingUI.Instance.LoadingProgress = dummyProgress;
        //    //    yield return null;
        //    //}

        //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));            
        //    var asyncUnload = SceneManager.UnloadSceneAsync("TitleScene");
        //    while(!asyncUnload.isDone)
        //    {
        //        yield return null;
        //    }

        //    LoadingUI.Instance.LoadingProgress = 1f;
        //    LoadingUI.Instance.Hide();
        //    gameObject.SetActive(false);
        //}

        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
