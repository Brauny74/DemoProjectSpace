using UnityEngine;

public class OnMouseOver : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    protected bool IsMouseOver()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
