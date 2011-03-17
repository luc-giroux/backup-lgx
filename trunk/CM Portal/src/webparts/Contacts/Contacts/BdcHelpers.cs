using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Office.Server.ApplicationRegistry.MetadataModel;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;
using Microsoft.Office.Server.ApplicationRegistry.SystemSpecific;
using Microsoft.Office.Server.ApplicationRegistry.Infrastructure;

namespace Contacts
{
    public class BdcHelpers
    {
        public static IEntityInstance GetSpecificRecord(string lobSystemInstance, string entityName, object []identifiers)
        {
            NamedLobSystemInstanceDictionary sysInstances = ApplicationRegistry.GetLobSystemInstances();
            LobSystemInstance instance = sysInstances[lobSystemInstance];
            Entity entity = instance.GetEntities()[entityName];
            
            IEntityInstance ie = entity.FindSpecific(identifiers, instance);

            return ie;
        }

        public static void ExecuteGenericInvoker(string lobSystemInstance, string entityName, string methodInstance, object[] parameters)
        {
            NamedLobSystemInstanceDictionary instances = ApplicationRegistry.GetLobSystemInstances();
            LobSystemInstance instance = instances[lobSystemInstance];
            Entity entity = instance.GetEntities()[entityName];

            MethodInstance methInst = entity.GetMethodInstances()[methodInstance];

            entity.Execute(methInst, instance, ref parameters);
        }
    }
}
