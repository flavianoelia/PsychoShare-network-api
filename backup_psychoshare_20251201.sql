-- MySQL dump 10.13  Distrib 9.1.0, for Win64 (x86_64)
--
-- Host: localhost    Database: psychoshare
-- ------------------------------------------------------
-- Server version	9.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20250904232554_InitialCreate','9.0.8'),('20250920205542_InitialMigration','8.0.0'),('20250925142652_AlterUser_RemoveUsername','8.0.0'),('20250928034424_AddUserEmailUniqueIndex','8.0.0'),('20251001221313_Alter_Post_correccion_sintaxis_campos_requeridos_imageOwner_opcional','8.0.0'),('20251002231753_Profe','8.0.0'),('20251002232302_Profe2','8.0.0'),('20251003192107_AddDiscriminatorTPHInheritance','8.0.0'),('20251006201848_Post_modification_File_add_to_database','8.0.0'),('20251010001329_FixCommentsCascadeDelete','8.0.0'),('20251106221648_AddProfessionalLicense','8.0.0'),('20251106222447_SeparateVerifiedNames','8.0.0'),('20251122194013_AddTimestampsToPost','8.0.0'),('20251122210937_RefactorAvatarRelationship','8.0.0'),('20251122000808_AddBaseFileStructure','8.0.0'),('20251124160759_AddNameColumnToBaseFiles','8.0.0'),('20251124202614_ChangedTableRoleForRoleTypeEnum┬á','8.0.0'),('20251130193255_AddUserCascadeDelete','8.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bans`
--

DROP TABLE IF EXISTS `bans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bans` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `BannedUserId` bigint NOT NULL,
  `BannedByAdminId` bigint NOT NULL,
  `RelatedReportId` bigint DEFAULT NULL,
  `StartDate` datetime(6) NOT NULL,
  `EndDate` datetime(6) DEFAULT NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `BanType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `BanUserId` bigint DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Bans_BanUserId` (`BanUserId`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bans`
--

LOCK TABLES `bans` WRITE;
/*!40000 ALTER TABLE `bans` DISABLE KEYS */;
INSERT INTO `bans` VALUES (1,3,1,NULL,'2025-11-13 03:20:20.337000','2025-11-13 00:33:15.605354','Other','Temporary',0,NULL),(2,3,1,NULL,'2025-11-13 03:40:26.246000','2025-11-13 00:41:52.243741','Spam','Temporary',0,NULL),(3,3,1,NULL,'2025-11-13 03:45:19.385000','2025-11-13 00:46:00.412327','Hate Speech','Temporary',0,NULL),(4,3,1,NULL,'2025-11-13 03:46:22.415000','2025-11-13 00:46:42.554495','Harassment','Temporary',0,NULL),(5,3,1,NULL,'2025-11-13 03:47:25.381000','2025-11-13 00:47:35.450870','Multiple Violations','Temporary',0,NULL),(6,3,1,NULL,'2025-11-13 03:52:00.904000','2025-11-13 00:52:38.385540','Violence/Threats','Temporary',0,NULL),(7,3,1,NULL,'2025-11-13 03:56:22.992000','2025-11-13 00:56:54.121001','Identity Theft','Temporary',0,NULL),(8,2,1,NULL,'2025-11-17 12:00:00.000000','2025-11-24 12:00:00.000000','Spam en publicaciones','temporary',1,NULL),(9,22,1,NULL,'2025-11-19 01:32:19.060000','2025-11-26 01:32:19.060000','Spam','Temporary',1,NULL);
/*!40000 ALTER TABLE `bans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `basefiles`
--

DROP TABLE IF EXISTS `basefiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `basefiles` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Url` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `IdUser` bigint DEFAULT NULL,
  `Discriminator` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ImageType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Pdf_IdUser` bigint DEFAULT NULL,
  `Pdf_Url` longtext,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Pdf_Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_BaseFiles_IdUser` (`IdUser`),
  KEY `IX_BaseFiles_Pdf_IdUser` (`Pdf_IdUser`)
) ENGINE=MyISAM AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `basefiles`
--

LOCK TABLES `basefiles` WRITE;
/*!40000 ALTER TABLE `basefiles` DISABLE KEYS */;
INSERT INTO `basefiles` VALUES (43,NULL,'http://localhost:5174/uploads/avatars/21104212-bb3d-4c81-a5c7-4c18ce972296.png',1,'Avatar','',NULL,NULL,'',NULL,NULL),(9,NULL,'http://localhost:5174/uploads/avatars/ana.jpg',2,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(10,NULL,'http://localhost:5174/uploads/avatars/carlos.jpg',22,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(11,NULL,'http://localhost:5174/uploads/avatars/maxi.jpg',25,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(12,NULL,'http://localhost:5174/uploads/avatars/mauro.jpg',24,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(13,NULL,'http://localhost:5174/uploads/avatars/paula.jpg',23,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(14,NULL,'http://localhost:5174/uploads/avatars/tamy.jpg',26,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(15,NULL,'http://localhost:5174/uploads/avatars/caty.jpg',27,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(38,NULL,'http://localhost:5174/uploads/avatars/2508af87-be94-488d-bd95-93afad19cb8a.png',28,'Avatar','',NULL,NULL,'',NULL,NULL),(17,NULL,'http://localhost:5174/uploads/avatars/tito.jpg',29,'Avatar','Avatar',NULL,NULL,NULL,NULL,NULL),(19,NULL,'http://localhost:5174/uploads/images/cc511981-15e1-45bb-923f-b63d1c2e68ae.jpg',28,'Image','',NULL,NULL,'maria-lupan-fE5IaNta2KM-unsplash.jpg',NULL,NULL),(20,'',NULL,NULL,'Pdf',NULL,28,'http://localhost:5174/uploads/pdfs/0219af8e-324c-4eb9-a93b-6ae2c46e582e.pdf',NULL,'Boleta-Pago.pdf',''),(21,NULL,'http://localhost:5174/uploads/images/0c1cf8c5-e0ac-478a-9b31-67291e83bfc9.jpg',28,'Image','',NULL,NULL,'maria-lupan-fE5IaNta2KM-unsplash.jpg',NULL,NULL),(22,'',NULL,NULL,'Pdf',NULL,28,'http://localhost:5174/uploads/pdfs/78562db4-a308-480c-85e4-8fe1a97c789b.pdf',NULL,'Boleta-Pago.pdf',''),(23,NULL,'http://localhost:5174/uploads/images/ce24cc57-50fb-460b-8971-a13ea0059d59.png',28,'Image','',NULL,NULL,'Captura de pantalla 2025-04-25 155511.png',NULL,NULL),(24,'',NULL,NULL,'Pdf',NULL,28,'http://localhost:5174/uploads/pdfs/1b2d992d-deb7-415e-882a-63211d51ded7.pdf',NULL,'comprobante (2).pdf',''),(25,NULL,'http://localhost:5174/uploads/images/0016fac7-e8e9-47ba-8973-80af8a5910e8.png',27,'Image','',NULL,NULL,'PsychoShare_logo_no_bg.png',NULL,NULL),(26,'',NULL,NULL,'Pdf',NULL,27,'http://localhost:5174/uploads/pdfs/da41a7e3-024d-4217-b72b-8c2c50cecd69.pdf',NULL,'Tema_ Proyecto web-scraping a la p├ígina planetadelibros.pdf',''),(27,NULL,'http://localhost:5174/uploads/images/3c398c9e-6947-4eef-bcc8-c3233c78b7da.png',27,'Image','',NULL,NULL,'Cat pressing two buttons .png',NULL,NULL),(28,'',NULL,NULL,'Pdf',NULL,27,'http://localhost:5174/uploads/pdfs/259b82e2-39db-481e-94ec-63ee844692b9.pdf',NULL,'Nuevo_requerimiento-_Sistema_Docente_(11).pdf',''),(29,NULL,'http://localhost:5174/uploads/images/34d9f682-6488-4a47-a9ff-2fea0174bfb4.jpg',28,'Image','',NULL,NULL,'tito.jpg',NULL,NULL),(30,'',NULL,NULL,'Pdf',NULL,28,'http://localhost:5174/uploads/pdfs/177beb85-5a83-428d-b369-cd4263374fc9.pdf',NULL,'Boleta-Pago.pdf',''),(31,NULL,'http://localhost:5174/uploads/images/7f3234bf-d290-4fca-850b-913997ebd4cc.jpg',28,'Image','',NULL,NULL,'Imagen de WhatsApp 2025-10-30 a las 19.34.19_f4695ffepaanda404.jpg',NULL,NULL),(32,'',NULL,NULL,'Pdf',NULL,28,'http://localhost:5174/uploads/pdfs/da27b335-c60c-44ee-8165-a9ed96e8f25a.pdf',NULL,'Perin Mauro (1).pdf',''),(33,NULL,'http://localhost:5174/uploads/images/cab7e17f-da8f-4412-a69e-550012b8befd.jpg',1,'Image','',NULL,NULL,'Imagen de WhatsApp 2025-10-30 a las 19.34.19_f4695ffepaanda404.jpg',NULL,NULL),(34,'',NULL,NULL,'Pdf',NULL,1,'http://localhost:5174/uploads/pdfs/63a7bf84-45ee-4aad-9467-50f7b29d6b13.pdf',NULL,'ejerciciosIngenieria.pdf',''),(35,NULL,'http://localhost:5174/uploads/images/d290d523-fd7f-465e-820b-6d60c26c251f.png',1,'Image','',NULL,NULL,'ChatGPT Image Apr 29, 2025, 10_40_09 PM.png',NULL,NULL),(36,'',NULL,NULL,'Pdf',NULL,1,'http://localhost:5174/uploads/pdfs/7bff088b-c771-4d9f-838e-b5b39c9e7e77.pdf',NULL,'Documento sin t├¡tulo.pdf',''),(37,NULL,'http://localhost:5174/uploads/images/74aa9bc2-f63f-47cc-ac82-a4f8e992efad.png',31,'Image','',NULL,NULL,'cover_issue_1161_es_ES revista psy.png',NULL,NULL),(39,NULL,'http://localhost:5174/uploads/images/501b7309-0132-4a45-9b87-000f7f8d951d.jpg',1,'Image','',NULL,NULL,'Imagen de WhatsApp 2025-05-09 a las 14.50.53_5fa2dd44.jpg',NULL,NULL),(40,'',NULL,NULL,'Pdf',NULL,1,'http://localhost:5174/uploads/pdfs/c6ab8565-44d2-4734-b866-a423e664d935.pdf',NULL,'saldremos-de-esta_javier-erro.pdf',''),(41,NULL,'http://localhost:5174/uploads/images/b3a34e30-ef03-4190-b34f-5305f4f9427b.png',1,'Image','',NULL,NULL,'Revista.png',NULL,NULL),(42,'',NULL,NULL,'Pdf',NULL,1,'http://localhost:5174/uploads/pdfs/726575d2-5aba-4bf9-ba29-83e11cd0d407.pdf',NULL,'NeuropsicologiaDeLosTrastornosDeAnsiedad.pdf',''),(44,NULL,'http://localhost:5174/uploads/images/2c462472-77b3-492f-a940-4c77d6031307.jpg',1,'Image','',NULL,NULL,'Imagen de WhatsApp 2025-10-30 a las 19.34.19_f4695ffepaanda404.jpg',NULL,NULL),(45,'',NULL,NULL,'Pdf',NULL,1,'http://localhost:5174/uploads/pdfs/2445fa3b-63c2-4622-b760-b130ce8ce6f0.pdf',NULL,'comprobante (1).pdf','');
/*!40000 ALTER TABLE `basefiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comments` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `Text` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` bigint NOT NULL,
  `PostId` bigint NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Comments_PostId` (`PostId`),
  KEY `IX_Comments_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=139 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
INSERT INTO `comments` VALUES (1,'┬íExcelente art├¡culo! Me parece muy interesante la perspectiva que presentas sobre este tema.',1,1),(137,'test',1,31),(138,'16',1,23),(134,'13',1,23),(135,'14',1,23),(136,'15',1,23),(132,'11',1,23),(133,'12',1,23),(130,'8',1,23),(131,'10',1,23),(127,'5',1,23),(128,'6',1,23),(129,'7',1,23),(125,'y si aparece el bot├│n ver m├ís',1,23),(126,'4',1,23),(124,'vamos a probar si aparecen todos los comentarios',1,23),(123,'Qu├® significa tu posteo?',1,23),(121,'ffffffffffffffffffff',1,21),(122,'fffffffffffffffffffffffffff',1,21),(120,'fffffffffffffffffffff',1,21),(118,'ffffffffffffffffffffffffffff',1,21),(119,'ffffffffffffffffffffffffff',1,21),(117,'ffffffffffffffffffffffffffff',1,21),(115,'ffffffffffffffffffffffffffffffff',1,21),(116,'fffffffffffffffffffff',1,21),(113,'fffffffffffffffffff',1,21),(114,'ffffffffffffffffffffffffffffffffffff',1,21),(112,'ffffffffffffffffffffffffffff',1,21),(110,'fffffffffffffffffffff',1,21),(111,'ffffffffffffffffffffffffffff',1,21),(108,'fvfvfvfv',28,25),(109,'ffffffffffffffffffffffffffffff',1,21),(107,'fvfvfvfvfv',28,25),(105,'fvfvfvfvfv',28,25),(106,'vfvfvffvfvfvf',28,25),(102,'lo edite y lo borre',1,25),(103,'aca vengo a reportarte de nuevo jejeje',1,4),(99,'frfrfrfrfr',1,25),(100,'frfrfrfrfrfrfrfr',1,25),(97,'frfrfrfrfrf',1,25),(98,'frfrfrfrfrfr',1,25),(95,'frfrfrfrf',1,25),(96,'frfrffrfrfr',1,25),(93,'rtrtrtggggggggggggggggg',1,4),(94,'rfrfrfrfrf',1,25),(92,'rtrtrtrtrt',1,4),(91,'ererererer',1,4),(90,'vfvfvfvfvfvf',1,4),(89,'vfvfvfvfvfv',1,4),(88,'ffghjk',1,25),(87,'me reportaste !! mala',1,4),(86,'mas mala',1,4),(80,'ahora si? edita si si',1,25),(83,'hfbvhfbvhbfvhbfhvbfhvbhf',1,25),(82,'estamos editando?',1,25),(84,'vfvfvfvfvbf',1,25),(85,'mala mala',1,4);
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `followings`
--

DROP TABLE IF EXISTS `followings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `followings` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `FollowedId` bigint NOT NULL,
  `StartDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Followings_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=87 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `followings`
--

LOCK TABLES `followings` WRITE;
/*!40000 ALTER TABLE `followings` DISABLE KEYS */;
INSERT INTO `followings` VALUES (37,1,26,'2025-11-23 18:11:45.040212'),(6,2,3,'2025-10-15 00:43:01.159161'),(7,2,1,'2025-10-15 00:43:12.340880'),(8,23,2,'0000-00-00 00:00:00.000000'),(9,23,3,'0000-00-00 00:00:00.000000'),(10,23,22,'0000-00-00 00:00:00.000000'),(12,1,3,'2025-11-21 16:34:41.431382'),(31,1,2,'2025-11-23 16:55:31.807964'),(45,27,2,'2025-11-24 18:33:21.233753'),(20,27,22,'2025-11-21 22:31:16.184958'),(18,27,3,'2025-11-21 21:43:32.267376'),(47,28,1,'2025-11-25 02:59:20.347089'),(60,28,23,'2025-11-29 21:47:47.271309'),(46,28,2,'2025-11-25 02:59:16.603892'),(24,26,3,'2025-11-22 21:03:26.508434'),(25,1,23,'2025-11-23 13:31:58.851670'),(30,1,24,'2025-11-23 14:18:05.981113'),(29,1,25,'2025-11-23 14:15:54.342847'),(32,26,1,'2025-11-23 16:56:47.569153'),(33,26,23,'2025-11-23 17:43:32.809989'),(34,26,24,'2025-11-23 17:43:40.012950'),(35,26,25,'2025-11-23 17:43:42.430352'),(36,26,27,'2025-11-23 18:08:01.623065'),(41,29,1,'2025-11-23 19:58:38.125772'),(43,29,22,'2025-11-23 20:17:20.380046'),(44,27,28,'2025-11-24 13:20:45.046738'),(51,31,1,'2025-11-29 15:35:17.436807'),(52,31,30,'2025-11-29 15:35:26.556260'),(53,31,2,'2025-11-29 15:35:48.833427'),(54,31,3,'2025-11-29 15:36:05.542476'),(55,32,1,'2025-11-29 15:50:29.955414'),(56,32,3,'2025-11-29 15:50:33.672531'),(57,32,30,'2025-11-29 15:50:40.999178'),(58,32,2,'2025-11-29 15:53:16.725199'),(59,32,31,'2025-11-29 17:55:37.176510'),(61,33,1,'2025-11-30 11:00:56.413179'),(62,33,2,'2025-11-30 11:01:00.813864'),(63,33,30,'2025-11-30 11:01:07.285602'),(64,33,31,'2025-11-30 11:01:12.417257'),(65,33,32,'2025-11-30 11:01:17.351529'),(66,34,1,'2025-11-30 11:04:33.494553'),(67,34,2,'2025-11-30 11:04:36.519643'),(68,34,30,'2025-11-30 11:04:42.862514'),(69,34,31,'2025-11-30 11:04:48.729584'),(70,34,32,'2025-11-30 11:04:55.568678'),(71,34,33,'2025-11-30 11:05:01.999476'),(72,35,1,'2025-11-30 11:09:14.782369'),(73,35,30,'2025-11-30 11:09:24.840617'),(74,35,3,'2025-11-30 11:09:28.283853'),(75,35,31,'2025-11-30 11:09:33.284076'),(76,35,32,'2025-11-30 11:09:38.357573'),(77,35,33,'2025-11-30 11:09:44.574357'),(78,35,34,'2025-11-30 11:09:52.359500'),(79,1,22,'2025-12-01 14:25:37.273548'),(80,1,27,'2025-12-01 14:25:47.347235'),(81,1,28,'2025-12-01 14:25:55.368361'),(82,1,30,'2025-12-01 14:26:11.389958'),(83,1,31,'2025-12-01 14:26:17.269486'),(84,1,35,'2025-12-01 14:26:22.222622'),(85,1,34,'2025-12-01 14:26:27.861311'),(86,1,33,'2025-12-01 14:26:32.551099');
/*!40000 ALTER TABLE `followings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `likes`
--

DROP TABLE IF EXISTS `likes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `likes` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `PostId` bigint NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Likes_PostId` (`PostId`),
  KEY `IX_Likes_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=63 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `likes`
--

LOCK TABLES `likes` WRITE;
/*!40000 ALTER TABLE `likes` DISABLE KEYS */;
INSERT INTO `likes` VALUES (1,1,1),(2,27,7),(3,27,6),(32,28,19),(12,26,7),(9,26,6),(13,26,4),(11,26,8),(14,26,2),(17,29,2),(18,27,13),(23,28,2),(35,28,21),(43,1,2),(37,28,10),(41,1,4),(50,1,25),(57,33,7),(59,1,21),(60,1,23),(61,1,18),(62,1,31);
/*!40000 ALTER TABLE `likes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `persons`
--

DROP TABLE IF EXISTS `persons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `persons` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` varchar(191) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PersonType` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `RoleType` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Persons_Email` (`Email`)
) ENGINE=MyISAM AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `persons`
--

LOCK TABLES `persons` WRITE;
/*!40000 ALTER TABLE `persons` DISABLE KEYS */;
INSERT INTO `persons` VALUES (1,'$2a$11$x30ixybfTryAk/ta6i6fEuZj6ihhfbAx0i7.keQ57WWKgi2boRcgW','flavia@psychoshare.com','Flavia','Perin','User',3),(2,'$2a$11$b.4N7Mq3bHRXcg3.7MqXqOLcxTj0HeeukBYZocd3mDpF.GnY6i/Um','ana@gmail.com','Ana','P├®rez','User',2),(3,'$2a$11$caBF31w1IZe4fzxV0uUg1OMV224312S8Sd5ZJNFG20sexcfU8plj.','juan@gmail.com','Juan','P├®rez','User',2),(22,'$2a$11$55gacLCwpYT.KOqIi9X.HOt0q96FRMf4Hkgn60n5n3K/posDUJVNy','carlos@gmail.com','Carlos','Spam','User',NULL),(25,'$2a$11$odGMUwlgPCWA6rTZp9bF.uxYMeXP6Lc4HVwzEbDaSoq6ry7B5w2CO','maxi@gmail.com','Maximo','Perin','User',2),(24,'$2a$11$NovbLzlXAh1NWwH3S5CdseiVu1DjUsuf2Auh.eMc0z44ieuB77sXS','mau@gmail.com','Mauro','Perin','User',2),(23,'$2a$11$mqoOlhJzNlaUsbqdBarRaOSjHOtVjbHtlILkL1zFDJOc9IxQWFe.W','paula@gmail.com','Paula','Perin','User',NULL),(26,'$2a$11$6qnyWea4fDsMe8uod6zQz.orrQ3hUV0xEQuBF6VMFZw6K1DiuCyYG','tamy@gmail.com','Tamy','Perin','User',2),(27,'$2a$11$lzVH2qNIkuqY4Mj1PWDwae5kv/yRU6X4MEwar5Q1UrI2wcpp1105W','caty@gmail.com','Caty','Perin','User',3),(28,'$2a$11$6HMLON4xuYhKfe.0GtPLf.uf52a8z0rDttA5zMFS6omoutVcRfKea','diana@gmail.com','Diana','Perin','User',2),(29,'$2a$11$UUxlp0A7oB02CdR1h0zesuJcuU9pKFKxhJ6YjtydEP1bNAuzpXPES','tito@gmail.com','Tito','Perin','User',NULL),(30,'$2a$11$qD.bPFiWwcUnI6snwAVnyuWZB4TAyc8SLAywHxGBvs.nLk4J1Fymm','coty@gmail.com','Constanza','Rodriguez','User',3),(31,'$2a$11$s4EdpBBSZDTWmUOleYb0n.1.BXscbp5Fv6/2HxrlMh/OJKTZvD8FS','vero@gmail.com','Ver├│nica','Ramirez','User',1),(33,'$2a$11$xebZoZQWhMH3oEFfjkzH1euSncjN/QO6Muap8NzC/iIp1YvRy8.VC','lau@gmail.com','Laura','Ibaceta','User',1),(34,'$2a$11$Y/JJ8l2tX5j2cMu5re9OheluVMOuDIkf90T0/KITP2/PefQr/xEYi','fede@gmail.com','Federico','Piedrasanta','User',1),(35,'$2a$11$0jL.AjBKt69VupU0RYnfRu59xpRhJTkTOuRCef9nqHPDaL2GkYExK','edu@gmail.com','Eduardo','Rodr├¡guez Pesce','User',1);
/*!40000 ALTER TABLE `persons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `UserId` bigint NOT NULL,
  `NameOwner` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastnameOwner` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Authorship` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Resume` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ImageId` bigint DEFAULT NULL,
  `PdfId` bigint DEFAULT NULL,
  `ImgOwnerId` bigint DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `UpdatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Posts_ImageId` (`ImageId`),
  KEY `IX_Posts_PdfId` (`PdfId`),
  KEY `IX_Posts_ImgOwnerId` (`ImgOwnerId`),
  KEY `IX_Posts_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts`
--

LOCK TABLES `posts` WRITE;
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
INSERT INTO `posts` VALUES (2,1,'Flavia','Perin','Estudio sobre los efectos de la terapia cognitiva en pacientes con ansiedad generalizada durante 12 meses.','Efectos de la Terapia Cognitiva','Dra. Mar├¡a Gonz├ílez, PhD','Investigaci├│n que demuestra mejoras significativas del 78% en pacientes tratados.',NULL,NULL,NULL,'2025-11-22 19:30:31.000000','2025-11-22 19:30:31.000000'),(6,2,'Ana','P├®rez','Estudio sobre rehabilitaci├│n cognitiva en pacientes con TCE','Neuropsicolog├¡a Cl├¡nica  Usuario 2','Dr. Ana Torres, PhD','An├ílisis de t├®cnicas de neuroplasticidad aplicadas',NULL,NULL,NULL,'2025-11-22 19:30:31.000000','2025-11-22 19:30:31.000000'),(4,3,'Juan','P├®rez','Introduction to supervised and unsupervised learning algorithms','Machine Learning Fundamentals','Academic Research Team','This paper explores foundational concepts in machine learning',NULL,NULL,NULL,'2025-11-22 19:30:31.000000','2025-11-22 19:30:31.000000'),(7,2,'Ana','P├®rez','Este post debe ser del usuario 2 para probar following','Post Real del Usuario 2','Usuario Dos','Post de prueba para usuario 2',NULL,NULL,NULL,'2025-11-22 19:30:31.000000','2025-11-22 19:30:31.000000'),(8,22,'Carlos','Spam','Spam spam spam spam. Visita mi sitio web para ofertas incre├¡bles!!!','COMPRA AQU├ì!!! OFERTA INCRE├ìBLE!!!','Carlos Spam','Contenido spam comercial no deseado',NULL,NULL,NULL,'2025-11-22 19:30:31.000000','2025-11-22 19:30:31.000000'),(10,28,'Diana','Perin','stringbbbbb','stringnnnnnnnnnnnnnnn','stringfffff','stringtttttt',21,22,NULL,'2025-11-24 16:42:45.241199','2025-11-24 16:42:45.241199'),(11,28,'Diana','Perin','hhhh','otra publi','ddddd','jjjjj',NULL,NULL,NULL,'2025-11-24 17:02:41.832995','2025-11-24 17:02:41.832995'),(12,28,'Diana','Perin','hhhh','otra publi','ddddd','jjjjj',NULL,NULL,NULL,'2025-11-24 17:03:40.132247','2025-11-24 17:03:40.132247'),(13,28,'Diana','Perin','hhhh','otra publi','ddddd','jjjjj',23,24,NULL,'2025-11-24 17:05:20.237270','2025-11-24 17:05:20.237270'),(14,27,'Caty','Perin','Buenas tardes, les trigo un art....','Ansiedad','An├│nimo','mbngjutoynvhgu',NULL,NULL,NULL,'2025-11-24 20:41:29.170925','2025-11-24 20:41:29.170925'),(15,27,'Caty','Perin','aaaaaaaa','eeeeee','iiiiii','oooooo',NULL,NULL,NULL,'2025-11-24 20:47:00.304882','2025-11-24 20:47:00.304882'),(16,27,'Caty','Perin','mmmmmmm','mmmmm','mmmmm','mmmmm',25,26,NULL,'2025-11-24 20:49:34.668756','2025-11-24 20:49:34.668756'),(17,27,'Caty','Perin','lllllllll','llllllll','lllllllll','lllllllllllllll',27,28,NULL,'2025-11-24 21:50:17.954897','2025-11-24 21:50:17.954897'),(18,28,'Diana','Perin','Buenas...','yyyy','yyyy','yyyyy',29,30,NULL,'2025-11-25 04:04:27.074666','2025-11-25 04:04:27.074666'),(19,28,'Diana','Perin','ult','si','si','ya',31,32,NULL,'2025-11-25 04:28:55.427753','2025-11-25 04:28:55.427753'),(20,28,'Diana','Perin','ggggggg','ggggggggggg','gggggggggg','gggggggggggg',NULL,NULL,NULL,'2025-11-25 21:55:11.391157','2025-11-25 21:55:11.391157'),(21,28,'Diana','Perin','aaaaa','aaaa','aaaaa','aaaaa',NULL,NULL,NULL,'2025-11-25 22:18:31.042978','2025-11-25 22:18:31.042978'),(22,28,'Diana','Perin','ttttt','ttttt','tttt','tttttt',NULL,NULL,NULL,'2025-11-25 22:21:32.294979','2025-11-25 22:21:32.294979'),(23,28,'Diana','Perin','ttttggggtgtgtgt','gtgtgtgtg','gtgtgtgtg','tgtgtgtgtg',NULL,NULL,NULL,'2025-11-25 22:21:53.049257','2025-11-25 22:21:53.049257'),(25,1,'Flavia','Perin','Hablemos','pro','nnnn','mmm',33,34,NULL,'2025-11-28 20:39:29.615263','2025-11-28 20:39:29.615263'),(26,1,'Flavia','Perin','vamos a ver si creo con un pdf si aparece el boton ver pdf, si si si','cdcdcdcmmmmmmmmmmmmmmmmmm','dcdcdcdcd','dcdcdcdcdc',35,36,NULL,'2025-11-29 04:26:44.211633','2025-11-29 04:26:44.211633'),(29,1,'Flavia','Perin','Buenas tardes, les comparto un material de abordaje en situaciones de crisis.','SALDREMOS DE ESTA','Javier Erro','La presentaci├│n introduce la gu├¡a como parte de un movimiento social que busca transformar la forma en que se aborda el sufrimiento ps├¡quico. Desde el activismo por los derechos humanos hasta los enfoques cr├¡ticos dentro de la salud mental, se destacan nuevas sinergias entre personas afectadas directamente y profesionales cr├¡ticos. La obra surge de estos encuentros y se presenta como una herramienta pr├íctica ÔÇöno una soluci├│n completaÔÇö para afrontar situaciones de crisis desde una perspectiva colectiva y no individualista.\r\n\r\nSe subraya la necesidad de desarrollar herramientas pr├ícticas y comunitarias, m├ís all├í del conocimiento especializado, que permitan fortalecer estrategias de apoyo mutuo en entornos cotidianos. La gu├¡a pretende ofrecer orientaciones b├ísicas para quienes acompa├▒an a una persona en crisis, contribuyendo a generar relaciones m├ís sanas, solidarias y sostenedoras frente al sufrimiento psicol├│gico.',39,40,NULL,'2025-11-30 15:07:53.542379','2025-11-30 15:07:53.542379'),(30,1,'Flavia','Perin','Buenas tardes, les comparto material sobre Ansiedad y trastornos de ansiedad integrando una mirada neuropsicol├│gica y neurobiol├│gica.','NEUROPSICOLOGIA DE LOS TRASTORNOS DE ANSIEDAD','Gonz├ílez, R., & Parra-Bola├▒os','Si bien existe cierta diversidad terminol├│gica y conceptual respecto a la ansiedad, desde una conceptualizaci├│n multidimensional se la considera como un estado emocional complejo, difuso y aversivo que se caracteriza por una aprensi├│n excesiva irracional, intranquilidad, tensi├│n, hipervigilancia y preocupaci├│n, acompa├▒ada de la activaci├│n del sistema nervioso aut├│nomo en ausencia de un est├¡mulo espec├¡fico que la desencadene. Sin embargo, esta respuesta emocional que inicialmente es adaptativa se puede convertir en patol├│gica y es aqu├¡ donde aparecen los trastornos de ansiedad que se caracterizan por presentar una dificultad para percibir los aspectos seguros de las situaciones de peligro y por una tendencia a subestimar las capacidades de afrontamiento. El objetivo de este trabajo es realizar una revisi├│n del concepto de ansiedad y los trastornos asociados a ella desde una perspectiva neuropsicol├│gica, explor├índola desde las diferentes escuelas psicol├│gicas hasta su estudio tambi├®n desde una mirada neurobiol├│gica. De esta manera, se realiza una distinci├│n entre la ansiedad y el miedo y se enfatiza en la diferenciaci├│n entre ansiedad normal y ansiedad patol├│gica. Por ├║ltimo, se analiza el impacto que tiene la ansiedad en el funcionamiento cognitivo y se destaca la importancia de su tratamiento.',41,42,NULL,'2025-11-30 15:24:28.921989','2025-11-30 15:24:28.921989'),(31,1,'Flavia','Perin','test','test','test','test',44,45,NULL,'2025-12-01 19:52:05.666430','2025-12-01 19:52:05.666431');
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `professionallicenses`
--

DROP TABLE IF EXISTS `professionallicenses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `professionallicenses` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `LicenseNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VerifiedLastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VerifiedDni` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `IsVerified` tinyint(1) NOT NULL,
  `UserId` bigint NOT NULL,
  `VerifiedFirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_ProfessionalLicenses_UserId` (`UserId`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `professionallicenses`
--

LOCK TABLES `professionallicenses` WRITE;
/*!40000 ALTER TABLE `professionallicenses` DISABLE KEYS */;
INSERT INTO `professionallicenses` VALUES (1,'6176','Perin','27078700',1,1,'Flavia');
/*!40000 ALTER TABLE `professionallicenses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reports`
--

DROP TABLE IF EXISTS `reports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reports` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `ReporterUserId` bigint NOT NULL,
  `ReportedUserId` bigint NOT NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Details` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReportDate` datetime(6) NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContentType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContentId` bigint DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Reports_ReportedUserId` (`ReportedUserId`),
  KEY `IX_Reports_ReporterUserId` (`ReporterUserId`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reports`
--

LOCK TABLES `reports` WRITE;
/*!40000 ALTER TABLE `reports` DISABLE KEYS */;
INSERT INTO `reports` VALUES (10,1,28,'Robo de identidad','foto falsa ...','2025-12-01 17:57:11.052393','Pending','Post',22),(2,3,1,'Contenido inapropiado','Este post contiene informaci├│n falsa y puede ser peligrosa','2025-10-07 20:04:19.832944','Resolved','Post',5),(6,0,2,'Contenido inapropiado','','2025-11-17 01:31:23.624964','Pending','User',NULL),(7,1,22,'Spam','Usuario publicando contenido comercial no deseado','2025-11-18 22:00:52.307375','Resolved','Post',8),(8,1,2,'Robo de identidad','foto falsa','2025-11-29 02:22:00.102957','Pending','Post',6),(9,31,3,'Spam','publicidad no deseada','2025-11-29 15:45:45.524364','Pending','Post',4);
/*!40000 ALTER TABLE `reports` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-12-01 18:07:58
