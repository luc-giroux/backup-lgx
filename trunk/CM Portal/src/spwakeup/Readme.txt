To install,do this:

* Add "C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\BIN" in the PATH environment variable
* Allow powershell to lauchn script. to do this, run "Set-ExecutionPolicy Unrestricted" in powershell
* Copy the folder with the 2 files on the MOSS server.
* Create a daily task with the windows task scheduler to run the "spwakeup.bat" 10 min after the recycle of your application pool
* Thats All