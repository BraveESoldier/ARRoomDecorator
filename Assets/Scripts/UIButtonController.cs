using UnityEngine;

public class UIButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _objectPlace;

    public void RotateObjectPlace()
    {
        _objectPlace.transform.rotation *= Quaternion.Euler(0f, 15f, 0f);
    }

    public void PlusSizeObjectPlace()
    {
        _objectPlace.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
    public void MinusSizeObjectPlace()
    {
        _objectPlace.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
