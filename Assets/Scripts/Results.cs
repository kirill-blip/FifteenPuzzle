using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Results : MonoBehaviour
    {
        public Data Data;

        [SerializeField] private List<TextMeshProUGUI> _resultsTexts;

        [DllImport("__Internal")]
        private static extern void GetData(string firstKey, string secondKey, string thirdKey);

        private void OnEnable()
        {
            GetData("3x3", "4x4", "5x5");
        }

        public void VisualizeResults(string json)
        {
            Data = JsonUtility.FromJson<Data>(json);

            foreach (var item in Data.keys)
            {
                Debug.Log($"Key: {item.key}. Value: {item.value}");
            }

            if (_resultsTexts.Count > 0)
            {
                for (int i = 0; i < Data.keys.Length; i++)
                {
                    if (string.IsNullOrEmpty(Data.keys[i].value))
                    {
                        _resultsTexts[i].text = "0";
                        continue;
                    }

                    _resultsTexts[i].text = Data.keys[i].value;
                }
            }

        }

        public int GetResult(string name)
        {
            return Convert.ToInt32(Data.keys.FirstOrDefault(x => x.key == name).value);
        }
    }
}