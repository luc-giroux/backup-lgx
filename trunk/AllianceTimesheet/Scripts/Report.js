/*
* Function to display a waiter while the report is loading
*/
function WaitReportLoading() {
    document.getElementById("PanelReport").style.backgroundImage = 'url(../../img/WaitReport.gif)';
    document.getElementById("PanelReport").style.backgroundRepeat = 'no-repeat';
    document.getElementById("PanelReport").style.backgroundPosition = 'center';
    document.getElementById("ButtonViewReport").disabled = true;
    // If a previous report was loader, We hide it
    if (document.getElementById("ReportViewer1_ReportViewer") != null) {
        document.getElementById("ReportViewer1_ReportViewer").style.display = "none";
    }
    
}