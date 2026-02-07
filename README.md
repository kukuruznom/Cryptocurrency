# Stablecoin Blockchain

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
- ✅ Automated Genesis Block with signature
- ✅ Transactions with digital signatures
- ✅ Mint & Burn token operations
- ✅ JSON persistence for blocks
- ✅ Interactive command shell (KURS_$> prompt)
- ✅ Signature verification for all blocks
- ✅ Compatible with Windows, Linux, and Mac
- ✅ Relative paths for portability
- ✅ Python utilities for key generation

## 📁 Project Structure

```
KURS/
├── Program.cs                  # Entry point and orchestration
├── Models/
│   └── Block.cs               # Block data structure (index, timestamp, previousHash, transactions, nonce, hash, firma)
├── Crypto/
│   ├── BlockHasher.cs         # SHA256 hash calculation
│   └── BlockSigner.cs         # ECDSA signing and verification
├── Storage/
│   └── BlockStore.cs          # JSON persistence and block loading
├── Builders/
│   └── BlockBuilder.cs        # Block creation utilities
├── Conosole/
│   └── Commands.cs            # Interactive command shell (shell v1.0)
├── Comands/
│   └── Functions.cs           # Command handlers
├── Api/
│   └── api.cs                 # API functionality
├── Balance/
│   └── Balance handling       # Token balance management
├── Utils/
│   └── Hex.cs                 # Hexadecimal conversion utilities
├── pythonutils/
│   ├── claveecsdagen.py       # Key generation utilities
│   └── verfirm.py             # Signature verification
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

### Start the blockchain application
```bash
dotnet run
```

### Interactive commands
Once running, use the blockchain shell:
```
KURS_$> help          # List all available commands
KURS_$> mint <amount> <address>  # Mint new tokens
KURS_$> burn <amount>  # Burn tokens
KURS_$> exit          # Exit the application
```

### Programmatic usage
```csharp
// Process genesis block with signature validation
ProcessGenesisBlock(blockPath, privateKeyHex, publicKeyHex);

// Validate complete blockchain
ProcessAllBlocks(blockPath, publicKeyHex);

// Start interactive shell
await Commands.Loop(blockPath);
```

## 🔐 Security

### Block Validation

Each block is validated through:
- **SHA256 Hash** - Integrity verification of block data (index, timestamp, previousHash, transactions, nonce)
- **ECDSA Signature** - Block authenticity using private/public key pairs
- **Signature Verification** - All blocks must have valid ECDSA signatures before acceptance
- **Block Chaining** - Validation of `previousHash` field for continuity

### Cryptographic Implementation

- **Hash Algorithm**: SHA256 for block hashing
- **Signature Scheme**: ECDSA (Elliptic Curve Digital Signature Algorithm)
- **Key Generation**: Done via Python utilities in `pythonutils/` directory
- **Signature Format**: DER encoded

### Block Structure

```json
{
  "index": 0,
  "timestamp": 1736180000,
  "previousHash": "0000000000000000000000000000000000000000000000000000000000000000",
  "transactions": ["MINT 1000 TO address1"],
  "nonce": 0,
  "hash": "sha256_block_hash_here",
  "firma": "ecdsa_signature_here"
}
```

## 📦 Dependencies

```xml
<PackageReference Include="NBitcoin" Version="9.0.4" />
```

## 🛣️ Roadmap

- [x] Basic blockchain with block structure
- [x] Block hashing (SHA256)
- [x] ECDSA digital signatures
- [x] Signature verification
- [x] Mint & Burn token operations
- [x] Interactive command shell
- [x] JSON block persistence
- [x] Cross-platform support
- [ ] Complete REST API
- [ ] Mempool/Transaction pool
- [ ] Multi-signature support
- [ ] Performance optimization
- [ ] Distributed consensus mechanism

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
