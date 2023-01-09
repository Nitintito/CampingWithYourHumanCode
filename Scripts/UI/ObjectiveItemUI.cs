using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveItemUI : MonoBehaviour
{
    private int curAmount = 0;
    [SerializeField]private TextMeshProUGUI amountText;
    [NonSerialized]public static UnityEvent burnedAllItems = new UnityEvent();
    private void Start()
    {
        DetectFire.burnObjectiveItem.AddListener(AddAmount);
    }

    void AddAmount()
    {
        curAmount++;
        if (curAmount >= 6)
        {
            burnedAllItems.Invoke();
        }
        amountText.text = (curAmount + "/6");
    }
}
