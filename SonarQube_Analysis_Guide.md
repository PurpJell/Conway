# SonarQube Analysis Setup Guide for Unity Projects

## Prerequisites
- Docker installed and running
- .NET SDK installed
- Java JDK (version 17 or higher)
- PowerShell

## Step 1: Start SonarQube Server (Docker)
```powershell
# Start your existing SonarQube Docker container
# (Replace with your specific Docker command)
docker start <your-sonarqube-container-name>

# Or run a new container if needed:
# docker run -d --name sonarqube -p 9000:9000 sonarqube:community
```

## Step 2: Verify SonarQube is Running
```powershell
# Test if SonarQube is accessible
curl http://localhost:9000
```

### Step 3: Download SonarScanner for MSBuild
```powershell
# Navigate to your project directory
Set-Location "E:\documents\Conway"

# Download the latest SonarScanner for MSBuild
Invoke-WebRequest -Uri "https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/6.2.0.85879/sonar-scanner-6.2.0.85879-net.zip" -OutFile "sonar-scanner-msbuild.zip"

# Extract the scanner
Expand-Archive -Path "sonar-scanner-msbuild.zip" -DestinationPath "sonar-scanner-msbuild" -Force
```

## Step 4: Run SonarQube Analysis (3-Step Process)

### Step 4a: Begin Analysis
```powershell
dotnet "sonar-scanner-msbuild\SonarScanner.MSBuild.dll" begin /k:"Conway" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_cecd295eff23d40c0401a5aa9bf1846f02ecc831"
```

### Step 4b: Build the Solution
```powershell
dotnet build Conway.sln
```

### Step 4c: End Analysis and Upload Results
```powershell
dotnet "sonar-scanner-msbuild\SonarScanner.MSBuild.dll" end /d:sonar.token="sqp_cecd295eff23d40c0401a5aa9bf1846f02ecc831"
```

## Step 5: View Results
Navigate to: http://localhost:9000/dashboard?id=Conway

---

## Important Notes

### Authentication Token
- Replace `sqp_cecd295eff23d40c0401a5aa9bf1846f02ecc831` with your actual SonarQube token
- To generate a new token: SonarQube → Administration → Security → Users → Generate Token

### Project Structure Requirements
- Must have Unity-generated `.csproj` files (Assembly-CSharp.csproj, etc.)
- Must have `.sln` solution file
- Run from project root directory containing these files

### What This Analysis Provides
- ✅ Complexity Metrics (Cognitive & Cyclomatic)
- ✅ Code Smells and Technical Debt
- ✅ Security Vulnerabilities
- ✅ Duplicated Code Detection
- ✅ Maintainability Index
- ✅ Full C# Rule Analysis (70+ rules)

### Troubleshooting

#### If SonarQube Server Not Accessible
```powershell
# Check if container is running
docker ps

# Start container if stopped
docker start <container-name>
```

#### If Build Fails
- Ensure .NET SDK is installed: `dotnet --version`
- Check that Conway.sln exists in current directory
- Unity must have generated the project files

#### If Analysis Fails
- Remove any `sonar-project.properties` files (MSBuild scanner doesn't use them)
- Ensure token is valid and has analysis permissions
- Check SonarQube server logs for errors

## Quick Reference Commands

### Full Analysis (Copy-Paste Ready)
```powershell
# Navigate to project
Set-Location "E:\documents\Conway"

# Download scanner (only needed once)
Invoke-WebRequest -Uri "https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/6.2.0.85879/sonar-scanner-6.2.0.85879-net.zip" -OutFile "sonar-scanner-msbuild.zip"
Expand-Archive -Path "sonar-scanner-msbuild.zip" -DestinationPath "sonar-scanner-msbuild" -Force

# Run analysis
dotnet "sonar-scanner-msbuild\SonarScanner.MSBuild.dll" begin /k:"Conway" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_cecd295eff23d40c0401a5aa9bf1846f02ecc831"
dotnet build Conway.sln
dotnet "sonar-scanner-msbuild\SonarScanner.MSBuild.dll" end /d:sonar.token="sqp_cecd295eff23d40c0401a5aa9bf1846f02ecc831"
```

### Results URL
http://localhost:9000/dashboard?id=Conway