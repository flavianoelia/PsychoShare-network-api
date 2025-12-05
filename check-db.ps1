# Verificar estado de la base de datos
$env:MYSQL_PWD = "1234"
$mysqlPath = "C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"

Write-Host "=== SEGUIMIENTOS DE USUARIO ID 1 (Flavia) ===" -ForegroundColor Cyan
& $mysqlPath -u root psychoshare -e "SELECT f.UserId, f.FollowedId, CONCAT(p.Name, ' ', p.LastName) as 'Siguiendo a' FROM Followings f JOIN Persons p ON f.FollowedId = p.Id WHERE f.UserId = 1 ORDER BY f.Id;"

Write-Host "`n=== POSTS DE USUARIOS QUE SIGUE (debería verlos en el feed) ===" -ForegroundColor Yellow  
& $mysqlPath -u root psychoshare -e "SELECT p.Id, p.UserId, CONCAT(u.Name, ' ', u.LastName) as 'Autor', p.Title, DATE_FORMAT(p.CreatedAt, '%Y-%m-%d %H:%i') as 'Creado' FROM Posts p JOIN Persons u ON p.UserId = u.Id WHERE p.UserId IN (SELECT FollowedId FROM Followings WHERE UserId = 1) OR p.UserId = 1 ORDER BY p.Id DESC LIMIT 20;"

Write-Host "`n=== TOTAL DE POSTS POR USUARIO QUE SIGUE ===" -ForegroundColor Green
& $mysqlPath -u root psychoshare -e "SELECT u.Id, CONCAT(u.Name, ' ', u.LastName) as 'Usuario', COUNT(p.Id) as 'Posts' FROM Persons u LEFT JOIN Posts p ON u.Id = p.UserId WHERE u.Id IN (SELECT FollowedId FROM Followings WHERE UserId = 1) OR u.Id = 1 GROUP BY u.Id ORDER BY u.Id;"

$env:MYSQL_PWD = $null
