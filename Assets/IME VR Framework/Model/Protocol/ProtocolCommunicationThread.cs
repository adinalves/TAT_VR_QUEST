using System;

public class ProtocolCommunicationThread {

    //ENUM DE DEFINIÇÃO DOS TIPOS DE GATILHO
    public enum TriggerType
    {
        MESSAGE, ACK, NACK,
    }

    //ATRIBUTOS
    private string ip;
    private string message;
    private TriggerType triggerType;

    //CONSTRUTOR
    public ProtocolCommunicationThread(string ip, string message, TriggerType triggerType)
    {
        this.ip = ip;
        this.message = message;
        this.triggerType = triggerType;
    }

    public void Start()
    {
        UIThreadDispatcher.RunOnMainThread(new Action(ProtocolCommunicationAction));
    }

    public void ProtocolCommunicationAction()
    {
        //Ler o id e o tipo da mensagem
        ProtocolMessage protocolMessage = new ProtocolMessage();
        JSONParser.fromJSON(message, protocolMessage);
        string messageID = protocolMessage.messageID;
        string messageTypeName = protocolMessage.messageTypeName;

        //Instanciar o objeto relacionado à mensagem
        Type clazz = Type.GetType(messageTypeName);
        object instancedObject = Activator.CreateInstance(clazz);

        switch(triggerType)
        {
            case TriggerType.MESSAGE:
                //Atualizar o IP do instrutor
                //DAO.getInstance().data.setInstructorIP(ip);
                //Tratar o recebimento da mensagem
                ((ProtocolMessageInterface)instancedObject).onReceiveMessage(ip);
                break;
            case TriggerType.ACK:
                //Tratar o recebimento do ACK
                ((ProtocolMessageInterface)instancedObject).onReceiveAck(ip);
                break;
            case TriggerType.NACK:
                //Tratar o recebimento do NACK
                ((ProtocolMessageInterface)instancedObject).onReceiveNack(ip);
                break;
            default:
                break;
        }
    }
}