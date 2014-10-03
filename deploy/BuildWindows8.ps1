param(
        [string] $ProjectName = "myProductsApp",
        [string] $ProjectFolder = "..\src\myProducts.Web",
        [string] $AppName = "com.tt.myp",
        [string] $BuildFolder = "out/windows8",
        [string] $IncludeWinJS = "false",
        [string] $Url = "http://localhost/ngmd/app"
    )

[string] $RootFolder = Get-Location
[string] $TemplateFolder = "CordovaTemplate"

# Build-Verzeichnis löschen
Write-Host "--Delete old build directory."
Remove-Item .\$BuildFolder -Force -Recurse

# Anlegen des Build-Verzeichnisses
Write-Host "--Create new build directory."
New-Item -ItemType Directory -Force -Path .\$BuildFolder

# Cordova-Template-Ordner löschen
Write-Host "--Delete old Cordova folder."
Remove-Item .\$TemplateFolder -Force -Recurse -ErrorAction SilentlyContinue

# Cordova-Template anlegen
Write-Host "--Create Cordova project."
cordova create $TemplateFolder $AppName $ProjectName

cd $TemplateFolder

# Windows 8 als Plattform hinzufügen
Write-Host "--Add Windows 8 platform"
cordova platform add windows

# Wieder ins Root-Verzeichnis des Skriptes springen
cd $RootFolder

# Kopieren des Plattform-Verzeichnisses in den Build-Ordner
Write-Host "--Copy Project template to build directory"
Copy-Item -Path .\$TemplateFolder\platforms\windows\* -Filter *.* -Destination .\$BuildFolder -Recurse -Force -ErrorAction SilentlyContinue

# Kopieren der Projektdaten in das Build-Verzeichnis
Write-Host "--Copy app files"
Copy-Item -Path .\$ProjectFolder\app -Filter *.* -Destination .\$BuildFolder\www -Recurse -Force -ErrorAction SilentlyContinue 
Copy-Item -Path .\$ProjectFolder\assets -Filter *.* -Destination .\$BuildFolder\www -Recurse -Force -ErrorAction SilentlyContinue 
Copy-Item -Path .\$ProjectFolder\libs -Filter *.* -Destination .\$BuildFolder\www -Recurse -Force -ErrorAction SilentlyContinue 
Copy-Item -Path .\$ProjectFolder\appServices -Filter *.* -Destination .\$BuildFolder\www -Recurse -Force -ErrorAction SilentlyContinue 

# Index.html vom Server laden
$URI = $Url -as [System.URI] 
Write-Host "--Get index.html from server."
curl -Uri $Url -OutFile _index.html

# Verschieben der index.html
Write-Host "--Move index.html" 
Move-Item -Path _index.html -Destination .\$BuildFolder\www\index.html -Force

# Wechsel ins Windows 8 Verzeichnis
cd $BuildFolder

# Modifiziere kritsiche AngularJS-Stellen
Write-Host "--Update AngularJS"
$JQueryFileSearchResult =  Get-ChildItem -Path www -Filter *jquery*.js -Recurse
(Get-Content -Path $JQueryFileSearchResult.FullName) | 
ForEach-Object {
$_ -creplace '(?:\s|;)?(.*?\.innerHTML\s?=\s?.*?;)', 'MSApp.execUnsafeLocalFunction(function () {$1});' } | 
ForEach-Object {
$_ -creplace '(?:\s|;)?(.*?\.outerHTML\s?=\s?.*?;)', 'MSApp.execUnsafeLocalFunction(function () {$1});' } | 
ForEach-Object {
$_ -creplace '(document\.write\(.*?\))', 'MSApp.execUnsafeLocalFunction(function () {$1});'} |
Set-Content $JQueryFileSearchResult.FullName

# Einfügen der WinJS-Referencen in die index.html
Write-Host "--Update index.html"
$IndexFileSearch = Get-ChildItem -Path www -Filter index.html
$TempFile = $IndexFileSearch.FullName + "_temp"

Get-Content -Path $IndexFileSearch.FullName | ForEach-Object {
    $PathToReplace = ""
    
    if($IncludeWinJS.Equals("true")) {
        if($_ -cmatch "</head>") {
            $WinJSReferences = '<script type="text/javascript" src="//Microsoft.WinJS.2.0/js/base.js"></script>';
            $WinJSReferences +='<script type="text/javascript" src="//Microsoft.WinJS.2.0/js/ui.js"></script>';
            $WinJSReferences +='<link rel="stylesheet" href="//Microsoft.WinJS.2.0/css/ui-dark.css"/>';
            $WinJSReferences += "</head>`r`n";
            $_ -creplace "</head>", $WinJSReferences
        }
    }    

    for ($i=0; $i -lt $URI.Segments.length - 1; $i++) {
	    $PathToReplace = $PathToReplace + $URI.Segments[$i]
    }

    $_ -replace $PathToReplace, ""
} | 
Set-Content $TempFile -Force

Remove-Item $IndexFileSearch.FullName
Move-Item -Path $TempFile -Destination $IndexFileSearch.FullName

cd $RootFolder
Remove-Item .\$TemplateFolder -Force -Recurse -ErrorAction SilentlyContinue

Write-Host "--Done!"
