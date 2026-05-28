using Unity.Netcode;
using UnityEngine;

// 1. Формируем пользовательский PTS пакет
public struct PTS_Packet : INetworkSerializable
{
    public ulong senderId;           // ID отправителя
    public Vector3 interactionPos;   // Координаты взаимодействия
    public int actionCode;           // Код действия (например, 1 - взял предмет, 2 - бросил)

    // Встроенное решение Netcode для сериализации нашего пакета
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref senderId);
        serializer.SerializeValue(ref interactionPos);
        serializer.SerializeValue(ref actionCode);
    }
}

// 2. Обработчик пакетов (вешается на сетевой объект)
public class NetworkPacketProcessor : NetworkBehaviour
{
    public void SendCustomAction(int code)
    {
        if (IsOwner) // Если это наш локальный игрок
        {
            PTS_Packet packet = new PTS_Packet
            {
                senderId = NetworkManager.Singleton.LocalClientId,
                interactionPos = transform.position,
                actionCode = code
            };
            
            // Отправляем пакет на сервер
            ProcessPTSPacketServerRpc(packet);
        }
    }

    // Сервер получает пакет и рассылает всем клиентам
    [ServerRpc]
    private void ProcessPTSPacketServerRpc(PTS_Packet packet)
    {
        Debug.Log($"[Сервер] Получен PTS от клиента {packet.senderId}");
        DistributePTSPacketClientRpc(packet);
    }

    // Клиенты получают обработанный пакет
    [ClientRpc]
    private void DistributePTSPacketClientRpc(PTS_Packet packet)
    {
        Debug.Log($"[Клиент] Игрок {packet.senderId} выполнил действие {packet.actionCode} в {packet.interactionPos}");
    }
}