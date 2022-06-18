# PrivCryptF-AES
Simple AES Encryption and Decryption tool in F#

Usage: `PrivCryptF.exe encrypt/decrypt directory password`<br>
Example: `PrivCryptF.exe encrypt C:\documents\anything 123helloworld`<br>
Output: Encrypted file with `*.aesfs` file extension

## Used Cryptography

I used Advanced Encryption Standard (AES) with a 256-bit key, 128-bit block size and Cipher Feedback Mode (CFB).

### ToDo

- Adding a simple GUI (Windows Forms or Eto Forms)
- Adding more options like custom Cipher Mode and custom Padding Mode
- Adding compress mode to shrink the file's size
