from cryptography.hazmat.primitives.asymmetric import ec
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.asymmetric.utils import Prehashed
from cryptography.exceptions import InvalidSignature
import binascii

# ====== DATOS ======

block_hash_hex = "ee18103e7d53ea9d91566e49a612e937ee6439c78a2b3fc8309f43de390ffcad"

signature_hex = "3044022032896ce057bdc6b34718ed353fdf8942f86bf4484f6dba6101e3ea2ce3c5ec97022004ab5084609e5a4c4bce274b6fed072c6344bb51199d50f33f80fb1299ee09c1"

public_key_hex = "047c06b9d0602b41fdfecc5022feb94201c3d222e61916de617b302840fcaf2cd65c518444c239b17c2374d3fda4652479c1c8612eaefbdc48be766bffe8be7d6b"

# ====== VERIFICACIÓN ======

def verificar_firma_bitcoin(block_hash_hex, signature_hex, public_key_hex):
    # ⚠️ Bitcoin usa little-endian internamente
    block_hash = binascii.unhexlify(block_hash_hex)[::-1]

    signature = binascii.unhexlify(signature_hex)
    public_key_bytes = binascii.unhexlify(public_key_hex)

    public_key = ec.EllipticCurvePublicKey.from_encoded_point(
        ec.SECP256K1(),
        public_key_bytes
    )

    try:
        public_key.verify(
            signature,
            block_hash,
            ec.ECDSA(Prehashed(hashes.SHA256()))
        )
        return True
    except InvalidSignature:
        return False


if __name__ == "__main__":
    print(
        "✅ Firma válida"
        if verificar_firma_bitcoin(block_hash_hex, signature_hex, public_key_hex)
        else "❌ Firma inválida"
    )