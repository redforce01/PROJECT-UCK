using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class TitleScene : SceneBase
    {
        public override float SceneLoadingProgress { get; protected set; }
        public override IEnumerator SceneStart()
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TitleScene");

            while (!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }

            // Ÿ��Ʋ UI�� ����ش�.
            // UIManager.Show<TitleUI>(UIList.TitleUI);
        }

        public override IEnumerator SceneEnd()
        {
            // Ÿ��Ʋ UI�� �����ش�.
            UIManager.Hide<TitleUI>(UIList.TitleUI);

            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("TitleScene");
            while(!async.isDone)
            {
                yield return null;
                SceneLoadingProgress = async.progress;
            }
        }
    }
}
