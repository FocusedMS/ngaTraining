$ErrorActionPreference = 'Stop'

function Invoke-Safe {
	param(
		[string]$Method,
		[string]$Uri,
		[object]$Body
	)
	try {
		$headers = @{ 'Accept' = 'application/json' }
		if ($PSBoundParameters.ContainsKey('Body') -and $null -ne $Body) {
			Invoke-WebRequest -Method $Method -Uri $Uri -Headers $headers -ContentType 'application/json' -Body ($Body | ConvertTo-Json -Compress) -UseBasicParsing
		} else {
			Invoke-WebRequest -Method $Method -Uri $Uri -Headers $headers -UseBasicParsing
		}
	} catch {
		return $_.Exception.Response
	}
}

$base = if ($env:API_BASE_URL) { $env:API_BASE_URL } else { 'http://localhost:5067' }

Write-Host '--- Running CLI API checks ---'

# Create author
$authorResp = Invoke-Safe -Method 'POST' -Uri "$base/api/authors" -Body @{ name = 'J. K. Rowling' }
$authorStatus = [int]$authorResp.StatusCode
$authorId = ($authorResp.Content | ConvertFrom-Json).id
Write-Host ("AuthorCreate: status=$authorStatus id=$authorId")

# Create book
$bookResp = Invoke-Safe -Method 'POST' -Uri "$base/api/books" -Body @{ title = 'HP 1'; authorId = $authorId; publicationYear = 1997 }
$bookStatus = [int]$bookResp.StatusCode
$bookId = ($bookResp.Content | ConvertFrom-Json).id
Write-Host ("BookCreate: status=$bookStatus id=$bookId")

# List books
$listResp = Invoke-Safe -Method 'GET' -Uri "$base/api/books"
$listStatus = [int]$listResp.StatusCode
$listJson = $null; if ($listResp.Content) { $listJson = $listResp.Content | ConvertFrom-Json }
$listCount = @($listJson).Count
Write-Host ("BooksList: status=$listStatus count=$listCount")

# Get book
$getResp = Invoke-Safe -Method 'GET' -Uri "$base/api/books/$bookId"
$getStatus = [int]$getResp.StatusCode
Write-Host ("BookGet: status=$getStatus")

# Books by author
$byAuthorResp = Invoke-Safe -Method 'GET' -Uri "$base/api/authors/$authorId/books"
$byAuthorStatus = [int]$byAuthorResp.StatusCode
$byJson = $null; if ($byAuthorResp.Content) { $byJson = $byAuthorResp.Content | ConvertFrom-Json }
$byAuthorCount = @($byJson).Count
Write-Host ("BooksByAuthor: status=$byAuthorStatus count=$byAuthorCount")

# Update book
$updateResp = Invoke-Safe -Method 'PUT' -Uri "$base/api/books/$bookId" -Body @{ id = $bookId; title = 'HP 1 Updated'; authorId = $authorId; publicationYear = 1998 }
$updateStatus = [int]$updateResp.StatusCode
Write-Host ("BookUpdate: status=$updateStatus")

# Delete book
$deleteResp = Invoke-Safe -Method 'DELETE' -Uri "$base/api/books/$bookId"
$deleteStatus = [int]$deleteResp.StatusCode
Write-Host ("BookDelete: status=$deleteStatus")

# Validation: missing title
$valResp = Invoke-Safe -Method 'POST' -Uri "$base/api/books" -Body @{ authorId = $authorId; publicationYear = 2000 }
$valStatus = [int]$valResp.StatusCode
Write-Host ("ValidationMissingTitle: status=$valStatus")

# Unknown author
$unkResp = Invoke-Safe -Method 'POST' -Uri "$base/api/books" -Body @{ title = 'X'; authorId = 99999; publicationYear = 2001 }
$unkStatus = [int]$unkResp.StatusCode
Write-Host ("UnknownAuthor: status=$unkStatus")

# Boundary tests for publicationYear
$yrOkResp = Invoke-Safe -Method 'POST' -Uri "$base/api/books" -Body @{ title = 'Boundary OK'; authorId = $authorId; publicationYear = 3000 }
$yrOkStatus = [int]$yrOkResp.StatusCode
Write-Host ("Year3000: status=$yrOkStatus")

$yrFailResp = Invoke-Safe -Method 'POST' -Uri "$base/api/books" -Body @{ title = 'Boundary Fail'; authorId = $authorId; publicationYear = 3001 }
$yrFailStatus = [int]$yrFailResp.StatusCode
Write-Host ("Year3001: status=$yrFailStatus")

$allOk = (
	$authorStatus -eq 201 -and
	$bookStatus -eq 201 -and
	$listStatus -eq 200 -and
	$getStatus -eq 200 -and
	$byAuthorStatus -eq 200 -and
	$updateStatus -eq 204 -and
	$deleteStatus -eq 204 -and
	$valStatus -eq 400 -and
	$unkStatus -eq 404 -and
	$yrOkStatus -eq 201 -and
	$yrFailStatus -eq 400
)

Write-Host ("RequirementsSatisfied: " + $allOk)
