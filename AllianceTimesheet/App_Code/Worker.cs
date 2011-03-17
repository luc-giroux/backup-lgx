using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Worker
/// </summary>
[Serializable]
public class Worker
{

    private int WorkerID;
    private int WorkerBadgeNumber;
    private string WorkerLastName;
    private string WorkerFirstName;
    private string WorkerTrade;
    private string WorkerPositionCategory;
    private bool WorkerActive;

	public Worker()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ID
    {
        get { return WorkerID; }
        set { WorkerID = value; }
    }

    public int BadgeNumber
    {
        get { return WorkerBadgeNumber; }
        set { WorkerBadgeNumber = value; }
    }

    public string LastName
    {
        get { return WorkerLastName; }
        set { WorkerLastName = value; }
    }

    public string FirstName
    {
        get { return WorkerFirstName; }
        set { WorkerFirstName = value; }
    }

    public string Trade
    {
        get { return WorkerTrade; }
        set { WorkerTrade = value; }
    }

    public string PositionCategory
    {
        get { return WorkerPositionCategory; }
        set { WorkerPositionCategory = value; }
    }

    public bool Active
    {
        get { return WorkerActive; }
        set { WorkerActive = value; }
    }
    
}