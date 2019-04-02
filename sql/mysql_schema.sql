/* SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO"; */
/* SET AUTOCOMMIT = 0; */
/* START TRANSACTION; */
/* SET time_zone = "+00:00"; */

-- --------------------------------------------------------

--
-- Table structure for table `Event` generated from model 'Event'
--

CREATE TABLE IF NOT EXISTS `Event` (
  `eventName` TEXT DEFAULT NULL,
  `params` JSON DEFAULT NULL,
  `organizationId` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Listener` generated from model 'Listener'
--

CREATE TABLE IF NOT EXISTS `Listener` (
  `id` TEXT NOT NULL,
  `eventName` TEXT NOT NULL,
  `userId` TEXT NOT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `LoggedEvent` generated from model 'LoggedEvent'
--

CREATE TABLE IF NOT EXISTS `LoggedEvent` (
  `id` TEXT NOT NULL,
  `eventName` TEXT NOT NULL,
  `params` JSON DEFAULT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Message` generated from model 'Message'
--

CREATE TABLE IF NOT EXISTS `Message` (
  `id` TEXT NOT NULL,
  `content` TEXT NOT NULL,
  `to` JSON NOT NULL,
  `userId` TEXT NOT NULL,
  `organizationId` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Organization` generated from model 'Organization'
--

CREATE TABLE IF NOT EXISTS `Organization` (
  `id` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `Template` generated from model 'Template'
--

CREATE TABLE IF NOT EXISTS `Template` (
  `id` TEXT DEFAULT NULL,
  `template` TEXT DEFAULT NULL,
  `to` JSON DEFAULT NULL,
  `userId` TEXT DEFAULT NULL,
  `organizationId` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Table structure for table `User` generated from model 'User'
--

CREATE TABLE IF NOT EXISTS `User` (
  `id` TEXT NOT NULL,
  `username` TEXT DEFAULT NULL,
  `password` TEXT DEFAULT NULL,
  `apiKey` TEXT DEFAULT NULL,
  `organizationId` TEXT DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

