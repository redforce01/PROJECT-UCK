using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace UCK
{
    public class FileManager
    {

        // <Path Manual> If u forget anything path point, open this region.
        #region FILEMANAGER_PATH_MANUAL
        // [윈도우 에디터]
        // Application.persistentDataPath : 사용자디렉토리/AppData/LocalLow/회사이름/프로덕트이름
        //  파일 읽기 쓰기 가능
        // Application.dataPath : 프로젝트디렉토리/Assets
        // Application.streamingAssetsPath : 프로젝트디렉토리/Assets/StreamingAssets
        //  파일 읽기 쓰기 가능

        // [윈도우 응용프로그램]
        // Application.persistentDataPath : 사용자디렉토리/AppData/LocalLow/회사이름/프로덕트이름
        //  파일 읽기 쓰기 가능
        // Application.dataPath : 실행파일/실행파일_Data
        // Application.streamingAssetsPath : 실행파일/실행파일_Data/StreamingAssets
        //  파일 읽기 쓰기 가능

        // [맥 에디터]
        // Application.persistentDataPath : 사용자디렉토리/Library/Caches/unity.회사이름.프로덕트이름
        //  파일 읽기 쓰기 가능
        // Application.dataPath : 프로젝트디렉토리/Assets
        // Application.streamingAssetsPath : 프로젝트디렉토리/Assets/StreamingAssets
        //  파일 읽기 쓰기 가능

        // [맥 응용프로그램]
        // Application.persistentDataPath : 사용자디렉토리/Library/Caches/unity.회사이름.프로덕트이름
        //  파일 읽기 쓰기 가능
        // Application.dataPath : 실행파일.app/Contents
        // Application.streamingAssetsPath : 실행파일.app/Contents/Data/StreamingAssets
        //  파일 읽기 쓰기 가능

        // [웹 플랫폼]
        // 웹에서는 명시적인 파일 쓰기 불가능. 애셋번들로 해야함
        // Application.persistentDataPath : /
        // plication.dataPath : unity3d파일이 있는 폴더
        // Application.streamingAssetsPath : 값 없음.


        // [안드로이드 External]
        // Application.persistentDataPath : /mnt/sdcard/Android/data/번들이름/files
        // 파일 읽기 쓰기 가능
        // Application.dataPath : /data/app/번들이름-번호.apk
        // Application.streamingAssetsPath : jar:/*file:///data/app/번들이름.apk!/assets */
        // 파일이 아닌 WWW로 읽기 가능

        // [안드로이드 Internal]
        // Application.persistentDataPath : /data/data/번들이름/files/
        // 파일 읽기 쓰기 가능
        // Application.dataPath : /data/app/번들이름-번호.apk
        // Application.streamingAssetsPath : jar:/*file:///data/app/번들이름.apk!/assets*/
        // 파일이 아닌 WWW로 읽기 가능

        // [iOS]
        // Application.persistentDataPath : /var/mobile/Applications/프로그램ID/Documents
        // 파일 읽기 쓰기 가능
        // Application.dataPath : /var/mobile/Applications/프로그램ID/앱이름.app/Data
        // Application.streamingAssetsPath : /var/mobile/Applications/프로그램ID/앱이름.app/Data/Raw
        // 파일 읽기 가능, 쓰기 불가능
        #endregion

        public static string pathForDocumentsFile(string filename)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(Path.Combine(path, "Documents"), filename);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                string path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, filename);
            }
            else
            {
                string path = Application.dataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                var result = Path.Combine(path, filename);
                return result;
            }
        }

        /// <summary>
        /// Simple Generic Save Text Method From String
        /// [Sample Path] "Assets/Resources/Test.txt"
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="jsonData"></param>
        public static void WriteFileFromString(string localPath, string jsonData)
        {
            string dataPath = pathForDocumentsFile(localPath);
            int dirIndex = dataPath.LastIndexOf('/');
            string dirPath = dataPath.Substring(0, dirIndex + 1);

            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            if (!File.Exists(dataPath))
            {
                FileStream fs = File.Create(dataPath);
                fs.Close();
            }
            File.WriteAllText(dataPath, jsonData, Encoding.UTF8);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            Debug.LogFormat("WriteFileFromString - Save Complete. Path:{0}", localPath);
        }

        /// <summary>
        /// Simple Binary Save Method From String
        /// [Sample Path] "Assets/Resources/Test.txt"
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="jsonData"></param>
        public static void WriteBinaryFileFromString(string localPath, string jsonData)
        {
            string dataPath = pathForDocumentsFile(localPath);
            int dirIndex = dataPath.LastIndexOf('/');
            string dirPath = dataPath.Substring(0, dirIndex + 1);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            if (!File.Exists(dataPath))
            {
                FileStream fs = File.Create(dataPath);
                fs.Close();
            }
            File.WriteAllText(dataPath, jsonData, Encoding.UTF8);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            Debug.LogFormat("WriteBinaryFileFromString - Save Complete. Path:{0}", localPath);
        }

        /// <summary>
        /// Simple Read Generic File Data To String
        /// [Sample Path] "Assets/Resources/Test.txt" in Editor
        /// [Sample Path] "PersistantData/Text.txt" in BuildFlatform
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="receiveData"></param>
        public static bool ReadFileData(string localPath, out string receiveData)
        {
            string dataPath = pathForDocumentsFile(localPath);
            receiveData = null;
            try
            {
                receiveData = File.ReadAllText(dataPath);

                if (string.IsNullOrEmpty(receiveData))
                    return false;

                Debug.LogFormat("ReadFileData - Load Complete. Path:{0}", localPath);
                return true;
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.LogErrorFormat("FileManager catched - '{0}' Directory Not Found Exception, Please Check Your Path Parameter or File Exist", dataPath);
                Debug.LogError(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Debug.LogErrorFormat("FileManager catched - '{0}' File Not Found Exception, Please Check Your Path Parameter or File Exist", dataPath);
                Debug.LogError(e.Message);
            }

            return false;
        }

        /// <summary>
        /// Simple Read Binary Data To String
        /// [Sample Path] "Assets/Resources/Test.txt" in Editor
        /// [Sample Path] "PersistantData/Text.txt" in BuildFlatform
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="receiveData"></param>
        public static bool ReadBinaryData(string localPath, out string receiveData)
        {
            string dataPath = pathForDocumentsFile(localPath);
            receiveData = null;

            try
            {
                var readData = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(dataPath));
                receiveData = readData;

                if (string.IsNullOrEmpty(receiveData))
                    return false;

                Debug.LogFormat("ReadBinaryData - Load Complete. Path:{0}", localPath);
                return true;
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.LogErrorFormat("FileManager catched - '{0}' Directory Not Found Exception, Please Check Your Path Parameter or File Exist", dataPath);
                Debug.LogError(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Debug.LogErrorFormat("FileManager catched - '{0}' File Not Found Exception, Please Check Your Path Parameter or File Exist", dataPath);
                Debug.LogError(e.Message);
            }

            return false;
        }
    }
}
