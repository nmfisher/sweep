-- MySQL dump 10.17  Distrib 10.3.13-MariaDB, for Win64 (AMD64)
--
-- Host: localhost    Database: sweep_db
-- ------------------------------------------------------
-- Server version	10.3.13-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `basemessage`
--

LOCK TABLES `basemessage` WRITE;
/*!40000 ALTER TABLE `basemessage` DISABLE KEYS */;
/*!40000 ALTER TABLE `basemessage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `eventrequestbody`
--

LOCK TABLES `eventrequestbody` WRITE;
/*!40000 ALTER TABLE `eventrequestbody` DISABLE KEYS */;
/*!40000 ALTER TABLE `eventrequestbody` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `listener`
--

LOCK TABLES `listener` WRITE;
/*!40000 ALTER TABLE `listener` DISABLE KEYS */;
INSERT INTO `listener` VALUES ('05c0fa5e-0be1-4b2f-a751-d6ce917dabb5','signup','[\"username\"]','bef5daed-1997-4ac6-b9c8-edcaf201b8a0',NULL),('0613bef8-982e-4bc9-8573-0db11fa12b67','signup','[\"username\"]','b55d39d8-776f-4045-9b66-2c0fe3c78eea',NULL);
/*!40000 ALTER TABLE `listener` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `listeneraction`
--

LOCK TABLES `listeneraction` WRITE;
/*!40000 ALTER TABLE `listeneraction` DISABLE KEYS */;
/*!40000 ALTER TABLE `listeneraction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `listenerrequestbody`
--

LOCK TABLES `listenerrequestbody` WRITE;
/*!40000 ALTER TABLE `listenerrequestbody` DISABLE KEYS */;
/*!40000 ALTER TABLE `listenerrequestbody` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `listenertemplate`
--

LOCK TABLES `listenertemplate` WRITE;
/*!40000 ALTER TABLE `listenertemplate` DISABLE KEYS */;
INSERT INTO `listenertemplate` VALUES ('05c0fa5e-0be1-4b2f-a751-d6ce917dabb5','1123d230-a696-49c5-9637-496362651755','bef5daed-1997-4ac6-b9c8-edcaf201b8a0'),('0613bef8-982e-4bc9-8573-0db11fa12b67','d06f18f6-576e-4eb8-a689-2fd174e3b7ce','b55d39d8-776f-4045-9b66-2c0fe3c78eea');
/*!40000 ALTER TABLE `listenertemplate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `message`
--

LOCK TABLES `message` WRITE;
/*!40000 ALTER TABLE `message` DISABLE KEYS */;
/*!40000 ALTER TABLE `message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `rendertemplaterequestbody`
--

LOCK TABLES `rendertemplaterequestbody` WRITE;
/*!40000 ALTER TABLE `rendertemplaterequestbody` DISABLE KEYS */;
/*!40000 ALTER TABLE `rendertemplaterequestbody` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `template`
--

LOCK TABLES `template` WRITE;
/*!40000 ALTER TABLE `template` DISABLE KEYS */;
INSERT INTO `template` VALUES ('1123d230-a696-49c5-9637-496362651755','Hi {{username}}  !<br>','Welcome {{username}}!','test@sweephq.com','Testing Sweep Mailer','[\"{{username}}\"]','bef5daed-1997-4ac6-b9c8-edcaf201b8a0',NULL,'nick.fisher@avinium.com'),('d06f18f6-576e-4eb8-a689-2fd174e3b7ce','Hi {{username}}  !<br>','Hi {{username}}!','test@sweephq.com','Sweep Test Mailer','[\"{{username}}\"]','b55d39d8-776f-4045-9b66-2c0fe3c78eea',NULL,'nick.fisher@avinium.com');
/*!40000 ALTER TABLE `template` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `templaterequestbody`
--

LOCK TABLES `templaterequestbody` WRITE;
/*!40000 ALTER TABLE `templaterequestbody` DISABLE KEYS */;
/*!40000 ALTER TABLE `templaterequestbody` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('114797661408259946381','nick.fisher@avinium.com',NULL,'b55d39d8-776f-4045-9b66-2c0fe3c78eea');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `organization`
--

LOCK TABLES `organization` WRITE;
/*!40000 ALTER TABLE `organization` DISABLE KEYS */;
INSERT INTO `organization` VALUES ('b55d39d8-776f-4045-9b66-2c0fe3c78eea','154542ec-59ba-4814-a062-fbad03ec2630','4510d3ab-e2dd-4600-88c2-5e545911e3fe');
/*!40000 ALTER TABLE `organization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `userrequestbody`
--

LOCK TABLES `userrequestbody` WRITE;
/*!40000 ALTER TABLE `userrequestbody` DISABLE KEYS */;
/*!40000 ALTER TABLE `userrequestbody` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-15 14:01:34
