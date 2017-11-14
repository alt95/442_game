-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 14, 2017 at 10:55 AM
-- Server version: 5.7.19
-- PHP Version: 5.6.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bp`
--

-- --------------------------------------------------------

--
-- Table structure for table `lab`
--

DROP TABLE IF EXISTS `lab`;
CREATE TABLE IF NOT EXISTS `lab` (
  `type` varchar(50) NOT NULL,
  `clicks` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `levelatt`
--

DROP TABLE IF EXISTS `levelatt`;
CREATE TABLE IF NOT EXISTS `levelatt` (
  `level` tinyint(4) NOT NULL,
  `choice` varchar(20) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `leveldef`
--

DROP TABLE IF EXISTS `leveldef`;
CREATE TABLE IF NOT EXISTS `leveldef` (
  `level` tinyint(4) NOT NULL,
  `correct` int(11) NOT NULL,
  `wrong` int(11) NOT NULL,
  `avg_time` decimal(7,2) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `misc`
--

DROP TABLE IF EXISTS `misc`;
CREATE TABLE IF NOT EXISTS `misc` (
  `event` varchar(60) NOT NULL,
  `clicks` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `stat`
--

DROP TABLE IF EXISTS `stat`;
CREATE TABLE IF NOT EXISTS `stat` (
  `level` tinyint(4) NOT NULL,
  `time` varchar(30) NOT NULL,
  `win` tinyint(1) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
