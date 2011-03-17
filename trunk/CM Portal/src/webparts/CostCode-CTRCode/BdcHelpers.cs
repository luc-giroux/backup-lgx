using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Office.Server.ApplicationRegistry.MetadataModel;
using Microsoft.Office.Server.ApplicationRegistry.Runtime;
using Microsoft.Office.Server.ApplicationRegistry.SystemSpecific;
using Microsoft.Office.Server.ApplicationRegistry.Infrastructure;
using Microsoft.Office.Server.ApplicationRegistry.SystemSpecific.Db;

namespace CostCodeCTRCode
{
    public class BdcHelpers
    {
        public static IEntityInstance GetSpecificRecord(string lobSystemInstance, string entityName, object[] identifiers)
        {
            NamedLobSystemInstanceDictionary sysInstances = ApplicationRegistry.GetLobSystemInstances();
            LobSystemInstance instance = sysInstances[lobSystemInstance];
            Entity entity = instance.GetEntities()[entityName];

            IEntityInstance ie = entity.FindSpecific(identifiers, instance);

            return ie;
        }

        // récupère toutes les valeurs d'une table donnée
        // renvoie un Dataset
        public static DataSet GetAllRecords(string lobSystemInstance, string entityName)
        {
            DataSet ds = new DataSet(entityName);

            NamedLobSystemInstanceDictionary sysInstances = ApplicationRegistry.GetLobSystemInstances();
            LobSystemInstance instance = sysInstances[lobSystemInstance];
            Entity entity = instance.GetEntities()[entityName];

            // Get the finder method
            MethodInstance finder = entity.GetFinderMethodInstance();

            //Execute Finder method
            DbEntityInstanceEnumerator records = (DbEntityInstanceEnumerator)entity.Execute(finder, instance);

            //Load matching entities into a DataTable
            DataTable entitiesTable = new DataTable();
            while (records.MoveNext())
            {
                //Load each entity and include the Action URL
                DbEntityInstance record = (DbEntityInstance)records.Current;
                DataTable entityTable = record.EntityAsDataTable;
                entityTable.AcceptChanges();
                entitiesTable.Merge(entityTable);
            }

            //DataTable ordersTable = customerOrders.Tables.Add(“Orders”);
            ds.Tables.Add(entitiesTable);
            return ds;
        }

        // Permet d'éxecuter une méthode select donné avec des paramètres
        // renvoie un Dataset
        public static DataSet GetAllRecordsWithParam(string lobSystemInstance, string entityName, string methodInstance, object[] parameters)
        {
            DataSet ds = new DataSet(entityName);

            NamedLobSystemInstanceDictionary sysInstances = ApplicationRegistry.GetLobSystemInstances();
            LobSystemInstance instance = sysInstances[lobSystemInstance];
            Entity entity = instance.GetEntities()[entityName];

            // Get the method
            MethodInstance methInst = entity.GetMethodInstances()[methodInstance];

            //Execute Finder method
            DbEntityInstanceEnumerator records = (DbEntityInstanceEnumerator)entity.Execute(methInst, instance, ref parameters);

            //Load matching entities into a DataTable
            DataTable entitiesTable = new DataTable();
            while (records.MoveNext())
            {
                //Load each entity and include the Action URL
                DbEntityInstance record = (DbEntityInstance)records.Current;
                DataTable entityTable = record.EntityAsDataTable;
                entityTable.AcceptChanges();
                entitiesTable.Merge(entityTable);
            }

            ds.Tables.Add(entitiesTable);
            return ds;
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
