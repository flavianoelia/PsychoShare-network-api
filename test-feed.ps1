# Script de diagnóstico del feed
Write-Host "=== DIAGNÓSTICO DEL FEED ===" -ForegroundColor Yellow

# Login
$loginBody = @{
    email = "flavia@psychoshare.com"
    password = "Fla1234!"
} | ConvertTo-Json

$loginResponse = Invoke-RestMethod -Uri "http://localhost:5174/api/users/login" -Method POST -Headers @{"Content-Type"="application/json"} -Body $loginBody
$token = $loginResponse.data.token
$headers = @{"Authorization"="Bearer $token"}

Write-Host "`n1. A quién sigues:" -ForegroundColor Cyan
$followings = Invoke-RestMethod -Uri "http://localhost:5174/api/users/followings" -Headers $headers
Write-Host "Total siguiendo: $($followings.data.Count)"
$followings.data | Select-Object -First 10 id, name, lastName | Format-Table

Write-Host "`n2. Posts en el FEED (página 1, size 10):" -ForegroundColor Cyan
$feed1 = Invoke-RestMethod -Uri "http://localhost:5174/api/posts/feed?page=1&size=10" -Headers $headers
Write-Host "Total posts disponibles: $($feed1.totalCount)"
Write-Host "Mostrando en página 1: $($feed1.posts.Count)"
$feed1.posts | Format-Table id, userId, nameOwner, title

Write-Host "`n3. Posts en el FEED (página 1, size 50 - TODOS):" -ForegroundColor Cyan
$feedAll = Invoke-RestMethod -Uri "http://localhost:5174/api/posts/feed?page=1&size=50" -Headers $headers
Write-Host "Total: $($feedAll.totalCount) | Mostrando: $($feedAll.posts.Count)"
$feedAll.posts | Format-Table id, userId, nameOwner, lastnameOwner, title

Write-Host "`n4. TODOS los posts del sistema (no solo feed):" -ForegroundColor Cyan
$allPosts = Invoke-RestMethod -Uri "http://localhost:5174/api/posts?page=1&size=50" -Headers $headers
Write-Host "Total posts en el sistema: $($allPosts.totalCount)"
$allPosts.posts | Format-Table id, userId, nameOwner, lastnameOwner, title
