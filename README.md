# Esercizio Tecnico OD 

## Descrizione del progetto

L’applicazione è sviluppata in .NET con architettura multilivello per la gestione di “Commesse”, “Jobs” e “Items”, a partire da un file XML e con persistenza su database MySQL tramite Entity Framework Core.

Il sistema simula un flusso di integrazione con un servizio esterno, composto da:
- chiamata HTTP verso una Mock API
- autenticazione tramite API Key
- recupero dati in formato XML
- parsing e deserializzazione dei dati
- persistenza su database relazionale

---

L’architettura è basata su una Console Application utilizzata come client principale e su librerie .NET separate (Core e Infrastructure) per garantire una chiara separazione delle responsabilità.

---

L’intera infrastruttura può essere eseguita tramite container Docker, inclusa la componente database, garantendo portabilità e semplicità di esecuzione. In alternativa, è possibile utilizzare un’istanza locale di MySQL già installata sul sistema.

---

Il progetto è stato sviluppato in riferimento ai requisiti del documento “Esercizio_Tecnico_Foti-1.docx”.


## Struttura della soluzione

La soluzione è composta da più progetti .NET con responsabilità separate:

- **EsercizioTecnicoOD.Console** → Client principale (entry point)
- **EsercizioTecnicoOD.Core** → Modelli e interfacce
- **EsercizioTecnicoOD.Infrastructure** → Implementazioni tecniche (HTTP, DB, XML)
- **EsercizioTecnicoOD.MockApi** → API remota simulata
- **docker-compose.yml** → Infrastruttura (MySQL)

---

### Componenti della soluzione

#### 1) Console App (Client principale)

- Punto di ingresso dell’applicazione
- Avvia il workflow applicativo
- Orchestrazione dei servizi
- Gestione del flusso completo del processo

---

#### 2) Progetto Core

Contiene il dominio applicativo:

- Modelli dati
- Interfacce dei servizi

Servizi definiti:

- `IAuthService`
- `IRemoteApiService`
- `IXmlParserService`
- `IPersistenceService`

---

#### 3) Progetto Infrastructure

Contiene le implementazioni concrete dei servizi:

- `AuthService` → autenticazione tramite API Key
- `RemoteApiService` → chiamate HTTP verso Mock API
- `XmlParserService` → parsing XML
- `PersistenceService` → accesso al database MySQL tramite Entity Framework Core

---

#### 4) Progetto Mock API

- ASP.NET Core Minimal API che simula un sistema esterno
- Espone un singolo endpoint HTTP
- Gestisce autenticazione tramite API Key (header `X-API-KEY`)
- Restituisce un oggetto XML come risposta

---

#### 5) Infrastruttura Docker

La soluzione utilizza Docker Compose per la gestione del database MySQL.



---

## Modellazione del database

Il modello dati è stato progettato a partire dalla struttura XML fornita.

---

### Entità principali

#### Commessa (tabella Commesse)
- Id (PK)

---

#### Job (tabella Jobs)
- Id (PK)
- CommessaId (FK → Commesse)
- FaseDiLavorazione
- StatoAvanzamento

---

#### Item (tabella Items)
- Id (PK)
- JobId (FK → Jobs)
- Tipo
- Colore
- Taglia
- Modello

---

### Relazioni

- Una Commessa può contenere uno o più Job (relazione 1:N)
- Un Job appartiene ad una sola Commessa
- Un Job è associato ad un singolo Item e un Item appartiene ad un solo Job (relazione 1:1)

---

### Vincoli e regole di integrità

- Le chiavi primarie sono di tipo stringa (ID univoci)
- Le relazioni sono implementate tramite chiavi esterne con vincolo di integrità referenziale
- È previsto il cascade delete tra:
  - Commessa → Job
  - Job → Item

---

### Implementazione

Il modello è stato implementato tramite Entity Framework Core Code First, utilizzando migrations per la generazione dello schema fisico del database.
  
  
  
   ## Istruzioni di avvio 
  
  ### Prerequisiti

- .NET 8 SDK
- Docker Desktop (o alternativa equivalente)
oppure 
- una istanza locale del database MySQL 
---

### Istruzioni per l'esecuzione

#### 1. Avvio database MySQL


Il progetto utilizza MySQL. È possibile utilizzare:
1) MySQL tramite Docker (configurazione consigliata) 
2) oppure installazione MySQL locale


1) Avvio MySQL tramite Docker.


Avviare Docker (Docker Desktop o alternativa equivalente).

Dalla root della solution (dove si trova docker-compose.yml):


```bash
docker compose up -d
```

Verificare container attivo:

```bash
docker ps
```

2) Avvio MySQL tramite installazione locale

Aggiornare la connection string nel progetto:

```csharp
server=localhost;port=3306;database=esercitazioneod_db;user=<utente>;password=<password>
```

---

#### 2. Avvio Mock API

In un nuovo terminale:

```bash
dotnet run --project .\EsercizioTecnicoOD.MockApi
```

---

#### 3. Inizializzazione database

È possibile scegliere una delle seguenti modalità:

- Entity Framework Core
```bash
dotnet ef database update --project .\EsercizioTecnicoOD.Infrastructure --startup-project .\EsercizioTecnicoOD.Console
```

- Script SQL
In alternativa, eseguire lo script di inizializzazione

```sql
CREATE DATABASE IF NOT EXISTS esercitazioneod_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE esercitazioneod_db;

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

---

#### 4. Avvio applicazione principale

Eseguire:

```bash
dotnet run --project .\EsercizioTecnicoOD.Console
```

---

#### 5. Accesso al database MySQL nel container:(opzionale)


```bash
docker exec -it esercitazioneod-mysql mysql -u esercitazioneod_user -p
```

Password:

```text
dev_password
```



 
