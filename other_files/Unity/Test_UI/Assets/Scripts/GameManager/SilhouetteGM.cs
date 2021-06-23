using UnityEngine;

public class SilhouetteGM : MonoBehaviour {
    [SerializeField] private Material silhouetteMaterial = default;
    [SerializeField] private string silhouetteTag = "Silhouette";
    // ----
    private Material oldMaterial = default;
    private MeshRenderer oldRenderer= default;
    private int oldInstanceID = default;

    private void Update()
    {
        if (Camera.main is null) return;

        int instanceID = default;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit = default;
        if (Physics.Raycast(ray, out raycastHit)) {
            if (raycastHit.transform.CompareTag(silhouetteTag)) {
                instanceID = raycastHit.transform.GetInstanceID();
            }
        }

        if (instanceID != oldInstanceID)
        {
            if (oldInstanceID != default && oldRenderer != default&& oldMaterial) {
                oldRenderer.material = oldMaterial;

                oldInstanceID = default;
                oldMaterial = default;
                oldRenderer = default;
            }

            if (instanceID != default) {
                oldRenderer = raycastHit.transform.GetComponent<MeshRenderer>();
                oldMaterial = oldRenderer.material;
                oldInstanceID = instanceID;
                
                oldRenderer.material = silhouetteMaterial;
            }
        }
    }
}
