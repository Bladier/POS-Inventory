-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 04, 2021 at 08:35 AM
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
-- Table structure for table `invlines`
--

CREATE TABLE `invlines` (
  `INVID` bigint(20) NOT NULL,
  `DOCID` bigint(20) NOT NULL,
  `ITEMCODE` varchar(20) DEFAULT NULL,
  `DESCRIPTION` varchar(255) DEFAULT NULL,
  `QTY` decimal(12,6) NOT NULL DEFAULT 0.000000,
  `UNITPRICE` decimal(12,6) NOT NULL DEFAULT 0.000000,
  `SALEPRICE` decimal(12,6) DEFAULT 0.000000,
  `ROWTOTAL` decimal(12,6) NOT NULL DEFAULT 0.000000,
  `UOM` varchar(20) DEFAULT NULL,
  `REMARKS` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `invlines`
--
ALTER TABLE `invlines`
  ADD PRIMARY KEY (`INVID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `invlines`
--
ALTER TABLE `invlines`
  MODIFY `INVID` bigint(20) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
