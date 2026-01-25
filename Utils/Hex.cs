namespace KURS.Utils
{
    public static class Hex
    {

        /// Convierte una cadena hexadecimal a un array de bytes

        public static byte[] FromHexString(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2)
                .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                .ToArray();
        }

        /// Convierte un array de bytes a una cadena hexadecimal
        public static string ToHexString(byte[] bytes)
        {
            return string.Concat(bytes.Select(b => b.ToString("x2")));
        }
    }
}
