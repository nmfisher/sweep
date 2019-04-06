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
  `subject` TEXT NOT NULL,
  `fromAddress` TEXT NOT NULL,
  `fromName` TEXT NOT NULL,
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
  `receivedOn` dateTime NOT NULL,
  `processedOn` dateTime DEFAULT NULL,
  `error` TEXT DEFAULT NULL,
  `organizationId` TEXT NOT NULL
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
  `id` CHAR(36) DEFAULT NULL,
  `templateId` TEXT NOT NULL,
  `eventName` TEXT NOT NULL,
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
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Organization` generated from model 'Organization'
--

CREATE TABLE IF NOT EXISTS `Organization` (
  `id` CHAR(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Template` generated from model 'Template'
--

CREATE TABLE IF NOT EXISTS `Template` (
  `id` CHAR(36) NOT NULL,
  `content` TEXT NOT NULL,
  `subject` TEXT NOT NULL,
  `fromAddress` TEXT NOT NULL,
  `fromName` TEXT NOT NULL,
  `sendTo` JSON NOT NULL,
  `organizationId` TEXT NOT NULL,
  `deleted` TINYINT(1) DEFAULT NULL,
  `userId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `User` generated from model 'User'
--

CREATE TABLE IF NOT EXISTS `User` (
  `id` CHAR(36) NOT NULL,
  `username` TEXT DEFAULT NULL,
  `password` TEXT DEFAULT NULL,
  `apiKey` TEXT DEFAULT NULL,
  `organizationId` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

