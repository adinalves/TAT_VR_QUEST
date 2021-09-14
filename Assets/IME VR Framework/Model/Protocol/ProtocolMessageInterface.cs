public interface ProtocolMessageInterface {
    void onReceiveMessage(string senderIP);
    void onReceiveAck(string receiverIP);
    void onReceiveNack(string receiverIP);
}