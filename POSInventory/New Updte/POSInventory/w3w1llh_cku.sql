-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 24, 2018 at 08:08 AM
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

--
-- Dumping data for table `doc`
--

INSERT INTO `doc` (`DOCID`, `DOCTYPE`, `MOP`, `DOCDATE`, `CODE`, `CUSTOMER`, `NOVAT`, `VATRATE`, `VATTOTAL`, `DOCTOTAL`, `STATUS`, `REMARKS`, `USERID`) VALUES
(1, 0, 'C', '2018-05-22', 'INV#000001', 'One Time Customer', '10.00', '0.00', '0.00', '10.000', 'V', NULL, 0),
(2, 0, 'C', '2018-05-22', 'INV#000002', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 0),
(3, 0, 'C', '2018-05-23', 'INV#000003', 'One Time Customer', '7.00', '0.00', '0.00', '7.000', '1', NULL, 1),
(4, 0, 'C', '2018-05-23', 'INV#000004', 'One Time Customer', '22.00', '0.00', '0.00', '22.000', '1', NULL, 1),
(5, 0, 'C', '2018-05-23', 'INV#000005', 'One Time Customer', '7.00', '0.00', '0.00', '7.000', '1', NULL, 1),
(6, 0, 'C', '2018-05-23', 'INV#000006', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(7, 0, 'C', '2018-05-23', 'INV#000007', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(9, 4, 'S', '2018-05-23', 'STO#000001', 'One Time Customer', '0.00', '0.00', '0.00', '7.000', '1', 'sample', 1),
(10, 0, 'C', '2018-05-23', 'INV#000008', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(11, 0, 'C', '2018-05-24', 'INV#000009', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(12, 0, 'C', '2018-05-24', 'INV#000010', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(13, 0, 'C', '2018-05-24', 'INV#000011', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(14, 0, 'C', '2018-05-24', 'INV#000012', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(15, 0, 'C', '2018-05-24', 'INV#000013', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(16, 0, 'C', '2018-05-24', 'INV#000014', 'One Time Customer', '7.00', '0.00', '0.00', '7.000', '1', NULL, 1),
(17, 0, 'C', '2018-05-24', 'INV#000015', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(18, 0, 'C', '2018-05-24', 'INV#000016', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(19, 0, 'C', '2018-05-24', 'INV#000017', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(20, 0, 'C', '2018-05-24', 'INV#000018', 'One Time Customer', '5.00', '0.00', '0.00', '5.000', '1', NULL, 1),
(21, 0, 'C', '2018-05-24', 'INV#000019', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(22, 0, 'C', '2018-05-24', 'INV#000020', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(23, 0, 'C', '2018-05-24', 'INV#000021', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1),
(24, 0, 'C', '2018-05-24', 'INV#000022', 'One Time Customer', '12.00', '0.00', '0.00', '12.000', '1', NULL, 1);

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

--
-- Dumping data for table `doclines`
--

INSERT INTO `doclines` (`DLID`, `DOCID`, `ITEMCODE`, `DESCRIPTION`, `QTY`, `UNITPRICE`, `SALEPRICE`, `ROWTOTAL`, `UOM`, `REMARKS`, `ENDING`) VALUES
(1, 1, '0001', 'pencil monggol 2', '2.000000', '0.000000', '5.000000', '10.000000', 'PCS', NULL, '0.000000'),
(2, 2, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(3, 2, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(4, 3, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(5, 4, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(6, 4, '0001', 'pencil monggol 2', '3.000000', '0.000000', '5.000000', '15.000000', 'PCS', NULL, '0.000000'),
(7, 5, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(8, 6, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(9, 7, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(10, 9, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '0.000000', 'PCS', NULL, '0.000000'),
(11, 10, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(12, 11, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(13, 11, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(14, 12, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(15, 12, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(16, 13, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(17, 13, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(18, 14, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(19, 14, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(20, 15, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(21, 16, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(22, 17, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(23, 18, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(24, 18, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(25, 19, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(26, 19, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(27, 20, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(28, 21, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(29, 21, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(30, 22, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(31, 22, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(32, 23, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(33, 23, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000'),
(34, 24, '00002', 'Uni Pen', '1.000000', '0.000000', '7.000000', '7.000000', 'PCS', NULL, '0.000000'),
(35, 24, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '5.000000', 'PCS', NULL, '0.000000');

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

--
-- Dumping data for table `inv`
--

INSERT INTO `inv` (`DOCID`, `DOCNUM`, `DOCDATE`, `PARTNER`, `GRANDTOTAL`, `REMARKS`, `DOCSTATUS`) VALUES
(1, 'STO#00001', '2018-05-21', 'N/A', '0.00', NULL, '0'),
(2, 'STO#00002', '2018-05-21', 'N/A', '2.00', NULL, '1'),
(3, 'STO#00002', '2018-05-21', 'N/A', '1.00', NULL, '1'),
(4, 'STO#00003', '2018-05-21', 'N/A', '2.00', NULL, '1'),
(5, 'STO#00004', '2018-05-21', 'N/A', '1.00', NULL, '1'),
(6, 'STO#00005', '2018-05-21', 'N/A', '2.00', NULL, '1'),
(7, 'STO#00006', '2018-05-21', 'N/A', '1.00', NULL, '0'),
(8, 'STO#00007', '2018-05-21', 'N/A', '2.00', NULL, '1'),
(9, 'STO#00008', '2018-05-21', 'N/A', '1.00', NULL, '1'),
(10, 'STO#00008', '2018-05-23', 'N/A', '2.00', NULL, '1');

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

--
-- Dumping data for table `invlines`
--

INSERT INTO `invlines` (`INVID`, `DOCID`, `ITEMCODE`, `DESCRIPTION`, `QTY`, `UNITPRICE`, `SALEPRICE`, `ROWTOTAL`, `UOM`, `REMARKS`) VALUES
(1, 1, '0001', 'pencil monggol 2', '20.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 8:41:26 AM'),
(2, 2, '00002', 'Uni Pen', '10.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 10:09:06 AM'),
(3, 3, '00002', 'Uni Pen', '1.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 10:11:04 AM'),
(4, 4, '0001', 'pencil monggol 2', '2.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 10:11:46 AM'),
(5, 4, '00002', 'Uni Pen', '1.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 10:11:46 AM'),
(6, 5, '00002', 'Uni Pen', '1.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 11:55:41 AM'),
(7, 6, '00002', 'Uni Pen', '1.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 11:55:41 AM'),
(8, 6, '0001', 'pencil monggol 2', '2.000000', '0.000000', NULL, '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 11:55:41 AM'),
(9, 7, '0001', 'pencil monggol 2', '1.000000', '0.000000', '5.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 12:44:30 PM'),
(10, 8, '0001', 'pencil monggol 2', '8.000000', '0.000000', '5.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 1:49:58 PM'),
(11, 8, '00002', 'Uni Pen', '8.000000', '0.000000', '7.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 1:49:58 PM'),
(12, 9, '00002', 'Uni Pen', '1.000000', '0.000000', '0.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/21/2018 2:02:43 PM'),
(13, 10, '0001', 'pencil monggol 2', '20.000000', '0.000000', '5.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/23/2018 10:07:11 AM'),
(14, 10, '00002', 'Uni Pen', '20.000000', '0.000000', '7.000000', '0.000000', 'PCS', 'UPLOADED DATE 5/23/2018 10:07:11 AM');

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

--
-- Dumping data for table `itemmaster`
--

INSERT INTO `itemmaster` (`ITEMID`, `ITEMCODE`, `DESCRIPTION`, `CATEGORIES`, `SUBCAT`, `UOM`, `UNITPRICE`, `SALEPRICE`, `ISSALE`, `ISINV`, `ONHAND`, `REMARKS`, `ADDTIME`, `UPDATETIME`, `ONHOLD`) VALUES
(1, '0001', 'pencil monggol 2', 'pens', '', 'PCS', '0.000', '5.000', '1', '1', '3.00', '', '2018-05-21 00:33:49', '2018-05-24 02:29:03', '0'),
(2, '00002', 'Uni Pen', 'pens', '', 'PCS', '0.000', '7.000', '1', '1', '44.00', '', '2018-05-21 02:09:52', '2018-05-24 02:29:03', '0');

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

--
-- Dumping data for table `tbldaily`
--

INSERT INTO `tbldaily` (`ID`, `CURRENTDATE`, `INITIALBAL`, `CASHCOUNT`, `STATUS`, `REMARKS`, `SYSTEMINFO`, `OPENNER`, `CLOSER`, `Overage/Shortage`) VALUES
(4, '2018-05-23', '1000.00', '1050.00', 0, NULL, '2018-05-23', 1, 1, 4);

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
(1, 'InvoiceNum', '23', NULL),
(2, 'SalesReturnNum', '1', NULL),
(3, 'STONum', '2', NULL),
(4, 'InventoryNum', '9', ''),
(5, 'CurrentBalance', '1000', NULL),
(6, 'PRINTER', 'sam', ''),
(7, 'BackupPath', 'C:\\Users\\MISGWAPOHON\\Desktop\\backup', '');

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
(1, 'Admin', '', 'Admin', 'Admin', 'GSTLEXYzj+g=', 'Admin', '1', '2018-05-21 06:54:02'),
(2, 'User', 'User', 'User', '1', 'gRSUjH6XasE=', 'User', '1', NULL);

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
  MODIFY `DOCID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
--
-- AUTO_INCREMENT for table `doclines`
--
ALTER TABLE `doclines`
  MODIFY `DLID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;
--
-- AUTO_INCREMENT for table `inv`
--
ALTER TABLE `inv`
  MODIFY `DOCID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `invlines`
--
ALTER TABLE `invlines`
  MODIFY `INVID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
--
-- AUTO_INCREMENT for table `itemmaster`
--
ALTER TABLE `itemmaster`
  MODIFY `ITEMID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `tbldaily`
--
ALTER TABLE `tbldaily`
  MODIFY `ID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `tblmaintenance`
--
ALTER TABLE `tblmaintenance`
  MODIFY `ID` smallint(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `tbluser`
--
ALTER TABLE `tbluser`
  MODIFY `ID` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
