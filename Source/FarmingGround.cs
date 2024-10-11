using UnityEngine;

namespace Assets.Sources.Object
{
    internal class FarmingGround : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Soil"))
                Destroy(this.gameObject);
        }
    }
}
