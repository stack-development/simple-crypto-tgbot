# ⚡️ Asynchronous Production-Ready Crypto Telegram Bot Engine

A high-performance, asynchronous **Telegram Bot** engineered with **C# (.NET 8)** and integrated with the official **Binance API** to deliver real-time macroeconomic cryptographic market data. Architected utilizing clean code principles, custom runtime command routing, and zero-allocation string parsing.

---

## 🛠️ TECH STACK & INFRASTRUCTURE
*   **Core Engine:** `Microsoft .NET 8.0 Runtime` (Asynchronous C# Baseline)
*   **API Wrapper:** `Telegram.Bot Framework` (Non-blocking long-polling orchestration)
*   **External Integration:** `Binance REST API v3` (High-frequency ticker sockets)
*   **Containerization:** `Docker` (Multi-stage lightweight execution environments)
*   **Operating System:** `Arch Linux` (Development & profiling baseline)

---

## 🏗️ ADVANCED ARCHITECTURAL OVERVIEW

This bot engine avoids messy "monolithic if-else chains" or beginner-level string parsing. It utilizes an advanced architectural pipeline to route commands dynamically:

### 1. Unified Update Handler (`UpdateHandler.cs`)
*   Intercepts incoming network updates from the Telegram Bot API.
*   Dynamically normalizes input vectors, differentiating standard slash commands (`/start`) from structural custom reply-keyboard payloads without pipeline latency.

### 2. Polymorphic Command Router (`CommandRouter.cs`)
*   Implements a clean **Strategy Pattern** for decoupled scalability.
*   Utilizes a dynamic interface-based collection iteration (`IEnumerable<ICommand>`) executing a non-blocking `FirstOrDefault` query, checking contextual capability via `CanExecute()` rulesets.

### 3. Single-Responsibility Engine (`PriceCommand.cs`)
*   An ultra-scalable, single-class price evaluation engine. 
*   Leverages highly efficient C# **Pattern Matching** switches to translate interface markup identifiers directly into physical stock exchange symbols (`BTCUSDT`, `ETHUSDT`, `TONUSDT`).
*   Utilizes `IHttpClientFactory` for managed connection pooling, drastically minimizing socket exhaustion under high concurrent load.

---

## 📋 INTERFACE & USER WORKFLOW
*   `/start` - Initializes the execution sequence, generating native, hardware-optimized responsive `ReplyKeyboardMarkup` blocks for mobile and desktop screens.
*   `🪙 Bitcoin` / `🔷 Ethereum` / `💎 Toncoin` - Dispatches instantaneous, self-editing HTTP visual components directly updating specific Telegram thread message indexes, preventing chat clutter.

---

## 💻 INDUSTRIAL DEPLOYMENT & CONSOLE STARTUP

### Step 1: Clone & Configure Token
Clone the codebase. Open your environment variables configuration space (`appsettings.json`) and safely mount your secure network authentication hash:
```json
{
  "TelegramBot": {
    "Token": "YOUR_SECRET_TELEGRAM_BOT_TOKEN"
  }
}
```

### Step 2: Compile & Execute Locally
Restore system dependencies and spin up the non-blocking worker thread:
```bash
dotnet run
```

### Step 3: Enterprise Cloud Deployment (Docker)
Build a minimal, secure multi-stage Linux runtime environment executing the isolated bot service:
```bash
docker build -t crypto-bot-service .
docker run -d --name live-crypto-bot crypto-bot-service
```
