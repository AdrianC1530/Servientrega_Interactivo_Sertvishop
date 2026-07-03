CREATE DATABASE IF NOT EXISTS `servientrega_db` CHARACTER SET utf8mb4;
USE `servientrega_db`;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;
ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Ordenes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `NombreCliente` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UbicacionOrigen` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UbicacionDestino` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Ordenes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Envios` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `OrdenId` int NOT NULL,
    `HoraSalida` datetime(6) NOT NULL,
    `HoraEntregaEstimada` datetime(6) NOT NULL,
    `HoraEntregaReal` datetime(6) NULL,
    `Estado` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Envios` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Envios_Ordenes_OrdenId` FOREIGN KEY (`OrdenId`) REFERENCES `Ordenes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Productos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Precio` decimal(65,30) NOT NULL,
    `OrdenId` int NOT NULL,
    CONSTRAINT `PK_Productos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Productos_Ordenes_OrdenId` FOREIGN KEY (`OrdenId`) REFERENCES `Ordenes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Valoraciones` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `EnvioId` int NOT NULL,
    `Puntuacion` int NOT NULL,
    `Comentario` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Valoraciones` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Valoraciones_Envios_EnvioId` FOREIGN KEY (`EnvioId`) REFERENCES `Envios` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_Envios_OrdenId` ON `Envios` (`OrdenId`);

CREATE INDEX `IX_Productos_OrdenId` ON `Productos` (`OrdenId`);

CREATE UNIQUE INDEX `IX_Valoraciones_EnvioId` ON `Valoraciones` (`EnvioId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260702032943_InitialCreate', '9.0.15');

CREATE TABLE `Catalogo` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Descripcion` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Precio` decimal(65,30) NOT NULL,
    `ImagenUrl` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Catalogo` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `Catalogo` (`Id`, `Descripcion`, `ImagenUrl`, `Nombre`, `Precio`)
VALUES (1, 'RTX 4060, 16GB RAM, 1TB SSD', 'ri-macbook-line', 'Laptop Gaming ASUS', 1200.0),
(2, 'Titanium, 256GB, Color Natural', 'ri-smartphone-line', 'iPhone 15 Pro', 999.0),
(3, 'Cancelación de ruido activa, Inalámbricos', 'ri-headphone-line', 'Audífonos Sony WH-1000XM5', 349.0);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260702050133_AddCatalogo2', '9.0.15');

INSERT INTO `Catalogo` (`Id`, `Descripcion`, `ImagenUrl`, `Nombre`, `Precio`)
VALUES (4, 'Consola híbrida de 64GB', 'ri-gamepad-line', 'Nintendo Switch OLED', 349.99),
(5, '34 pulgadas, 144Hz, IPS', 'ri-computer-line', 'Monitor LG UltraWide', 450.0),
(6, 'Cámara Mirrorless Full-Frame 33MP', 'ri-camera-line', 'Cámara Sony Alpha a7 IV', 2499.0),
(7, 'Mecánico inalámbrico, RGB', 'ri-keyboard-line', 'Teclado Keychron K8 Pro', 110.0),
(8, 'Ergonómico, sensor láser 8K DPI', 'ri-mouse-line', 'Mouse Logitech MX Master 3S', 99.99),
(9, '45mm, GPS, Midnight Aluminum', 'ri-smart-watch-line', 'Apple Watch Series 9', 399.0);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260702050423_AddMoreProducts', '9.0.15');

ALTER TABLE `Ordenes` ADD `DireccionEntrega` longtext CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Ordenes` ADD `TelefonoContacto` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260702051422_AddDeliveryDetails', '9.0.15');

COMMIT;
