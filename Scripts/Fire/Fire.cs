using UnityEngine;

public class Fire
{
    private FireData fireData;
    private float currentTimeLeft;
    private float maxTimeLeft;
    public Fire(FireData fireData)
    {
        this.fireData = fireData;
    }

    public float CurrentTimeLeft { get => currentTimeLeft; set => currentTimeLeft = value; }
    public float MaxTimeLeft { get => maxTimeLeft; set => maxTimeLeft = value; }

    public void RestoreFire() => CurrentTimeLeft = Mathf.Clamp(CurrentTimeLeft + fireData.EachFireWoodRestore, 0, maxTimeLeft);

    public void RestoreFireToMax() => CurrentTimeLeft = maxTimeLeft;

}
