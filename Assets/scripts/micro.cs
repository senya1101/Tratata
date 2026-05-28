using Unity.Netcode;
using UnityEngine;

public class VRPlayerSetup : NetworkBehaviour
{
    public Camera vrCamera;
    public AudioListener audioListener;

    public override void OnNetworkSpawn()
    {
        // Если это не наш персонаж (а копия другого игрока по сети)
        if (!IsOwner)
        {
            // Отключаем его камеру и слушатель звука, чтобы они не конфликтовали с нашими
            if (vrCamera != null) vrCamera.enabled = false;
            if (audioListener != null) audioListener.enabled = false;
        }
    }
}