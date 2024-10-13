using UnityEngine;

public class Platform : MonoBehaviour   
{
    [SerializeField] private Destroyer _destroyer;

    public void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(_destroyer.Package(gameObject, collision.gameObject));
    }
}
