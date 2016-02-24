using System;
using System.Collections.Generic;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace EscribirCola
{
    public class ManejadorCola
    {
        private static ManejadorCola _instance;
        public static ManejadorCola Instance
        {
            get
            {
                if(_instance==null)
                    _instance=new ManejadorCola();
                return _instance;
            }

        }

        private ManejadorCola()
        {
            
        }
        public bool CrearCola(String conn, String nombre, int tamano, int tiempo)
        {
            var c = NamespaceManager.CreateFromConnectionString(conn);
            if (c.QueueExists(nombre))
            {
                return false;
            }
            
            var cola=new QueueDescription(nombre);
            cola.MaxSizeInMegabytes = tamano;
            cola.DefaultMessageTimeToLive=new TimeSpan(0,0,tiempo);
            try
            {
                c.CreateQueue(cola);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return false;

        }

        public void Enviar(String conn, String nombre, Dictionary<string, string> parametros, String texto)
        {
            var cl = QueueClient.CreateFromConnectionString(conn,nombre);

            var msg=new BrokeredMessage(texto);
            foreach (var key in parametros.Keys)
            {
                msg.Properties[key] = parametros[key];
            }

            cl.Send(msg);

        }
    }
}