INSERT INTO Posts (Id, UserId, NameOwner, LastnameOwner, Title, Description, Authorship, Resume, PdfId, ImageId, ImgOwnerId) 
VALUES (1, 1, 'Flavia', 'Perin', 'Mi primer post de prueba', 'Este es un post de prueba para demonstrar el sistema de likes. Contiene información interesante sobre psicología.', 'Flavia Perin', 'Post de prueba para el sistema de likes', NULL, NULL, 1);

SELECT * FROM Posts WHERE Id = 1;
SELECT * FROM Persons WHERE Id = 1 AND PersonType = 'User';