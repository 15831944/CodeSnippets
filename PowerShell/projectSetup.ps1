clear
cd C:\Users\davidvesely\Documents
. .\hosts.ps1
. .\iis.ps1

$appPoolName = "InventoryWeb"
$authHttpPort = 8081
$setHttpPort = 8080
$projectPath = "C:\Projects\Fourth.Inventory.Project"
$hostfile = "C:\Windows\System32\drivers\etc\hosts"
$majorWindowsVer = [System.Environment]::OSVersion.Version.Major

# Register IIS
# Windows 7
if ($majorWindowsVer -eq 6) {
    echo "Register IIS"
    C:\Windows\Microsoft.NET\Framework64\v4.0.30319\aspnet_regiis.exe -i
}
# Windows 10
elseif ($majorWindowsVer -eq 10) {
    echo "Install IIS features"
    #dism /online /enable-feature /featurename:IIS-ASPNET45 /all
    #dism /online /enable-feature /featurename:WCF-HTTP-Activation45 /all
}

echo "Add entries to hostfile"
add-host $hostfile "127.0.0.1" "auth.r9.local"
add-host $hostfile "127.0.0.1" "set01.r9.local"
# DEV, QA and QAI databases
add-host $hostfile "10.11.10.50"     "4TH-DEVSQL04"
add-host $hostfile "191.239.208.219" "R9DBQA"
add-host $hostfile "52.164.120.227"  "IE1R9QAIDBFS01"
add-host $hostfile "52.169.78.102"   "IE1R9QDB01"

# Create IIS sites
echo "Create Authentication IIS Site"
$authPath = Join-Path $projectPath "Common\Fourth.GHS.Web.Authentication\Authentication"
create-iis-site $appPoolName "auth.r9.local" $authPath $authHttpPort
echo "Create Set IIS Site and Application"
$setPath = Join-Path $projectPath "Common\Fourth.GHS.Web.Set\Set"
create-iis-site $appPoolName "set01.r9.local" "C:\inetpub\wwwroot" $setHttpPort
create-iis-app $appPoolName "set01.r9.local" "SetApp" $setPath
