using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Objeto que armazena todos os atributos da aplicação que necessitam de persistência
 * (deve ser SERIALIZÁVEL pois utiliza a serialização JSON)
 */ 
[Serializable]
public class Data {

    //////////////////////////////////////// TO DO //////////////////////////////////////////
    /*
      * ATRIBUTOS (Insira seus atributos abaixo deste comentário)
      * 
      * Devem ser definidos como públicos para a serialização JSON. Veja os exemplos abaixo:
      * public string aString;
      * public int anInt;
      * public float aFloat;
      * public OtherClass otherClass; //A classe deve estar definida como [Serializable]
      * public List<...> aList;
      */
    public string name;
    public int acertos;
    public int mao; 

    ////////////////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////// TO DO //////////////////////////////////////////
    /*
     * GETTERS e SETTERS (Insira seus seletores e modificadores de atributos abaixo deste comentário)
     * 
     * Os modificadores devem sempre chamar o método save() da classe DAO. Veja o exemplo abaixo:
     * public string getAString() {
     *      return aString;
     * }
     * public void setAString(string aString) {
     *      this.aString = aString;
     *      DAO.getInstance().save(); //Persistindo a modificação
     * }
     */
    
    /*public string getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
        DAO.getInstance().save(); //Persistindo a modificação
    }

    public void setAcerto(int acertos)
    {
        this.acertos = acertos;
        DAO.getInstance().save(); //Persistindo a modificação
    }

    public int getAcerto()
    {
        return acertos;
    }
*/

    //////////////////////////////////////////////////////////////////////////////////////////
}

public class DataRaiz
{
    public Data[] jogadores;
}


