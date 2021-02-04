-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 04, 2021 at 08:30 AM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.2.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `w3w1llh_cku`
--

-- --------------------------------------------------------

--
-- Table structure for table `tbldaily`
--

CREATE TABLE `tbldaily` (
  `ID` bigint(20) NOT NULL,
  `CURRENTDATE` date DEFAULT NULL,
  `INITIALBAL` decimal(12,2) DEFAULT NULL,
  `CASHCOUNT` decimal(12,2) DEFAULT NULL,
  `STATUS` smallint(6) DEFAULT NULL,
  `REMARKS` smallint(6) DEFAULT NULL,
  `SYSTEMINFO` date DEFAULT NULL,
  `OPENNER` smallint(6) DEFAULT NULL,
  `CLOSER` smallint(6) DEFAULT NULL,
  `Overage/Shortage` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tbldaily`
--
ALTER TABLE `tbldaily`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tbldaily`
--
ALTER TABLE `tbldaily`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
