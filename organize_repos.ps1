# Create main category folders
$categories = @(
    "CSharp_Projects",
    "Web_Development",
    "Database_Scripts",
    "Testing_Examples",
    "Student_Management",
    "Zips_And_Backups",
    "Miscellaneous"
)
foreach ($cat in $categories) {
    if (-not (Test-Path $cat)) { New-Item -ItemType Directory -Name $cat }
}

# Move C# projects
$csProjects = @(
    "AgentApp", "CollectionsDay4", "ConsoleApp1", "Day2Practice", "Day2Practice - Copy",
    "Day3Sessions", "Day3Sessions - Copy", "Delegates", "Examples", "ExceptionFiltersDay4",
    "ExceptionhandlingDay3", "ExceptionhandlingDay3 - Copy", "ExtensionExample1", "ExtensionExample2",
    "ExtensionLibrary", "FileHandlingDay4", "LambdaExpressions", "LibraryManagementSystem",
    "MilestoneSolution", "MockRepeat", "Nullable", "PracticeDay1", "PracticeDay2", "PropertiesDay4",
    "Reflections", "SolidPrinciples", "SolidPrinciplesPrject", "SolidReportingSystem",
    "UnitTestingTask", "UserMngmentSystem"
)
foreach ($proj in $csProjects) {
    if (Test-Path $proj) { Move-Item $proj CSharp_Projects\ }
}

# Move Web Development folders
$webFolders = @("CSS", "Html", "Javascript", "Typescript")
foreach ($web in $webFolders) {
    if (Test-Path $web) { Move-Item $web Web_Development\ }
}

# Move Database scripts
$dbFolders = @("SQLSERVERSCRIPT", "MongoDbAssignment")
foreach ($db in $dbFolders) {
    if (Test-Path $db) { Move-Item $db Database_Scripts\ }
}

# Move Testing examples
if (Test-Path "Testing") { Move-Item "Testing" Testing_Examples\ }

# Move Student Management
$studentFolders = @("student-management", "student-management-sprint2.zip")
foreach ($sf in $studentFolders) {
    if (Test-Path $sf) { Move-Item $sf Student_Management\ }
}

# Move zip/backups
$zipFiles = @("LibraryManagementSystem.zip", "MongoDbAssignment.zip")
foreach ($zf in $zipFiles) {
    if (Test-Path $zf) { Move-Item $zf Zips_And_Backups\ }
}

# Move miscellaneous/uncategorized
$miscFolders = @("Project_25")
foreach ($mf in $miscFolders) {
    if (Test-Path $mf) { Move-Item $mf Miscellaneous\ }
}

Write-Host "Repos folder organized successfully!"
