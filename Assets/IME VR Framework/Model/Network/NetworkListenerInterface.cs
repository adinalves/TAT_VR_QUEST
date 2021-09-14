public interface NetworkListenerInterface
{
    void onReceiveMessage(string senderIP, string message);
    void onReceiveAck(string receiverIP, string message);
    void onReceiveNack(string receiverIP, string message);
}