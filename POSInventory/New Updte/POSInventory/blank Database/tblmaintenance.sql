-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 04, 2021 at 08:32 AM
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
-- Table structure for table `tblmaintenance`
--

CREATE TABLE `tblmaintenance` (
  `ID` smallint(6) NOT NULL,
  `OPT_KEYS` varchar(20) NOT NULL,
  `OPT_VALUES` varchar(255) NOT NULL DEFAULT '',
  `REMARKS` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tblmaintenance`
--

INSERT INTO `tblmaintenance` (`ID`, `OPT_KEYS`, `OPT_VALUES`, `REMARKS`) VALUES
(1, 'InvoiceNum', '1', NULL),
(2, 'SalesReturnNum', '1', NULL),
(3, 'STONum', '1', NULL),
(4, 'InventoryNum', '1', ''),
(5, 'CurrentBalance', '1', NULL),
(6, 'PRINTER', '', ''),
(7, 'BackupPath', 'C:\\Users\\MISGWAPOHON\\Desktop\\backup', ''),
(8, 'StoreName', '', NULL),
(9, 'Address', '', NULL),
(10, 'DBVERSION', '1', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblmaintenance`
--
ALTER TABLE `tblmaintenance`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblmaintenance`
--
ALTER TABLE `tblmaintenance`
  MODIFY `ID` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
