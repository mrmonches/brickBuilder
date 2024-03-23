using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private BrickDataSO OutlineData;

    private bool outlineCheck = true;

    private BrickController _brickController;

    public bool OutlineCheck { get => outlineCheck; private set => outlineCheck = value; }

    public bool BrickCheck(BrickDataSO data)
    {
        if (data.BrickColor == OutlineData.BrickColor && data.BrickType == OutlineData.BrickType)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    public bool BrickCheck(GameObject brick)
    {
        if (brick.CompareTag("Brick") && brick.GetComponent<BrickController>() != null)
        {
            _brickController = brick.GetComponent<BrickController>();

            BrickDataSO brickData = _brickController.BrickData;

            if (brickData.BrickColor == OutlineData.BrickColor && brickData.BrickType == OutlineData.BrickType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("Brick"))
        //{
        //    if (other.gameObject.GetComponent<BrickController>().IsPlaced && 
        //        BrickCheck(other.gameObject) && other.gameObject.transform.position == gameObject.transform.position)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
