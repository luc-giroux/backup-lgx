"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o retractsolution -name PAFMaintenance.wsp -url https://cm.projetkoniambo.com -immediate

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o execadmsvcjobs

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o deletesolution -name PAFMaintenance.wsp -override

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o execadmsvcjobs

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o addsolution -filename PAFMaintenance.wsp

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o deploysolution -name PAFMaintenance.wsp -url https://cm.projetkoniambo.com -immediate -allowGacDeployment

pause