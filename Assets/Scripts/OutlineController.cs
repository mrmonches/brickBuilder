using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private BrickDataSO OutlineData;

    private GameObject currentBrick;

    private bool outlineCheck = true;

    public bool OutlineCheck { get => outlineCheck; private set => outlineCheck = value; }

    public void BrickCheck(BrickDataSO data, GameObject brick)
    {
        if (currentBrick == null)
        {
            if (data.Color == OutlineData.Color && data.BrickType == OutlineData.BrickType)
            {
                OutlineCheck = true;

                currentBrick = brick;

                currentBrick.GetComponent<BrickController>().GoToSelectedSpot(gameObject);
            }
            else
            {
                OutlineCheck = false;
            }
        }
    }
}
