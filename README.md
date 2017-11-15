# DesktopAppConverter.Example 
This is an example to demonstrate how to use the **DesktopAppConverter** tool to simply convert existing WPF and Win32 (Windows Forms, VB) application to UWP application (.appx) and install them locally. 
  
## Prerequisite 
1. Computer running Windows 10 OS 
2. Windows PowerShell 
  
## Procedure 
#### Step 1 
Install [DesktopAppConverter](https://www.microsoft.com/en-us/store/p/desktopappconverter/9nblggh4skzw) App from your Windows Store application 
  
#### Step 2 
Check your Windows 10 OS version by **Run** the command `winver`. For example it will have like **Version 1670 (OS Build 14393.1770)** 
  
#### Step 3 
Based on your operating system build version number please download the [DesktopAppConverter Base Image](https://www.microsoft.com/en-us/download/details.aspx?id=56049) image. 
  
#### Step 4 
Open **Windows PowerShell** app as Administrator (x64 bit recommended. It will be the default), then type and enter the following command 
`Set-ExecutionPolicy bypass` 
It will ask for your option, then press **Y** to accept the change. 
  
#### Step 5 
Now install the downloaded base image in our system by running the following command in powershell.  
  
`DesktopAppConverter.exe -Setup -BaseImage .\BaseImage-1XXXX.wim -Verbose` 
  
> Note: It will ask for restart to complete the installation and it will take some time to complete the process. Make sure your C: drive having enough space. 
  
Once everything completed we can start the app conversion process. 
  
#### Step 6 
Now clone this project in C: drive and build the project in Release configuration. Once done you can see the **Weather.WinForm.exe** and its dependencies in the `C:\DesktopAppConverter.Example\Weather.WinForm\bin\Release` folder. 
  
#### Step 7 
Now open the PowerShell app as Administrator and execute the following command 
  
`DesktopAppConverter.exe -Installer C:\DesktopAppConverter.Example\Weather.WinForm\bin\Release\ -AppExecutable Weather.WinForm.exe -Destination C:\DesktopAppConverter.Example -PackageName "WeatherApp" -Publisher "CN=iLink" -Version 0.0.0.1 -Verbose -Makeappx` 
  
It will run set of commands and finally generate the Appx file `C:\DesktopAppConverter.Example\WeatherApp\WeatherApp.appx` 
  
#### Step 8 
Now we need to create self signing certificate to sing the generate appx file, then only we can install the app to debug. 
Create the certificate by executing the following command in power shell. 
  
`New-SelfSignedCertificate -Type Custom -Subject "CN=iLink" -KeyUsage DigitalSignature -FriendlyName "iLink Systems" -CertStoreLocation "Cert:\LocalMachine\My" 
` 
It will show the output with Thumbprint code. Take a copy of the code. 
  
#### Step 9 
Now we need to create Personal Information Exchange (PFX) file to export the generated sigining certificate. Later it will be installed in local machine. Create the pfx file by executing the following command in power shell 
  
`$pwd = ConvertTo-SecureString -String Welcome123 -Force -AsPlainText` 
`Export-PfxCertificate -cert "Cert:\LocalMachine\My\<YOUR_THUMBRINT_CODE>" -FilePath C:\DesktopAppConverter.Example\Weather.pfx -Password $pwd` 
  
Once the command executed, you can see the **Weather.pfx** file in root of the project. Now right click the file and select the option to install. Select "Local machine" to install the certificate. 
  
#### Step 10 
Now we need to sign the application using **SignTool**. Sign the appx file with the generated **.pfx** file by following command  
  
`SignTool sign /fd sha256 /a /f C:\DesktopAppConverter.Example\Weather.pfx /p Welcome123 C:\DesktopAppConverter.Example\WeatherApp\WeatherApp.appx` 
  
It will sign the appx file. Once everything done we can install the WeatherApp.appx file in our windows 10 machine. 
 