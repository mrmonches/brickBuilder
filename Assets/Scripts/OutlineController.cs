using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private BrickDataSO OutlineData;

    private bool outlineCheck = true;

    public bool OutlineCheck { get => outlineCheck; private set => outlineCheck = value; }

    public bool BrickCheck(BrickDataSO data)
    {
        if (data.Color == OutlineData.Color && data.BrickType == OutlineData.BrickType)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            if (other.gameObject.GetComponent<BrickController>().IsPlaced)
            {
                Destroy(gameObject);
            }
        }
    }
}
