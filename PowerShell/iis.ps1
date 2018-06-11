Import-Module WebAdministration

function create-iis-site(
    [String]$iisAppPoolName,
    [String]$iisSiteName,
    [String]$directoryPath,
    [int]$httpPort) {
    $iisAppPoolDotNetVersion = "v4.0"
    #navigate to the app pools root
    cd IIS:\AppPools\

    #check if the app pool exists
    if (!(Test-Path $iisAppPoolName -pathType container))
    {
        #create the app pool
        $appPool = New-Item $iisAppPoolName
        $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
    }

    #navigate to the sites root
    cd IIS:\Sites\

    #check if the site exists
    if (Test-Path $iisSiteName -pathType container)
    {
        return
    }

    #create the site
    $iisApp = New-Item $iisSiteName -bindings @{protocol="http";bindingInformation=":" + $httpPort + ":" + $iisSiteName} -physicalPath $directoryPath
    $iisApp | Set-ItemProperty -Name "applicationPool" -Value $iisAppPoolName
}

function create-iis-app(
    [String]$iisAppPoolName,
    [String]$iisSiteName,
    [String]$iisAppName,
    [String]$directoryPath) {
    # Check if site exists
    cd IIS:\Sites\
    if (-not (Test-Path $iisSiteName -pathType container)) {
        throw "Create the IIS site '$iisSiteName' first!"
    }

    # Check if web app exists
    cd (Join-Path "IIS:\Sites" $iisSiteName)
    if (Test-Path $iisAppName -pathType container) {
        return
    }

    $iisApp = New-Item $iisAppName -physicalPath $directoryPath -type Application
    $iisWebAppPath = Join-Path "IIS:\Sites" (Join-Path $iisSiteName $iisAppName)
    Set-ItemProperty $iisWebAppPath -Name "applicationPool" -Value $iisAppPoolName
}
