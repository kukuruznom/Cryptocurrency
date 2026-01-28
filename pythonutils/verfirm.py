from cryptography.hazmat.primitives.asymmetric import ec
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.asymmetric.utils import Prehashed
from cryptography.exceptions import InvalidSignature
import binascii

# ====== DATOS ======

block_hash_hex = "13030b395ab609b9b36ac46da931f133041dbc4760051b251cb238c67d41e682"

signature_hex = "304402200f95ce634388a0e9123d751193fade74472e05d2227ebcb5be8a5b311fec0a97022059982c8231535223e8938b2c532cedb9b52a70a30e9f7ae2f03b451951fe23bf"

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