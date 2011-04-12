using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataSetReportsTableAdapters;

/// <summary>
/// LGX: class created to extend the ReportProgressCWPTableAdapter class to add a 
/// method to set the timeout of the query because by default for a tableadapter 
/// the timeout is set to 30 seconds. 
/// </summary>
public partial class ReportProgressCWPTableAdapterExtended : ReportProgressCWPTableAdapter //: global::System.ComponentModel.Component
{


        public int SelectCommandTimeout
        {
            get
            {
                return this.CommandCollection[0].CommandTimeout;
            }
            set
            {
                this.CommandCollection[0].CommandTimeout = value;
            }
        }
    
}