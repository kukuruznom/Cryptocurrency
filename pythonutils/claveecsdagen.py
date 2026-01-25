from ecdsa import SigningKey, SECP256k1

# Generar clave privada
sk = SigningKey.generate(curve=SECP256k1)

# Obtener clave pública
vk = sk.get_verifying_key()

# Clave privada (32 bytes)
private_key_hex = sk.to_string().hex()

# Clave pública NO comprimida: 04 + X + Y
public_key_hex = "04" + vk.to_string().hex()

print("Clave privada:", private_key_hex)
print("Clave pública:", public_key_hex)

# Guardar en archivos
with open("clave_privada.txt", "w") as f:
    f.write(private_key_hex)

with open("clave_publica.txt", "w") as f:
    f.write(public_key_hex)
#verificar firma
message = b"Mensaje para firmar"
signature = sk.sign(message)
is_valid = vk.verify(signature, message)
print("la firma es:", is_valid)