module Encryption

open System
open System.Text
open System.IO
open System.Security.Cryptography

let HashKey(input : string) : byte[] =
    use sha256 = SHA256.Create()
    sha256.ComputeHash(Encoding.UTF8.GetBytes(input))

let GenerateSalt() : byte[] =
    let data : byte array = Array.zeroCreate 32
    use rng = new RNGCryptoServiceProvider()
    for i = 1 to 10 do
        rng.GetBytes(data)
    data


let Encrypt(file : string, key : string) : unit = 
    let fs              = new FileStream(String.Format("{0}.aesfs", file), FileMode.Create)
    let password        = HashKey(key)
    let salt            = GenerateSalt()
        
    use aesAlg = Aes.Create()
    aesAlg.KeySize      <- 256
    aesAlg.BlockSize    <- 128
    aesAlg.Padding      <- PaddingMode.PKCS7

    let key = new Rfc2898DeriveBytes(key, salt, 1000)
    aesAlg.Key          <- key.GetBytes(aesAlg.KeySize / 8)
    aesAlg.IV           <- key.GetBytes(aesAlg.BlockSize / 8)

    aesAlg.Mode         <- CipherMode.CFB

    fs.Write(salt, 0, salt.Length)

    let cs = new CryptoStream(fs, aesAlg.CreateEncryptor(), CryptoStreamMode.Write)
    let fsIn = new FileStream(file, FileMode.Open)

    let mutable buffer : byte array = Array.zeroCreate 1048576
    let mutable read : int = Unchecked.defaultof<int> 

    try
        while (read <- fsIn.Read (buffer, 0, buffer.Length); read > 0) do
            cs.Write(buffer, 0, read)

        fsIn.Close()
    finally
        cs.Close()
        fs.Close()

let Decrypt(file : string, key : string) : unit =
    let fs              = new FileStream(file, FileMode.Open)
    let password        = HashKey(key)
    let salt : byte[]   = Array.zeroCreate 32

    fs.Read(salt, 0, salt.Length)

    use aesAlg = Aes.Create()
    aesAlg.KeySize      <- 256
    aesAlg.BlockSize    <- 128

    let key = new Rfc2898DeriveBytes(key, salt, 1000)
    aesAlg.Key          <- key.GetBytes(aesAlg.KeySize / 8)
    aesAlg.IV           <- key.GetBytes(aesAlg.BlockSize / 8)
    aesAlg.Padding      <- PaddingMode.PKCS7;
    aesAlg.Mode         <- CipherMode.CFB;

    let cs = new CryptoStream(fs, aesAlg.CreateDecryptor(), CryptoStreamMode.Read)
    let fsOut = new FileStream(file.Substring(0, file.Length - 6), FileMode.Create)

    let mutable buffer : byte array = Array.zeroCreate 1048576
    let mutable read : int = Unchecked.defaultof<int> 

    try
        while (read <- cs.Read (buffer, 0, buffer.Length); read > 0) do
            fsOut.Write(buffer, 0, read)
        
        fsOut.Close()
    finally
        cs.Close()
        fs.Close()