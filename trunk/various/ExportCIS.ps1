# notes:
#
# Set-ExecutionPolicy Unrestricted # to allow the .ps1 to run without signature
#
# constants
$extractFolder = "C:\Users\caro2009\Desktop\VAVDB1\ExportCIS\SQLCMD\"
$extractFile = "CIS.txt"
$extractFile = $extractFolder + $extractFile
$extractCmd = $extractFolder + "cis.bat"

$smtpServer = "VAVMAIL1.projetkoniambo.local";
$emailFrom = "NCIT@projetkoniambo.com"
$emailTo = "jcaron@hatch.ca; jeancaron@hatch.com.au; jcaron@projetkoniambo.com"
$emailSubject = "CIS Export"
$emailBody = ""

# extracting info from db
set-Location -Path $extractFolder
try
{
    # Invoke-Item (or &...) does not take into account the set-Location command - need to use the Start-Process
    Start-Process $extractCmd -wait
}
catch
{
    "Error running extranction command:"
    $extractCmd
    "exiting"
    exit 1
}
# sending mail

Try
{
    $msg = new-object Net.Mail.MailMessage
    $att = new-object Net.Mail.Attachment($extractFile)
    $smtp = new-object Net.Mail.SmtpClient($smtpServer)
 
    $msg.From = $emailFrom
    foreach ($to in $emailTo -split ";")
    {
        $to
        $msg.To.Add($to)
    }
    $msg.Subject = $emailSubject
    $msg.Body = $emailBody
    $msg.Attachments.Add($att)
    $smtp.Send($msg)
    $att.Dispose()
}
catch
{
    "Error sending email!"
}