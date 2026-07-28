# ⚡️ Asynchronous Crypto Telegram Bot Engine

A high-performance, asynchronous Telegram Bot engineered with C# (.NET 8) and integrated with the Binance REST API v3 to deliver real-time cryptographic market data. The system is architected utilizing Clean Code principles, the Strategy Pattern for command routing, and optimized connection pooling.

---

## 🛠️ Tech Stack & Infrastructure

*   **Core Engine:** Microsoft .NET 8.0 SDK (ASP.NET Core Worker Service)
*   **API Wrapper:** Telegram.Bot Framework (Asynchronous long-polling orchestration)
*   **External Integration:** Binance REST API v3 (Market data endpoints)
*   **Containerization:** Docker (Multi-stage lightweight execution environments)

---

## 🏗️ Architectural Overview

The application completely avoids monolithic if-else chains, utilizing an extensible polymorphic pipeline to route commands dynamically:

### 1. Unified Update Handler (`UpdateHandler.cs`)
*   Intercepts incoming network updates from the Telegram Bot API.
*   Abstracts and normalizes input vectors, processing standard slash commands and custom reply-keyboard payloads through a unified interface.

### 2. Polymorphic Command Router (`CommandRouter.cs`)
*   Implements the **Strategy Pattern** for decoupled scalability.
*   Maintains an isolated, interface-driven collection (`IEnumerable<ICommand>`) to dynamically select and execute the appropriate command handler based on user input.

### 3. Market Data Engine (`PriceCommand.cs`)
*   Handles real-time price evaluation using C# pattern matching to map user requests to exchange symbols (`BTCUSDT`, `ETHUSDT`, `TONUSDT`).
*   Utilizes `IHttpClientFactory` for managed connection pooling, preventing socket exhaustion under high concurrent loads.

---

## 📋 Interface & User Workflow

*   `/start` - Initializes the execution sequence and deploys responsive `ReplyKeyboardMarkup` layouts for mobile and desktop screens.
*   `🪙 Bitcoin` / `🔷 Ethereum` / `💎 Toncoin` - Fetches the latest tickers from Binance and dispatches instant text updates back to the user chat.

---

## 💻 Deployment & Local Quickstart

### Step 1: Clone & Configure Token
Clone the repository and inject your secure API credentials into the `appsettings.json` configuration file:

```json
{
  "TelegramBot": {
    "Token": "YOUR_SECRET_TELEGRAM_BOT_TOKEN"
  }
}
```

### Step 2: Compile & Execute Locally
Restore NuGet dependencies and launch the background worker service:

```bash
dotnet run
```

### Step 3: Containerized Deployment (Docker)
Build and run a minimal, production-ready Linux runtime container:

```bash
docker build -t crypto-bot-service .
docker run -d --name live-crypto-bot crypto-bot-service
```
