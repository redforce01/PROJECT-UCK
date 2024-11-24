using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCK
{
    public class UserDataModel : SingletonBase<UserDataModel>
    {
        [field: SerializeField] public PlayerSessionDTO PlayerSessionData { get; private set; } = new PlayerSessionDTO();
        [field: SerializeField] public PlayerDTO PlayerData { get; private set; } = new PlayerDTO();

        public void Initialize()
        {
            //PlayerSessionData.lastPosition = new Vector3(10, 3, 10);
            //PlayerSessionData.lastHP = 98;
            //PlayerSessionData.lastMP = 50;

            //PlayerData.nickName = "REDFORCE";
            //PlayerData.level = 1;
            //PlayerData.money = 1000;
            //PlayerData.exp = 234;

            //SaveData<PlayerSessionDTO>(PlayerSessionData);
            //SaveData<PlayerDTO>(PlayerData);

            if (LoadData<PlayerSessionDTO>(out PlayerSessionDTO PlayerSessionData))
            {
                this.PlayerSessionData = PlayerSessionData;
            }

            if (LoadData<PlayerDTO>(out PlayerDTO PlayerData))
            {
                this.PlayerData = PlayerData;
            }
        }

        public void SetPlayerSessionData(Vector3 position, float hp, float mp)
        {
            PlayerSessionData.lastPosition = position;
            PlayerSessionData.lastHP = hp;
            PlayerSessionData.lastMP = mp;

            SaveData<PlayerSessionDTO>(PlayerSessionData);
        }

        public void SetPlayerData(string nickName, int level, int money, float exp)
        {
            PlayerData.nickName = nickName;
            PlayerData.level = level;
            PlayerData.money = money;
            PlayerData.exp = exp;

            SaveData<PlayerDTO>(PlayerData);
        }

        public void SaveData<T>(T data) where T : UserDataDTO
        {
            string toJson = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(typeof(T).Name, toJson);

            Debug.Log($"Save Success - {typeof(T).Name} : {toJson}");
        }

        public bool LoadData<T>(out T data) where T : UserDataDTO
        {
            string savedData = PlayerPrefs.GetString(typeof(T).Name, string.Empty);

            if (string.IsNullOrEmpty(savedData))
            {
                data = default(T);
                Debug.Log($"Load Failed - Default Data <{typeof(T).Name}>");
                return false;
            }

            data = JsonUtility.FromJson<T>(savedData);
            Debug.Log($"Load Success - Saved Data <{typeof(T).Name}> : {savedData}");

            return true;
        }
    }
}
