CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Commesse` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Commesse` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Jobs` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CommessaId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FaseDiLavorazione` longtext CHARACTER SET utf8mb4 NOT NULL,
    `StatoAvanzamento` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Jobs` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Jobs_Commesse_CommessaId` FOREIGN KEY (`CommessaId`) REFERENCES `Commesse` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Items` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Tipo` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Colore` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Taglia` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Modello` longtext CHARACTER SET utf8mb4 NOT NULL,
    `JobId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Items` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Items_Jobs_JobId` FOREIGN KEY (`JobId`) REFERENCES `Jobs` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_Items_JobId` ON `Items` (`JobId`);

CREATE INDEX `IX_Jobs_CommessaId` ON `Jobs` (`CommessaId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260511100741_InitialCreate', '8.0.0');

COMMIT;




