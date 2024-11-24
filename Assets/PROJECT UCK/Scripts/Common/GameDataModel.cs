using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace UCK
{
    public class GameDataModel : SingletonBase<GameDataModel>
    {
        [field: SerializeField] public CharacterStatData CharacterStatData { get; private set; } = new CharacterStatData();

        public void Initialize()
        {
            //CharacterStatData.characterStats = new List<CharacterStatData.CharacterStatDTO>();
            //CharacterStatData.characterStats.Add(new CharacterStatData.CharacterStatDTO()
            //{
            //    level = 1,
            //    moveSpeed = 1.5f,
            //    hp = 100f,
            //});
            //CharacterStatData.characterStats.Add(new CharacterStatData.CharacterStatDTO()
            //{
            //    level = 2,
            //    moveSpeed = 2f,
            //    hp = 100f,
            //});
            //MakeSampleData<CharacterStatData>(CharacterStatData);

            if (LoadGameData<CharacterStatData>(out CharacterStatData statData))
            {
                this.CharacterStatData = statData;
            }
        }

        public void MakeSampleData<T>(T data) where T : GameDataDTO
        {
            string toJson = JsonUtility.ToJson(data, true);
            FileManager.WriteFileFromString($"Assets/PROJECT UCK/Resources/Game Data/{typeof(T).Name}.json", toJson);
        }

        public bool LoadGameData<T>(out T data)
        {
            TextAsset dataAsset = Resources.Load<TextAsset>($"Game Data/{typeof(T).Name}");
            Assert.IsNotNull(dataAsset, $"GameDataModel.LoadGameData : {typeof(T).Name} is not found.");

            if (dataAsset == null)
            {
                data = default;
                return false;
            }

            Debug.Log(dataAsset.text);
            data = JsonUtility.FromJson<T>(dataAsset.text);
            return true;
        }
    }
}
