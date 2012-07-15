-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.5.16 - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2012-07-12 21:16:32
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for ragedb
CREATE DATABASE IF NOT EXISTS `ragedb` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `ragedb`;


-- Dumping structure for table ragedb.habbo
CREATE TABLE IF NOT EXISTS `habbo` (
  `habbo_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `sso_ticket` varchar(128) NOT NULL,
  `sso_ip` varchar(128) NOT NULL,
  `figure` varchar(256) NOT NULL,
  `fuserank_id` int(1) DEFAULT '1',
  `gender` enum('F','M') NOT NULL,
  `credits` int(255) DEFAULT '100',
  `activity_points` int(255) DEFAULT '100',
  `last_online` datetime DEFAULT NULL,
  PRIMARY KEY (`habbo_id`),
  UNIQUE KEY `habbo_id` (`habbo_id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`),
  UNIQUE KEY `sso_ticket` (`sso_ticket`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='A table to store habbo data.\r\n';

-- Data exporting was unselected.


-- Dumping structure for table ragedb.users
CREATE TABLE IF NOT EXISTS `users` (
  `user_id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(32) NOT NULL,
  `email` varchar(50) NOT NULL,
  `encrypted_password` varchar(256) NOT NULL,
  `gender` enum('F','M') NOT NULL,
  `sign_in_counts` int(11) NOT NULL,
  `user_ip` varchar(24) NOT NULL,
  `last_ip` varchar(24) NOT NULL,
  `created_at` datetime NOT NULL,
  `last_signed_in_at` datetime NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_id` (`user_id`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Generic users table, this is not for in-game stuff, only for web stuff.\r\n';

-- Data exporting was unselected.
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;