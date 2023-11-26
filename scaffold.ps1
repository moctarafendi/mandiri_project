$serverName = "LAPTOP-E88QR8QI\SQLSERVER2017";
$database = "Mandiri";
$userId = "sa";
$password = "12345";
$provider = "Microsoft.EntityFrameworkCore.SqlServer";
$entityFolderPath = "Entities";

$connectionString = "Server=$($serverName);Database=$($database);User Id=$($userId);Password=$($password);";

$dbContextName = "AppDbContext";

# -c: set DB Context name to...
# -d: use Data Annotation / attributes where possible.
# --use-database-names: do not PascalCase-ify table and column names.
# -v: show verbose output.
$command = "dotnet ef dbcontext scaffold ""$ConnectionString"" $provider -d -f -c $dbContextName -v -o $entityFolderPath"

Invoke-Expression "$command";