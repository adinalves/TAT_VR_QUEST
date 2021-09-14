using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinkTestMessage : ProtocolMessage, ProtocolMessageInterface
{
    //CONTRUTOR
    public LinkTestMessage() : base() {}

    //IMPLEMENTAÇÃO DOS MÉTODOS DA INTERFACE ProtocolMessageInterface 
    public void onReceiveMessage(string senderIP)
    {
        //((MainSceneController) StaticReferences.sceneController).showMessage("Teste do link de comunicação recebido de " + DAO.getInstance().data.getInstructorIP() + ":" + Settings.LOCAL_PORT + " às " + DateTime.Now.ToString("HH:mm:ss"));
        Debug.Log("Recebido mensagem de LinkTestMessage");

        ////Teste de envio
        //new LinkTestMessage().send();
    }

    public void onReceiveAck(string receiverIP)
    {
        //((MainSceneController)StaticReferences.sceneController).showMessage("Teste do link de comunicação enviado para " + DAO.getInstance().data.getInstructorIP() + ":" + Settings.REMOTE_PORT + " às " + DateTime.Now.ToString("HH:mm:ss"));
        Debug.Log("Recebido ACK de LinkTestMessage");
    }

    public void onReceiveNack(string receiverIP)
    {
        //((MainSceneController)StaticReferences.sceneController).showMessage("Envio do teste do link de comunicação para " + DAO.getInstance().data.getInstructorIP() + ":" + Settings.REMOTE_PORT + " falhou às " + DateTime.Now.ToString("HH:mm:ss"));
        Debug.Log("Recebido NACK de LinkTestMessage");
    }
}