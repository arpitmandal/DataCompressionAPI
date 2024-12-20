-- MySQL dump 10.13  Distrib 5.7.24, for osx11.1 (x86_64)
--
-- Host: localhost    Database: DataCompressionDB
-- ------------------------------------------------------
-- Server version	9.1.0-commercial

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20241114003438_InitialMigrationCreate','7.0.2'),('20241114004717_UpdateMigrationCreateInitialSetup','7.0.2');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `FileLogs`
--

DROP TABLE IF EXISTS `FileLogs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `FileLogs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FileName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ReducedSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL DEFAULT '0',
  `CreatedOn` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `Error` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FileType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FinalSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `InitialSize` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FileLogs_CreatedBy` (`CreatedBy`),
  CONSTRAINT `FK_FileLogs_Users_CreatedBy` FOREIGN KEY (`CreatedBy`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `FileLogs`
--

LOCK TABLES `FileLogs` WRITE;
/*!40000 ALTER TABLE `FileLogs` DISABLE KEYS */;
INSERT INTO `FileLogs` VALUES (13,'Screenshot 2024-11-14 at 6.31.55 PM.png','0.00%',1,'2024-11-16 00:03:00.218670','No errors','image','1240554','1240554'),(14,'Screenshot 2024-11-14 at 6.31.55 PM.png','0.00%',1,'2024-11-16 00:03:58.391497','No errors','image','1240554','1240554'),(15,'10th Marksheet.pdf','0.00%',1,'2024-11-16 00:04:28.499682','No errors','pdf','1746047','1746047'),(16,'Bloodgroup.pdf','0.00%',1,'2024-11-16 00:09:11.776464','No errors','pdf','666.38 KB','666.38 KB'),(17,'DatabaseSchema.mwb','0.00%',1,'2024-11-16 00:09:59.495263','Unsupported file type. Allowed types are image, video, pdf, and word.','Unknown','N/A','7.39 KB'),(18,'WS-011T00A__M051.pdf','0.00%',1,'2024-11-16 00:15:42.338184','No errors','pdf','1.29 MB','1.29 MB'),(19,'MS-900T01A-ENU-PowerPoint_04.pdf','0.00%',1,'2024-11-20 00:00:56.226121','No errors','pdf','1.13 MB','1.13 MB'),(20,'Arpit-Profile photo.jpg','75.57%',1,'2024-11-20 00:07:15.007639','No errors','image','67.85 KB','277.69 KB'),(21,'Arpit-Profile photo.jpg','66.64%',1,'2024-11-20 00:08:03.808891','No errors','image','22.63 KB','67.85 KB');
/*!40000 ALTER TABLE `FileLogs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Role` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'testuser','$2a$11$UcCceLrTiciT9/I.RDglCuyejkN1jwvyBROAUCV2r9tP4xm7D3ZtC','test@gmail.com',1),(3,'testuser2','$2a$11$wJi1dIsz23aQMqgYmdLLVOe110viHAiQbOT2HHRzYAkZI2MHckCKG','arpittest@gmail.com',0),(4,'testuser4','$2a$11$NshalybHmmrM3dFCj2Q1c.95XYYyNsLY.xV98S8xtvok32Fb6ij8a','arpitmandal@gmail.com',0);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-19 19:10:05
