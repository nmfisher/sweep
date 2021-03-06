/* SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO"; */
/* SET AUTOCOMMIT = 0; */
/* START TRANSACTION; */
/* SET time_zone = "+00:00"; */

-- --------------------------------------------------------

--
-- Table structure for table `BaseMessage` generated from model 'BaseMessage'
--

CREATE TABLE IF NOT EXISTS `BaseMessage` (
  `id` CHAR(36) NOT NULL,
  `content` TEXT NOT NULL,
  `subject` TEXT DEFAULT NULL,
  `fromAddress` TEXT DEFAULT NULL,
  `fromName` TEXT DEFAULT NULL,
  `sendTo` JSON NOT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Event` generated from model 'Event'
--

CREATE TABLE IF NOT EXISTS `Event` (
  `id` CHAR(36) NOT NULL,
  `eventName` TEXT NOT NULL,
  `params` JSON DEFAULT NULL,
  `receivedOn` DATETIME NOT NULL,
  `processedOn` DATETIME DEFAULT NULL,
  `error` TEXT DEFAULT NULL,
  `organizationId` TEXT NOT NULL,
  `actions` JSON DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `EventRequestBody` generated from model 'EventRequestBody'
--

CREATE TABLE IF NOT EXISTS `EventRequestBody` (
  `eventName` TEXT NOT NULL,
  `params` JSON DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Listener` generated from model 'Listener'
--

CREATE TABLE IF NOT EXISTS `Listener` (
  `id` CHAR(36) NOT NULL,
  `eventName` TEXT NOT NULL,
  `eventParams` JSON DEFAULT NULL,
  `organizationId` TEXT NOT NULL,
  `triggerEvent` TEXT DEFAULT NULL,
  `triggerNumber` INT DEFAULT NULL,
  `triggerPeriod` TEXT DEFAULT NULL,
  `triggerMatch` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `ListenerAction` generated from model 'ListenerAction'
--

CREATE TABLE IF NOT EXISTS `ListenerAction` (
  `id` CHAR(36) NOT NULL,
  `eventId` CHAR(36) NOT NULL,
  `listenerId` CHAR(36) NOT NULL,
  `organizationId` CHAR(36) NOT NULL,
  `completed` TINYINT(1) NOT NULL,
  `error` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `ListenerRequestBody` generated from model 'ListenerRequestBody'
--

CREATE TABLE IF NOT EXISTS `ListenerRequestBody` (
  `eventName` TEXT NOT NULL,
  `triggerEvent` TEXT DEFAULT NULL,
  `triggerNumber` DECIMAL(20, 9) DEFAULT NULL,
  `triggerPeriod` TEXT DEFAULT NULL,
  `triggerMatch` TEXT DEFAULT NULL,
  `eventParams` JSON DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `ListenerTemplate` generated from model 'ListenerTemplate'
--

CREATE TABLE IF NOT EXISTS `ListenerTemplate` (
  `listenerId` CHAR(36) NOT NULL,
  `templateId` CHAR(36) NOT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Message` generated from model 'Message'
--

CREATE TABLE IF NOT EXISTS `Message` (
  `id` CHAR(36) NOT NULL,
  `content` TEXT NOT NULL,
  `subject` TEXT NOT NULL,
  `fromAddress` TEXT NOT NULL,
  `fromName` TEXT NOT NULL,
  `sendTo` JSON NOT NULL,
  `organizationId` TEXT NOT NULL,
  `listenerActionId` TEXT NOT NULL,
  `sentOn` DATETIME DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Organization` generated from model 'Organization'
--

CREATE TABLE IF NOT EXISTS `Organization` (
  `id` CHAR(36) NOT NULL,
  `primaryApiKey` TEXT NOT NULL,
  `secondaryApiKey` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `RenderTemplateRequestBody` generated from model 'RenderTemplateRequestBody'
--

CREATE TABLE IF NOT EXISTS `RenderTemplateRequestBody` (
  `params` JSON DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Template` generated from model 'Template'
--

CREATE TABLE IF NOT EXISTS `Template` (
  `id` CHAR(36) NOT NULL,
  `content` TEXT NOT NULL,
  `subject` TEXT DEFAULT NULL,
  `fromAddress` TEXT DEFAULT NULL,
  `fromName` TEXT DEFAULT NULL,
  `sendTo` JSON NOT NULL,
  `organizationId` TEXT NOT NULL,
  `deleted` TINYINT(1) DEFAULT NULL,
  `userId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `TemplateRequestBody` generated from model 'TemplateRequestBody'
--

CREATE TABLE IF NOT EXISTS `TemplateRequestBody` (
  `content` TEXT NOT NULL,
  `subject` TEXT NOT NULL,
  `fromAddress` TEXT NOT NULL,
  `fromName` TEXT NOT NULL,
  `sendTo` JSON NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `User` generated from model 'User'
--

CREATE TABLE IF NOT EXISTS `User` (
  `id` VARCHAR(100) NOT NULL,
  `username` TEXT DEFAULT NULL,
  `password` TEXT DEFAULT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `UserRequestBody` generated from model 'UserRequestBody'
--

CREATE TABLE IF NOT EXISTS `UserRequestBody` (
  `username` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

