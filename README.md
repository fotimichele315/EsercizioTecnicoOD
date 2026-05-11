# Esercizio Tecnico OD - .NET Application

## Descrizione del progetto

Applicazione sviluppata in .NET con architettura multilivello per la gestione di “Commesse”, “Jobs” e “Items”, partendo da un file XML e persistendo i dati su database MySQL tramite Entity Framework Core.

Il progetto simula un flusso reale di integrazione tra:
- API esterna (Mock)
- Parsing XML
- Logica applicativa
- Persistenza su database

---

## Architettura della soluzione

La soluzione è composta da 5 progetti:

- **EsercizioTecnicoOD.Core**
  - Entità di dominio (Commessa, Job, Item)
  - Interfacce dei servizi

- **EsercizioTecnicoOD.Infrastructure**
  - Implementazione servizi
  - DbContext EF Core
  - Persistence layer

- **EsercizioTecnicoOD.Console**
  - Entry point applicazione
  - Dependency Injection
  - Orchestrazione flusso

- **Mock API**
  - Endpoint XML
  - Validazione API Key

- **Database MySQL **
  - Persistenza dati

---

## Modellazione database

### Tabelle

**Commesse**
- Id (PK)

**Jobs**
- Id (PK)
- CommessaId (FK → Commesse)
- FaseDiLavorazione
- StatoAvanzamento

**Items**
- Id (PK)
- JobId (FK → Jobs)
- Tipo
- Colore
- Taglia
- Modello

---

### Relazioni

- Commessa → Jobs (1:N)
- Job → Item (1:1)

---

### Vincoli

- Foreign Key con Cascade Delete
- Indici sulle FK
- Schema generato tramite EF Core Migrations (Code First)

---

## Docker (MySQL)

```yaml
version: '3.8'

services:

  mysql:
    image: mysql:8.0
    container_name: EsercizioTecnicood-mysql

    environment:
      MYSQL_ROOT_PASSWORD: dev_root_password
      MYSQL_DATABASE: EsercizioTecnicood_db
      MYSQL_USER: EsercizioTecnicood_user
      MYSQL_PASSWORD: dev_password

    ports:
      - "3306:3306"

    volumes:
      - mysql_data:/var/lib/mysql

volumes:
  mysql_data:
 ```
 
 
 ---
  
  
  ## Prerequisiti

- .NET 8 SDK
- Docker Desktop (o alternativa equivalente).  
  In locale il database MySQL è stato eseguito tramite Docker Engine installato su Ubuntu (WSL2).

---

## Istruzioni di avvio

### 1. Avviare il database MySQL


Il progetto utilizza MySQL. È possibile utilizzare:

- MySQL tramite Docker (configurazione consigliata)
- oppure una installazione MySQL locale

In caso di MySQL locale, aggiornare la connection string nel progetto:

```csharp
server=localhost;
port=3306;
database=EsercizioTecnicood_db;
user=<utente>;
password=<password>
```

In caso di uso di Docker, eseguire dalla root della solution:

```bash
docker compose up -d
```

Verificare che il container sia attivo:

```bash
docker ps
```

---

### 2. Avviare la Mock API

In un nuovo terminale:

```bash
dotnet run --project .\EsercizioTecnicoOD.MockApi
```

---

### 3. Applicare le migration Entity Framework Core

Dalla root della solution:

```bash
dotnet ef database update --project .\EsercizioTecnicoOD.Infrastructure --startup-project .\EsercizioTecnicoOD.Console
```

---

### 4. Avviare l'applicazione principale

In un nuovo terminale:

```bash
dotnet run --project .\EsercizioTecnicoOD.Console
```

---

### 5. Verifica dati su database 

Accesso al database MySQL nel container:

```bash
docker exec -it EsercizioTecnicood-mysql mysql -u EsercizioTecnicood_user -p
```

Password:

```text
dev_password
```

Query di verifica:

```sql
USE EsercizioTecnicood_db;
SHOW TABLES;

SELECT * FROM Commesse;
SELECT * FROM Jobs;
SELECT * FROM Items;
```


---

## Creazione manuale database MySQL (alternativa a EF Core)

In alternativa alle Entity Framework Core Migrations, è possibile creare manualmente il database eseguendo il seguente script SQL.

### 1. Creazione database

```sql
CREATE DATABASE IF NOT EXISTS EsercizioTecnicood_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE EsercizioTecnicood_db;
```

---

### 2. Creazione tabelle

```sql
CREATE TABLE `Commesse` (
    `Id` varchar(255) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Jobs` (
    `Id` varchar(255) NOT NULL,
    `CommessaId` varchar(255) NOT NULL,
    `FaseDiLavorazione` longtext NOT NULL,
    `StatoAvanzamento` longtext NOT NULL,

    PRIMARY KEY (`Id`),

    CONSTRAINT `FK_Jobs_Commesse`
        FOREIGN KEY (`CommessaId`)
        REFERENCES `Commesse` (`Id`)
        ON DELETE CASCADE
);

CREATE TABLE `Items` (
    `Id` varchar(255) NOT NULL,
    `Tipo` longtext NOT NULL,
    `Colore` longtext NOT NULL,
    `Taglia` longtext NOT NULL,
    `Modello` longtext NOT NULL,
    `JobId` varchar(255) NOT NULL,

    PRIMARY KEY (`Id`),

    CONSTRAINT `FK_Items_Jobs`
        FOREIGN KEY (`JobId`)
        REFERENCES `Jobs` (`Id`)
        ON DELETE CASCADE
);

CREATE UNIQUE INDEX `IX_Items_JobId`
ON `Items` (`JobId`);

CREATE INDEX `IX_Jobs_CommessaId`
ON `Jobs` (`CommessaId`);
```