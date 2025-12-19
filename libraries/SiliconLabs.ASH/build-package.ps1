# PowerShell script to build and package the ASH Protocol library

param(
    [string]$Configuration = "Release",
    [string]$Version = "1.0.0",
    [string]$ProjectPath = "",  # Auto-detect if not provided
    [switch]$SkipTests,
    [switch]$Pack,
    [switch]$Push,
    [string]$ApiKey = "",
    [string]$Source = "https://api.nuget.org/v3/index.json"
)

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "ASH Protocol Library - Build & Package" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# Set error action preference
$ErrorActionPreference = "Stop"

# Get the script directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Auto-detect project file if not specified
if ([string]::IsNullOrWhiteSpace($ProjectPath)) {
    Write-Host "Auto-detecting project file..." -ForegroundColor Yellow
    
    # Look for .csproj file in script directory
    $projectFiles = Get-ChildItem -Path $scriptDir -Filter "*.csproj" -File
    
    if ($projectFiles.Count -eq 0) {
        Write-Host "Error: No .csproj file found in $scriptDir" -ForegroundColor Red
        Write-Host "Please specify project path with -ProjectPath parameter" -ForegroundColor Yellow
        exit 1
    }
    elseif ($projectFiles.Count -gt 1) {
        Write-Host "Error: Multiple .csproj files found:" -ForegroundColor Red
        $projectFiles | ForEach-Object { Write-Host "  - $($_.Name)" -ForegroundColor Yellow }
        Write-Host "Please specify which project with -ProjectPath parameter" -ForegroundColor Yellow
        exit 1
    }
    
    $ProjectPath = $projectFiles[0].FullName
    Write-Host "Found project: $($projectFiles[0].Name)" -ForegroundColor Green
}
else {
    # Resolve relative path
    if (-not [System.IO.Path]::IsPathRooted($ProjectPath)) {
        $ProjectPath = Join-Path $scriptDir $ProjectPath
    }
    
    if (-not (Test-Path $ProjectPath)) {
        Write-Host "Error: Project file not found: $ProjectPath" -ForegroundColor Red
        exit 1
    }
}

$projectDir = Split-Path -Parent $ProjectPath
$projectName = [System.IO.Path]::GetFileNameWithoutExtension($ProjectPath)

Write-Host "Project: $projectName" -ForegroundColor Cyan
Write-Host "Location: $projectDir" -ForegroundColor Cyan
Write-Host ""

# Change to project directory
Set-Location $projectDir

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
if (Test-Path "bin") { Remove-Item -Recurse -Force "bin" }
if (Test-Path "obj") { Remove-Item -Recurse -Force "obj" }
if (Test-Path "*.nupkg") { Remove-Item -Force "*.nupkg" }
if (Test-Path "*.snupkg") { Remove-Item -Force "*.snupkg" }

# Restore dependencies
Write-Host ""
Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore $ProjectPath

if ($LASTEXITCODE -ne 0) {
    Write-Host "Restore failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

# Build all target frameworks
Write-Host ""
Write-Host "Building for all target frameworks ($Configuration)..." -ForegroundColor Yellow
dotnet build $ProjectPath --configuration $Configuration --no-restore /p:Version=$Version

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

# Run tests (if not skipped)
if (-not $SkipTests) {
    Write-Host ""
    Write-Host "Running tests..." -ForegroundColor Yellow
    dotnet test $ProjectPath --configuration $Configuration --no-build --verbosity normal
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Tests failed!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
}

# Create NuGet packages
if ($Pack) {
    Write-Host ""
    Write-Host "Creating NuGet packages..." -ForegroundColor Yellow
    
    # Pack with symbols
    dotnet pack $ProjectPath --configuration $Configuration --no-build /p:Version=$Version `
        /p:IncludeSymbols=true /p:SymbolPackageFormat=snupkg
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Packaging failed!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    
    # List created packages
    Write-Host ""
    Write-Host "Created packages:" -ForegroundColor Green
    Get-ChildItem -Path "bin\$Configuration" -Filter "*.nupkg" -Recurse | ForEach-Object {
        Write-Host "  - $($_.Name)" -ForegroundColor Green
    }
}

# Push to NuGet (if requested)
if ($Push) {
    if (-not $Pack) {
        Write-Host "Error: Cannot push without packing. Use -Pack switch." -ForegroundColor Red
        exit 1
    }
    
    if ([string]::IsNullOrWhiteSpace($ApiKey)) {
        Write-Host "Error: API key required for pushing. Use -ApiKey parameter." -ForegroundColor Red
        exit 1
    }
    
    Write-Host ""
    Write-Host "Pushing packages to $Source..." -ForegroundColor Yellow
    
    Get-ChildItem -Path "bin\$Configuration" -Filter "*.nupkg" -Recurse -Exclude "*.symbols.nupkg" | ForEach-Object {
        Write-Host "Pushing $($_.Name)..." -ForegroundColor Cyan
        dotnet nuget push $_.FullName --api-key $ApiKey --source $Source --skip-duplicate
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Push failed for $($_.Name)!" -ForegroundColor Red
            exit $LASTEXITCODE
        }
    }
    
    # Push symbol packages
    Get-ChildItem -Path "bin\$Configuration" -Filter "*.snupkg" -Recurse | ForEach-Object {
        Write-Host "Pushing symbol package $($_.Name)..." -ForegroundColor Cyan
        dotnet nuget push $_.FullName --api-key $ApiKey --source $Source --skip-duplicate
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Warning: Symbol package push failed for $($_.Name)" -ForegroundColor Yellow
        }
    }
}

Write-Host ""
Write-Host "============================================" -ForegroundColor Green
Write-Host "Build completed successfully!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Green
Write-Host ""

# Display package information
if ($Pack) {
    # Extract package ID from project file
    [xml]$projectXml = Get-Content $ProjectPath
    $packageId = $projectXml.Project.PropertyGroup.PackageId
    if ([string]::IsNullOrWhiteSpace($packageId)) {
        $packageId = $projectName
    }
    
    Write-Host "Package Information:" -ForegroundColor Cyan
    Write-Host "  Package ID: $packageId" -ForegroundColor White
    Write-Host "  Version: $Version" -ForegroundColor White
    Write-Host "  Project: $projectName" -ForegroundColor White
    
    # Extract target frameworks
    $targetFrameworks = $projectXml.Project.PropertyGroup.TargetFrameworks
    if (-not [string]::IsNullOrWhiteSpace($targetFrameworks)) {
        Write-Host "  Targets: $targetFrameworks" -ForegroundColor White
    }
    
    Write-Host ""
    Write-Host "To install:" -ForegroundColor Cyan
    Write-Host "  dotnet add package $packageId --version $Version" -ForegroundColor Yellow
    Write-Host ""
}
