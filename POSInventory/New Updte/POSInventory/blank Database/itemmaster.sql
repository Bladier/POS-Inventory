-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 04, 2021 at 08:33 AM
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
-- Table structure for table `itemmaster`
--

CREATE TABLE `itemmaster` (
  `ITEMID` bigint(20) NOT NULL,
  `ITEMCODE` varchar(50) NOT NULL,
  `DESCRIPTION` varchar(100) NOT NULL,
  `CATEGORIES` varchar(30) DEFAULT NULL,
  `SUBCAT` varchar(30) DEFAULT NULL,
  `UOM` varchar(30) DEFAULT NULL,
  `UNITPRICE` decimal(12,3) NOT NULL DEFAULT 0.000,
  `SALEPRICE` decimal(12,3) NOT NULL DEFAULT 0.000,
  `ISSALE` varchar(1) NOT NULL DEFAULT '1',
  `ISINV` varchar(1) NOT NULL DEFAULT '1',
  `ONHAND` decimal(12,2) NOT NULL DEFAULT 0.00,
  `REMARKS` varchar(150) DEFAULT NULL,
  `ADDTIME` timestamp NOT NULL DEFAULT current_timestamp(),
  `UPDATETIME` timestamp NOT NULL DEFAULT current_timestamp(),
  `ONHOLD` varchar(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `itemmaster`
--
ALTER TABLE `itemmaster`
  ADD PRIMARY KEY (`ITEMID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `itemmaster`
--
ALTER TABLE `itemmaster`
  MODIFY `ITEMID` bigint(20) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
