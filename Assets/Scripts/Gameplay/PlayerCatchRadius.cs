using Core;
using Mechanic;
using Model;
using UnityEngine;
using static Core.Simulation;

namespace Gameplay
{
    public class PlayerCatchRadius : Simulation.Event<PlayerCatchRadius>
    {
        private readonly IsoModel model = Simulation.GetModel<IsoModel>();
        public PlayerController player;

        // Calculate angle increment for each ray
        private readonly int rayCount = 10;
        private readonly float raySize = 90f;

        public override void Execute()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 attackDir = (mousePosition - player.transform.position).normalized;

            bool collideWithCat = false;
            CatController collidedCat = null;

            // Draw rays
            for (int i = 0; i < rayCount; i++)
            {
                // Calculate direction of the ray based on current angle
                float angle = i * (raySize / rayCount);
                float distance = player.attackRadius / 2;

                // Rotate the direction vector
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                rotation *= Quaternion.AngleAxis(-45, Vector3.forward);

                // Calculate origin of the ray
                Vector2 direction = rotation * attackDir;
                Vector2 origin = (Vector2)player.transform.position + (Mathf.Pow(player.attackRadius, 2) * direction);

                // Cast ray
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);

                // Draw ray (for debugging)
                Debug.DrawRay(origin, direction * distance, (hit.collider && !hit.collider.CompareTag("Player")) ? Color.green : Color.red);

                if ((hit.collider && hit.collider.CompareTag("Cat")))
                {
                    collideWithCat = true;
                    collidedCat = hit.collider.gameObject.GetComponent<CatController>();
                }
            }

            if (collideWithCat)
            {
                var ev = Schedule<PlayerCatchCat>();
                ev.cat = collidedCat;
            }
        }
    }
}