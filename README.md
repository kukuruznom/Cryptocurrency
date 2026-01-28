# KURS - Stablecoin Blockchain

[![Status](https://img.shields.io/badge/Status-Under%20Development-yellow)]()
[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)]()
[![License](https://img.shields.io/badge/License-MIT-green)]()

Centralized stablecoin with blockchain implemented from scratch in C#, 
ECDSA cryptography and complete chain validation.

## 📋 Table of Contents

- [Features](#-features)
- [Project Structure](#-project-structure)
- [Quick Start](#-quick-start)
- [Examples](#-examples)
- [Security](#-security)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)

## ✨ Features

- ✅ Blockchain with SHA256 + ECDSA validation
- ✅ Automated Genesis Block
- ✅ Transactions with digital signature
- ✅ Mint & Burn tokens
- ✅ JSON persistence
- ✅ Compatible with any OS (Windows, Linux, Mac)
- ✅ Relative paths for portability

## 📁 Project Structure

```
KURS/
├── Program.cs                  # Entry point and orchestration
├── Models/
│   └── Block.cs               # Block data structure
├── Crypto/
│   ├── BlockHasher.cs         # SHA256 hash calculation
│   ├── BlockSigner.cs         # ECDSA signing and verification
│   └── TransactionSigner.cs   # Transaction signing
├── Storage/
│   └── BlockStore.cs          # JSON persistence
├── Builders/
│   └── BlockBuilder.cs        # Block creation
├── Mint-Burn/
│   └── MintBurn.cs            # Minting and burning logic
├── Utils/
│   └── Hex.cs                 # Conversion utilities
└── blockchain/                # Block storage
    └── block_0.json           # Genesis block
```

## 🚀 Quick Start

### Requirements

- .NET 9.0 SDK
- Git

### Installation

```bash
# Clone repository
git clone https://github.com/kukuruznom/KURS.git
cd KURS

# Install dependencies
dotnet restore

# Run
dotnet run
```

**Expected output:**
```
Iniciando...
Firma válida: True
Bloque verificado correctamente: blockchain/block_0.json
Comenzando desde el ultimo bloque correcto...
```

## 📝 Examples

### Process genesis block
```csharp
ProcessGenesisBlock(blockPath, privateKeyHex, publicKeyHex);
```

### Validate complete chain
```csharp
int nextIndex = ProcessAllBlocks(blockPath, publicKeyHex);
```

### Create new block
```csharp
CreateNewBlock(index, previousHash, transactions, nonce, blockPath);
```

## 🔐 Security

### Block Validation

Each block is validated through:
- **SHA256 Hash** - Integrity verification
- **ECDSA Signature** - Chain authenticity
- **Chaining** - Validation of `previousHash`

### Block Structure

```json
{
  "index": 0,
  "timestamp": 1736180000,
  "previousHash": "0000000000000000000000000000000000000000000000000000000000000000",
  "transactions": ["MINT 1000 TO address1"],
  "nonce": 0,
  "hash": "ee18103e7d53ea9d91566e49a612e937ee6439c78a2b3fc8309f43de390ffcad",
  "firma": "304402203289...c5ec97"
}
```

## 📦 Dependencies

```xml
<PackageReference Include="NBitcoin" Version="9.0.4" />
```

## 🛣️ Roadmap

- [x] Basic blockchain
- [x] Block hashing
- [x] ECDSA digital signatures
- [ ] Mint & Burn
- [ ] UTXO Model
- [ ] REST API
- [ ] Mempool
- [ ] Wallet
- [ ] Distributed consensus

## 🤝 Contributing

Contributions are welcome. Please:

1. Fork the project
2. Create a branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ⚠️ Disclaimer

This project is **educational and experimental only**. Do not use in production.

## 📄 License

MIT License - see [LICENSE](LICENSE)

## 👨‍💻 Author

**kukuruznom**  
GitHub: [@kukuruznom](https://github.com/kukuruznom)

---

**Last updated**: January 2026
