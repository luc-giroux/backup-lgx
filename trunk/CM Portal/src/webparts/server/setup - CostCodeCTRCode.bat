"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o retractsolution -name CostCodeCTRCode.wsp -url https://cm.projetkoniambo.com -immediate

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o execadmsvcjobs

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o deletesolution -name CostCodeCTRCode.wsp -override

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o execadmsvcjobs

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o addsolution -filename CostCodeCTRCode.wsp

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o deploysolution -name CostCodeCTRCode.wsp -url https://cm.projetkoniambo.com -immediate -allowGacDeployment

"C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN\stsadm.exe" -o execadmsvcjobs

pause