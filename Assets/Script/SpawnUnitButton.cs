using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnUnitButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public string CharacterID;
    public bool aiSpawnTest = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnRequest spawnRequest =
            new SpawnRequest(CharacterID, aiSpawnTest ? Team.P1 : Team.P2);
        SpawnManager.Instance.SendSpawnRequest(spawnRequest);
    }
}
