-- Insertar un post de prueba para el sistema de likes
INSERT INTO `Posts` (
    `UserId`, 
    `NameOwner`, 
    `LastnameOwner`, 
    `Description`, 
    `Title`, 
    `Authorship`, 
    `Resume`,
    `ImageId`,
    `PdfId`,
    `ImgOwnerId`
) VALUES (
    1,  -- UserId (Flavia)
    'Flavia',  -- NameOwner
    'Perin',   -- LastnameOwner
    'Este es mi primer post de prueba para el sistema de likes. ¡Espero que funcione correctamente!',  -- Description
    'Post de Prueba para Likes',  -- Title
    'Flavia Perin',  -- Authorship
    'Un post simple para probar la funcionalidad de likes del sistema.',  -- Resume
    NULL,  -- ImageId (sin imagen por ahora)
    NULL,  -- PdfId (sin PDF por ahora) 
    NULL   -- ImgOwnerId (sin imagen de owner)
);