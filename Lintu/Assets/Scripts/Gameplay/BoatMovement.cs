using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float Speed;
    public LayerMask RaycastLayer;
    public float MeshXExtent;

    int DirectionMultiplier;

    private void Start()
    {
        DirectionMultiplier = Random.Range(0, 2);
        if (DirectionMultiplier == 0)
            DirectionMultiplier = -1;
    }

    private void Update()
    {
        string layerHitted;
        RaycastHit hit;
        MeshXExtent = GetComponent<MeshRenderer>().bounds.extents.x;
        Vector3 LeftRayPos = transform.position + Vector3.left * MeshXExtent;
        Vector3 RightRayPos = transform.position + Vector3.right * MeshXExtent;

        if (Physics.Raycast(RightRayPos, Vector3.right, out hit, 2, RaycastLayer) ||
            Physics.Raycast(LeftRayPos, Vector3.left, out hit, 2, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);
            if (layerHitted == "Wall")
            {
                DirectionMultiplier *= -1;
                Debug.DrawRay(RightRayPos, Vector3.right, Color.red);
            }
        }

        transform.position += transform.forward * Speed * DirectionMultiplier * Time.deltaTime;
    }
}
