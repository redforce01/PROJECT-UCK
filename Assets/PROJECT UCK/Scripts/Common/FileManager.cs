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
        // [������ ������]
        // Application.persistentDataPath : ����ڵ��丮/AppData/LocalLow/ȸ���̸�/���δ�Ʈ�̸�
        //  ���� �б� ���� ����
        // Application.dataPath : ������Ʈ���丮/Assets
        // Application.streamingAssetsPath : ������Ʈ���丮/Assets/StreamingAssets
        //  ���� �б� ���� ����

        // [������ �������α׷�]
        // Application.persistentDataPath : ����ڵ��丮/AppData/LocalLow/ȸ���̸�/���δ�Ʈ�̸�
        //  ���� �б� ���� ����
        // Application.dataPath : ��������/��������_Data
        // Application.streamingAssetsPath : ��������/��������_Data/StreamingAssets
        //  ���� �б� ���� ����

        // [�� ������]
        // Application.persistentDataPath : ����ڵ��丮/Library/Caches/unity.ȸ���̸�.���δ�Ʈ�̸�
        //  ���� �б� ���� ����
        // Application.dataPath : ������Ʈ���丮/Assets
        // Application.streamingAssetsPath : ������Ʈ���丮/Assets/StreamingAssets
        //  ���� �б� ���� ����

        // [�� �������α׷�]
        // Application.persistentDataPath : ����ڵ��丮/Library/Caches/unity.ȸ���̸�.���δ�Ʈ�̸�
        //  ���� �б� ���� ����
        // Application.dataPath : ��������.app/Contents
        // Application.streamingAssetsPath : ��������.app/Contents/Data/StreamingAssets
        //  ���� �б� ���� ����

        // [�� �÷���]
        // �������� ������� ���� ���� �Ұ���. �ּ¹���� �ؾ���
        // Application.persistentDataPath : /
        // plication.dataPath : unity3d������ �ִ� ����
        // Application.streamingAssetsPath : �� ����.


        // [�ȵ���̵� External]
        // Application.persistentDataPath : /mnt/sdcard/Android/data/�����̸�/files
        // ���� �б� ���� ����
        // Application.dataPath : /data/app/�����̸�-��ȣ.apk
        // Application.streamingAssetsPath : jar:/*file:///data/app/�����̸�.apk!/assets */
        // ������ �ƴ� WWW�� �б� ����

        // [�ȵ���̵� Internal]
        // Application.persistentDataPath : /data/data/�����̸�/files/
        // ���� �б� ���� ����
        // Application.dataPath : /data/app/�����̸�-��ȣ.apk
        // Application.streamingAssetsPath : jar:/*file:///data/app/�����̸�.apk!/assets*/
        // ������ �ƴ� WWW�� �б� ����

        // [iOS]
        // Application.persistentDataPath : /var/mobile/Applications/���α׷�ID/Documents
        // ���� �б� ���� ����
        // Application.dataPath : /var/mobile/Applications/���α׷�ID/���̸�.app/Data
        // Application.streamingAssetsPath : /var/mobile/Applications/���α׷�ID/���̸�.app/Data/Raw
        // ���� �б� ����, ���� �Ұ���
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
