-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-06-2023 a las 06:09:28
-- Versión del servidor: 10.4.24-MariaDB
-- Versión de PHP: 7.4.29

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `bdimagen`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `datos`
--

CREATE TABLE `datos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(150) DEFAULT NULL,
  `R1` int(11) DEFAULT NULL,
  `G1` int(11) DEFAULT NULL,
  `B1` int(11) DEFAULT NULL,
  `R2` int(11) DEFAULT NULL,
  `G2` int(11) DEFAULT NULL,
  `B2` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `datos`
--

INSERT INTO `datos` (`Id`, `Nombre`, `R1`, `G1`, `B1`, `R2`, `G2`, `B2`) VALUES
(49, 'orilla', 77, 97, 156, 0, 255, 255),
(51, 'laguna', 51, 72, 129, 0, 24, 231),
(52, 'decierto', 225, 181, 144, 231, 147, 0),
(53, 'arena movediza', 207, 177, 151, 214, 55, 55),
(54, 'fango', 146, 133, 116, 132, 96, 0),
(56, 'montania', 111, 104, 88, 132, 56, 0),
(58, 'arboles', 35, 61, 40, 4, 255, 0),
(60, 'arbustos', 80, 99, 77, 186, 218, 77),
(61, 'rocas', 127, 127, 127, 0, 0, 0),
(63, 'glaciar', 234, 235, 234, 255, 0, 189);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `datos`
--
ALTER TABLE `datos`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `datos`
--
ALTER TABLE `datos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=64;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
