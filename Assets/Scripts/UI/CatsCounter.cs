using Core;
using Mechanics;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CatsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI catsTextCounter;
    private TamedCatsModel catsModel = new TamedCatsModel();

    void Update()
    {
        catsModel.cats = new List<string>();

        string tamedCatsJson = PlayerPrefs.GetString(SaveKeys.TAMED_CATS, null);
        if (tamedCatsJson.Length > 0)
        {
            catsModel.cats = JsonUtility.FromJson<TamedCatsModel>(tamedCatsJson).cats;
        }

        catsTextCounter.text = $"{catsModel.cats.Count} / 20";
    }
}
