using Unity.Netcode.Components;
using UnityEngine;

[DisallowMultipleComponent]
public class ClientNetworkTransform : NetworkTransform
{
    // Переопределяем встроенное решение: даем клиенту право самому диктовать 
    // свои координаты серверу. Это убирает лаги и "дерганья" VR-шлема.
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}