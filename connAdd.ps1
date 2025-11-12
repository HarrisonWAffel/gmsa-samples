# connAdd.ps1 updates the web.config of an IIS website with a specific connection string
param (
    [Parameter()]
    [String]
    $webConfigPath = "C:\inetpub\wwwroot\YourWebApp\web.config",

    [Parameter()]
    [String]
    $connName = "sqlTest",

    [Parameter()]
    [String]
    $connValue = "value",

    [Parameter()]
    [String]
    $provider = "System.Data.SqlClient"
)


if (-not (Test-Path $webConfigPath)) { Throw "Error: web.config '$webConfigPath' not found." }
[xml]$wc = Get-Content $webConfigPath -ErrorAction Stop


$connStrings = $wc.configuration.connectionStrings
if (-not $connStrings) {
    $connStrings = $wc.configuration.AppendChild($wc.CreateElement("connectionStrings"))
}

$connAdd = $connStrings.add | Where-Object { $_.name -eq $connName }
if (-not $connAdd) {
    $connAdd = $wc.CreateElement("add")
    $connAdd.SetAttribute("name", $connName)
    $connStrings.AppendChild($connAdd)
}

$connAdd.SetAttribute("connectionString", $connValue)
if ($provider) { # Only set if provider name is provided
    $connAdd.SetAttribute("providerName", $provider)
}

$wc.Save($webConfigPath)
Write-Host "Successfully updated connection string '$connName' in '$webConfigPath'."
