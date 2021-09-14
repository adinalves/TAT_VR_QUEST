using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProtocolMessage {

    //ATRIBUTOS
    public string messageID;
    public string messageTypeName;

    //CONSTRUTOR
    public ProtocolMessage()
    {
        messageID = Guid.NewGuid().ToString();
        messageTypeName = this.GetType().Name;
    }

    //MÉTODO DE ENVIO DA MENSAGEM DE PROTOCOLO
    public bool send()
    {
        //string instructorIP = DAO.getInstance().data.getInstructorIP();
        string instructorIP = "";
        if (instructorIP != null && instructorIP.Length != 0)
        {
            NetworkManager.sendMessage(instructorIP, JSONParser.toJSON(this));
            return true;
        }
        return false;
    }
}