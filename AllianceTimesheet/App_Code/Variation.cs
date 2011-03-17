using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Variation
/// </summary>

[Serializable]
public class Variation 
{
    private string VariationNumber;
    private string VariationDiscipline;
    private string VariationDescription;
    private bool VariationCompleted;

	public Variation()
	{
	}

    public string Number
    {
        get { return VariationNumber; }
        set { VariationNumber = value; }
    }

    public string Discipline
    {
        get { return VariationDiscipline; }
        set { VariationDiscipline = value; }
    }

    public string Description
    {
        get { return VariationDescription; }
        set { VariationDescription = value; }
    }

    public bool Completed
    {
        get { return VariationCompleted; }
        set { VariationCompleted = value; }
    }
}