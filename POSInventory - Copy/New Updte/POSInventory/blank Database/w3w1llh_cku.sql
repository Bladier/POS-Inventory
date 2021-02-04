-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 25, 2018 at 08:17 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 7.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `w3w1llh@cku`
--

-- --------------------------------------------------------

--
-- Table structure for table `doc`
--

CREATE TABLE `doc` (
  `DOCID` bigint(20) NOT NULL,
  `DOCTYPE` smallint(6) NOT NULL DEFAULT '0',
  `MOP` varchar(1) NOT NULL DEFAULT 'C',
  `DOCDATE` date NOT NULL,
  `CODE` varchar(20) NOT NULL,
  `CUSTOMER` varchar(50) DEFAULT 'One-Time Customer',
  `NOVAT` decimal(12,2) DEFAULT NULL,
  `VATRATE` decimal(12,2) DEFAULT NULL,
  `VATTOTAL` decimal(12,2) DEFAULT NULL,
  `DOCTOTAL` decimal(12,3) NOT NULL DEFAULT '0.000',
  `STATUS` varchar(1) NOT NULL DEFAULT '1',
  `REMARKS` varchar(255) DEFAULT NULL,
  `USERID` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `doclines`
--

CREATE TABLE `doclines` (
  `DLID` bigint(20) NOT NULL,
  `DOCID` bigint(20) NOT NULL DEFAULT '0',
  `ITEMCODE` varchar(20) NOT NULL,
  `DESCRIPTION` varchar(255) NOT NULL,
  `QTY` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `UNITPRICE` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `SALEPRICE` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `ROWTOTAL` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `UOM` varchar(20) DEFAULT NULL,
  `REMARKS` varchar(255) DEFAULT NULL,
  `ENDING` decimal(12,6) NOT NULL DEFAULT '0.000000'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `inv`
--

CREATE TABLE `inv` (
  `DOCID` bigint(20) NOT NULL,
  `DOCNUM` varchar(20) NOT NULL,
  `DOCDATE` date DEFAULT NULL,
  `PARTNER` varchar(50) NOT NULL DEFAULT '',
  `GRANDTOTAL` decimal(12,2) NOT NULL DEFAULT '0.00',
  `REMARKS` varchar(255) DEFAULT NULL,
  `DOCSTATUS` varchar(20) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `invlines`
--

CREATE TABLE `invlines` (
  `INVID` bigint(20) NOT NULL,
  `DOCID` bigint(20) NOT NULL,
  `ITEMCODE` varchar(20) DEFAULT NULL,
  `DESCRIPTION` varchar(255) DEFAULT NULL,
  `QTY` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `UNITPRICE` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `SALEPRICE` decimal(12,6) DEFAULT '0.000000',
  `ROWTOTAL` decimal(12,6) NOT NULL DEFAULT '0.000000',
  `UOM` varchar(20) DEFAULT NULL,
  `REMARKS` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `UNITPRICE` decimal(12,3) NOT NULL DEFAULT '0.000',
  `SALEPRICE` decimal(12,3) NOT NULL DEFAULT '0.000',
  `ISSALE` varchar(1) NOT NULL DEFAULT '1',
  `ISINV` varchar(1) NOT NULL DEFAULT '1',
  `ONHAND` decimal(12,2) NOT NULL DEFAULT '0.00',
  `REMARKS` varchar(150) DEFAULT NULL,
  `ADDTIME` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UPDATETIME` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ONHOLD` varchar(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Stand-in structure for view `sales_or`
-- (See below for the actual view)
--
CREATE TABLE `sales_or` (
`DOCID` bigint(20)
,`DOCTYPE` smallint(6)
,`MOP` varchar(1)
,`CODE` varchar(20)
,`CUSTOMER` varchar(50)
,`DOCDATE` date
,`NOVAT` decimal(12,2)
,`VATRATE` decimal(12,2)
,`VATTOTAL` decimal(12,2)
,`DOCTOTAL` decimal(12,3)
,`STATUS` varchar(1)
,`REMARKS` varchar(255)
,`ITEMCODE` varchar(20)
,`DESCRIPTION` varchar(255)
,`QTY` decimal(12,6)
,`UNITPRICE` decimal(12,6)
,`SALEPRICE` decimal(12,6)
,`ROWTOTAL` decimal(12,6)
);

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

-- --------------------------------------------------------

--
-- Table structure for table `tbluser`
--

CREATE TABLE `tbluser` (
  `ID` int(15) NOT NULL,
  `firstname` varchar(30) NOT NULL,
  `middlename` varchar(30) DEFAULT NULL,
  `lastname` varchar(30) NOT NULL,
  `uname` varchar(50) NOT NULL,
  `pword` varchar(50) NOT NULL,
  `userrole` varchar(30) NOT NULL,
  `status` varchar(1) NOT NULL DEFAULT '1',
  `lastLogin` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbluser`
--

INSERT INTO `tbluser` (`ID`, `firstname`, `middlename`, `lastname`, `uname`, `pword`, `userrole`, `status`, `lastLogin`) VALUES
(1, 'Admin', '', 'Admin', 'Admin', 'GSTLEXYzj+g=', 'Admin', '1', '2018-05-25 06:13:15');

-- --------------------------------------------------------

--
-- Structure for view `sales_or`
--
DROP TABLE IF EXISTS `sales_or`;

CREATE ALGORITHM=UNDEFINED DEFINER=`blade`@`localhost` SQL SECURITY DEFINER VIEW `sales_or`  AS  select `doc`.`DOCID` AS `DOCID`,`doc`.`DOCTYPE` AS `DOCTYPE`,`doc`.`MOP` AS `MOP`,`doc`.`CODE` AS `CODE`,`doc`.`CUSTOMER` AS `CUSTOMER`,`doc`.`DOCDATE` AS `DOCDATE`,`doc`.`NOVAT` AS `NOVAT`,`doc`.`VATRATE` AS `VATRATE`,`doc`.`VATTOTAL` AS `VATTOTAL`,`doc`.`DOCTOTAL` AS `DOCTOTAL`,`doc`.`STATUS` AS `STATUS`,`doc`.`REMARKS` AS `REMARKS`,`doclines`.`ITEMCODE` AS `ITEMCODE`,`doclines`.`DESCRIPTION` AS `DESCRIPTION`,`doclines`.`QTY` AS `QTY`,`doclines`.`UNITPRICE` AS `UNITPRICE`,`doclines`.`SALEPRICE` AS `SALEPRICE`,`doclines`.`ROWTOTAL` AS `ROWTOTAL` from (`doc` join `doclines` on((`doc`.`DOCID` = `doclines`.`DOCID`))) ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `doc`
--
ALTER TABLE `doc`
  ADD PRIMARY KEY (`DOCID`);

--
-- Indexes for table `doclines`
--
ALTER TABLE `doclines`
  ADD PRIMARY KEY (`DLID`);

--
-- Indexes for table `inv`
--
ALTER TABLE `inv`
  ADD PRIMARY KEY (`DOCID`);

--
-- Indexes for table `invlines`
--
ALTER TABLE `invlines`
  ADD PRIMARY KEY (`INVID`);

--
-- Indexes for table `itemmaster`
--
ALTER TABLE `itemmaster`
  ADD PRIMARY KEY (`ITEMID`);

--
-- Indexes for table `tbldaily`
--
ALTER TABLE `tbldaily`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tblmaintenance`
--
ALTER TABLE `tblmaintenance`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tbluser`
--
ALTER TABLE `tbluser`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `doc`
--
ALTER TABLE `doc`
  MODIFY `DOCID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `doclines`
--
ALTER TABLE `doclines`
  MODIFY `DLID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `inv`
--
ALTER TABLE `inv`
  MODIFY `DOCID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `invlines`
--
ALTER TABLE `invlines`
  MODIFY `INVID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `itemmaster`
--
ALTER TABLE `itemmaster`
  MODIFY `ITEMID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `tbldaily`
--
ALTER TABLE `tbldaily`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `tblmaintenance`
--
ALTER TABLE `tblmaintenance`
  MODIFY `ID` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `tbluser`
--
ALTER TABLE `tbluser`
  MODIFY `ID` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
